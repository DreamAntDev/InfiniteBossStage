using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Common.Controller;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private CharacterController characterController;
    public Animator animator { get; private set; }

    [HideInInspector]
    public Vector3 defaultMoveVector = new Vector3(0, -0.1f, 0);

    private Vector3 moveVector = Vector3.zero;

    Character.State.IState state;
    Dictionary<Character.State.State, Character.State.IState> stateContainer;
    Vector3 reserveMoveVector = Vector3.zero;
    Vector3 acceleration = Vector3.zero;
    bool cachedIsGround = true;

    public Data.Character.CharacterStatus statusData;
    Character.Status status;

    private void Awake()
    {
        if (PlayerController.instance != null)
        {
            Debug.LogError("PlayerController Is SingleTone");
            return;
        }

        PlayerController.instance = this;
        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Move.performed += OnMovePerform;
        playerInputActions.Player.Move.canceled += OnMoveCancel;
        playerInputActions.Player.Fire.performed += OnAttackPerform;
        playerInputActions.Player.Avoid.performed += OnAvoidPerform;

        this.characterController = GetComponent<CharacterController>();
        this.animator = GetComponent<Animator>();

        stateContainer = new Dictionary<Character.State.State, Character.State.IState>();
        stateContainer.Add(Character.State.State.Idle, new Character.State.Idle());
        stateContainer.Add(Character.State.State.Move, new Character.State.Move());
        stateContainer.Add(Character.State.State.Attack, new Character.State.Attack());
        stateContainer.Add(Character.State.State.Avoid, new Character.State.Avoid());
        stateContainer.Add(Character.State.State.Grogy, new Character.State.Grogy());
        stateContainer.Add(Character.State.State.Dead, new Character.State.Dead());

        this.state = stateContainer[Character.State.State.Idle];
    }

    private void Start()
    {
        status = new Character.Status(statusData);
    }

    private void Update()
    {
        bool success = this.state.Update();
        if(success == false)
        {
            if (this.reserveMoveVector != Vector3.zero)
            {
                SetState(Character.State.State.Move);
                var moveState = this.state as Character.State.Move;
                if (moveState == null)
                    return;

                moveState.SetMoveDirection(this.reserveMoveVector);

            }
            else
            {
                SetState(Character.State.State.Idle);
            }
        }

        if(this.characterController.isGrounded != this.cachedIsGround)
        {
            if(this.characterController.isGrounded == true)
            {
                // OnGround
                if(this.state.GetStateType() == Character.State.State.Grogy)
                {
                    var grogy = this.state as Character.State.Grogy;
                    grogy.OnGround();
                }
            }
            else
            {
                // ReleaseGround
            }
            this.cachedIsGround = this.characterController.isGrounded;
        }

        Vector3 gravityPerFrame = Physics.gravity * Time.deltaTime * Time.deltaTime;
        // AppendGravity
        if (this.characterController.isGrounded == false)
        {
            
            this.acceleration += gravityPerFrame;
        }
        else
        {
            this.acceleration = defaultMoveVector;
            ////this.acceleration = new Vector3(0, -0.1f, 0);
            //if(this.state.GetStateType() == Character.State.State.Grogy)
            //{
            //    this.acceleration = new Vector3(0, 0, 0);
            //}
        }

        AppendMoveVectorPerFrame(this.acceleration);
        this.characterController.Move(this.moveVector);
        this.moveVector = Vector3.zero;

        this.status.RecoveryStamina();
    }

    private void SetState(Character.State.State state)
    {
        if (this.state.GetStateType() == state)
            return;

        if (this.state.CanExit(state) == true)
        {
            this.state.Exit();
            this.state = stateContainer[state];
            this.state.Entry();
        }
    }

    private void OnMovePerform(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 moveVec = new Vector3(input.x, 0, input.y);// ī�޶� ���� ���⿡ ���� ������ �ʿ���

        this.reserveMoveVector = moveVec;
        if (this.state.GetStateType() == Character.State.State.Attack)
        {
            return;
        }

        SetState(Character.State.State.Move);
        
        var moveState = this.state as Character.State.Move;
        if (moveState == null)
            return;

        moveState.SetMoveDirection(moveVec);
    }
    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        this.reserveMoveVector = Vector3.zero;
        if (this.state.GetStateType() == Character.State.State.Move)
        {
            this.SetState(Character.State.State.Idle);
        }
    }

    private void OnAttackPerform(InputAction.CallbackContext context)
    {
        if (this.state.GetStateType() == Character.State.State.Attack)
        {
            var attackState = this.state as Character.State.Attack;
            attackState.SetCombo();
        }
        else
        {
            this.SetState(Character.State.State.Attack);
        }
    }
    private void OnAvoidPerform(InputAction.CallbackContext context)
    {
        if (this.state.GetStateType() == Character.State.State.Avoid)
            return;

        if (this.state.GetStateType() == Character.State.State.Grogy)
            return;

        bool successAvoid = this.status.OnAvoid();
        if (successAvoid == false)
            return;

        this.SetState(Character.State.State.Avoid);
        var avoidState = this.state as Character.State.Avoid;
        Vector3 avoidVector = (reserveMoveVector == Vector3.zero) ? this.characterController.transform.forward : reserveMoveVector;
        avoidState?.SetMoveVector(avoidVector);
    }
    
    public void OnDamage(int damage = 0)
    {
        if (this.state.GetStateType() == Character.State.State.Avoid)
            return;

        this.status.OnDamage(damage);
        if (this.status.CurrentHP <= 0)
            this.SetState(Character.State.State.Dead);

        if (this.state.GetStateType() == Character.State.State.Grogy)
            return;

        this.SetState(Character.State.State.Grogy);
        var grogyState = this.state as Character.State.Grogy;

        int type = Random.Range(0, 3);
        if (type == 0)
        {
            Vector3 moveVector = this.characterController.transform.forward;
            moveVector *= -1;
            moveVector += new Vector3(0, 1f, 0);
            moveVector *= 5;

            Character.State.Grogy.GrogyType types = Character.State.Grogy.GrogyType.Push | Character.State.Grogy.GrogyType.Upper;
            Character.State.Grogy.Context context = new Character.State.Grogy.Context(types, 3.0f, moveVector, 3.0f);
            grogyState?.SetContext(context);
        }
        else if (type == 1)
        {
            Vector3 moveVector = this.characterController.transform.forward;
            moveVector *= -1;
            moveVector *= 10;

            Character.State.Grogy.GrogyType types = Character.State.Grogy.GrogyType.Push;
            Character.State.Grogy.Context context = new Character.State.Grogy.Context(types, 1.5f, moveVector, 0.5f);
            grogyState?.SetContext(context);
        }
        else if(type == 2)
        {
            Character.State.Grogy.GrogyType types = Character.State.Grogy.GrogyType.None;
            Character.State.Grogy.Context context = new Character.State.Grogy.Context(types, 1.0f, Vector3.zero, 1.0f);
            grogyState?.SetContext(context);
        }
    }

    public void AppendMoveVectorPerFrame(Vector3 moveVector)
    {
        this.moveVector += moveVector;
    }

    private void AnimationEvent_Attack()
    {
        float damage = 10.0f;
        var list = this.GetComponent<Detector>().GetCurrentList();
        foreach(var obj in list)
        {
            var combatFighter = obj.GetComponent<IBS.Combat.CombatFighter>();
            if(combatFighter != null)
            {
                combatFighter.TakeDamage(damage);
            }
        }
    }
}

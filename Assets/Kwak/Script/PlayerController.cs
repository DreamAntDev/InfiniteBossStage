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

    private Vector3 moveVector = Vector3.zero;

    Character.State.IState state;
    Dictionary<Character.State.State, Character.State.IState> stateContainer;
    Vector3 reserveMoveVector = Vector3.zero;

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

        this.state = stateContainer[Character.State.State.Idle];
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
        this.characterController.Move(this.moveVector);
        this.moveVector = Vector3.zero;
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
        Vector3 moveVec = new Vector3(input.x, 0, input.y);// 카메라가 보는 방향에 따라 연산이 필요함

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
        this.SetState(Character.State.State.Attack);
    }
    private void OnAvoidPerform(InputAction.CallbackContext context)
    {
        if (this.state.GetStateType() == Character.State.State.Avoid)
            return;

        this.SetState(Character.State.State.Avoid);
        var avoidState = this.state as Character.State.Avoid;
        Vector3 avoidVector = (reserveMoveVector == Vector3.zero) ? this.characterController.transform.forward : reserveMoveVector;
        avoidState.SetMoveVector(avoidVector);
    }

    public void OnDamage()
    {
        Debug.Log("OnDamage");
    }

    public void AppendMoveVectorPerFrame(Vector3 moveVector)
    {
        this.moveVector += moveVector;
    }
}

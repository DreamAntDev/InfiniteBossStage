using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Common.Controller;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public CharacterController characterController { get; private set; }
    public Animator animator { get; private set; }

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

        this.characterController = GetComponent<CharacterController>();
        this.animator = GetComponent<Animator>();

        stateContainer = new Dictionary<Character.State.State, Character.State.IState>();
        stateContainer.Add(Character.State.State.Idle, new Character.State.Idle());
        stateContainer.Add(Character.State.State.Move, new Character.State.Move());
        stateContainer.Add(Character.State.State.Attack, new Character.State.Attack());

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
    }

    private void SetState(Character.State.State state)
    {
        if (this.state.GetStateType() == state)
            return;

        this.state.Exit();
        this.state = stateContainer[state];
        this.state.Entry();
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
}

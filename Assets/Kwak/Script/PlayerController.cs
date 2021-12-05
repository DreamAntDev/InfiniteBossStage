using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Common.Controller;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Move.performed += OnMovePerform;
        playerInputActions.Player.Move.canceled += OnMoveCancel;
        playerInputActions.Player.Fire.performed += OnAttackPerform;

        this.characterController = GetComponent<CharacterController>();
        this.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 캐릭터 State에 따라서 동작해야 할 것 같다
        if (moveDirection != Vector3.zero)
        {
            this.characterController.Move(Time.deltaTime * moveDirection * 3.0f);
        }
    }

    private void OnMovePerform(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        // 카메라가 보는 방향에 따라 연산이 필요함
        this.moveDirection = new Vector3(input.x, 0, input.y);
        Quaternion toRotation = Quaternion.LookRotation(this.moveDirection);
        this.transform.rotation = toRotation;

        this.animator.CrossFade("Run", 0.15f);
        
    }
    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        this.moveDirection = Vector3.zero;
        this.animator.CrossFade("Idle",0.15f);
    }

    private void OnAttackPerform(InputAction.CallbackContext context)
    {
        this.animator.CrossFade("Attack01", 0.05f);
    }
}

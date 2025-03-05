using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XboxControls : MonoBehaviour



{
    [SerializeField] private Rigidbody rbD;
    [SerializeField] private float speed;
    private Vector2 moveInputValue;

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
        Debug.Log(moveInputValue);
    }

    private void OnFlashlight()
    {
        Debug.Log("flashlight on");
    }

    private void OnCrouch()
    {
        Debug.Log("crouch on");
    }

    private void OnSprint()
    {
        Debug.Log("sprinting");
    }



    private void MoveLogicMethod()
    {
        Vector2 result = moveInputValue * speed * Time.fixedDeltaTime;
        rbD.velocity = result;
    }

    private void FixedUpdate()
    {
        MoveLogicMethod();
    }
}

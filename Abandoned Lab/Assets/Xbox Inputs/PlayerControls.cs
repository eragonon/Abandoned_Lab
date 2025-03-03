using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private Vector2 moveInputValue;

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>(); // generic Vector2 [X,Y]
        Debug.Log(moveInputValue); // moveInputValue = (0,0) of joystick
    }

    private void OnButtonRegular()
    {
        Debug.Log("button pressed");
    }

    private void OnButtonHold()
    {
        Debug.Log("button held");
    }

    private void MoveLogicMethod()
    {
        Vector2 result = moveInputValue * speed * Time.fixedDeltaTime;
        rb.velocity = result;
    }

    private void FixedUpdate()
    {
        MoveLogicMethod();
    }
}

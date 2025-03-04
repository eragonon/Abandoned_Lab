using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XboxControls : MonoBehaviour



{
    [SerializedField] private Rigidbody rbD;
    [SerializedField] private float speed;
    private Vector2 moveInputValue;

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
        Debug.Log(moveInputValue);
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

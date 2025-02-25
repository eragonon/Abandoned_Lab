using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool _enable = true;
    [SerializeField, Range(0, 0.1f)] private float _Amplitude = 0.015f;  // Original amplitude value
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
    }

    void Update()
    {
        if (!_enable) return;
        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;

        // Temporary variable to hold adjusted amplitude
        float currentAmplitude = _Amplitude;

        // Check if Shift is held down and double the amplitude temporarily
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentAmplitude *= 2;  // Temporarily double the amplitude
        }

        // Footstep motion with the adjusted amplitude
        pos.y += Mathf.Sin(Time.time * _frequency) * currentAmplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * currentAmplitude * 2;

        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        if (speed < _toggleSpeed) return;
        if (!_controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }

    // Optional: Reset the camera position (if needed)
    private void ResetPosition()
    {
        if (_camera.localPosition != _startPos)
        {
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, Time.deltaTime * 5f);
        }
    }
}

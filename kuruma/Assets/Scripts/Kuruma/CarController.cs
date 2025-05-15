using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider _frontLeftWheelCollider;
    public WheelCollider _frontRightWheelCollider;
    public WheelCollider _backLeftWheelCollider;
    public WheelCollider _backRightWheelCollider;

    public Transform _frontLeftWheelTransform;
    public Transform _frontRightWheelTransform;
    public Transform _backLeftWheelTransform;
    public Transform _backRightWheelTransform;

    public float _breakForce = 1000f;
    public float _motorForce = 100f;
    public float _maxAngle = 30f;

    private float _horizontalInput;
    private float _verticalInput;
    private float _currentBreakForce;
    private float _currentSteerAngle;

    private bool _isBreak;

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MotorCar();
        SteerCar();
        UpdateAllWheels();
    }
    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _isBreak = Input.GetKey(KeyCode.Space);
    }
    private void MotorCar()
    {
        _frontLeftWheelCollider.motorTorque = _verticalInput * _motorForce;
        _frontRightWheelCollider.motorTorque = _verticalInput * _motorForce;
        _currentBreakForce = _isBreak ? _breakForce : 0f;

        ApplyBreak();
    }
    private void ApplyBreak()
    {
        _frontLeftWheelCollider.brakeTorque = _currentBreakForce;
        _frontRightWheelCollider.brakeTorque = _currentBreakForce;
        _backLeftWheelCollider.brakeTorque = _currentBreakForce;
        _backRightWheelCollider.brakeTorque = _currentBreakForce;
    }
    private void SteerCar()
    {
        _currentSteerAngle = _horizontalInput * _maxAngle;
        _frontLeftWheelCollider.steerAngle = _currentSteerAngle;
        _frontRightWheelCollider.steerAngle = _currentSteerAngle;
    }
    private void UpdateWheel(WheelCollider _wheelCollider, Transform _transform)
    {
        Vector3 position;
        Quaternion rotation;
        _wheelCollider.GetWorldPose(out position, out rotation);
        _transform.position = position;
        _transform.rotation = rotation;
    }
    private void UpdateAllWheels()
    {
        UpdateWheel(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateWheel(_frontRightWheelCollider, _frontRightWheelTransform);
        UpdateWheel(_backLeftWheelCollider, _backLeftWheelTransform);
        UpdateWheel(_backRightWheelCollider, _backRightWheelTransform);
    }
    
}

using Modification;
using UnityEngine;

public class PlayerMovable : MovableCharacter, IModificationListener<float>
{
    private Camera _camera;

    private float _currentTargetRotation;
    private float _timeToReachTargetRotation = 0.14f;
    private float _dampedTargetRotationCurrentVelocity;
    private float _dampedTargetRotationPassedTime;

    private const float FullRotation = 360f;

    protected override void OnAwake()
    {
        _camera = Camera.main;
    }

    public override void Move(Vector3 direction)
    {
        float inputAngleDirection = GetDirectionAngleFrom(direction);
        inputAngleDirection = AddCameraAngleTo(inputAngleDirection);

        Rotate(inputAngleDirection);

        Vector3 newDirection = Quaternion.Euler(0, inputAngleDirection, 0) * Vector3.forward;
        OnMove(newDirection);
    }

    private void Rotate(float inputAngleDirection)
    {
        if (inputAngleDirection != _currentTargetRotation)
            UpdateTargetRotationData(inputAngleDirection);

        RotateTowardsTargetRotation();
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        _currentTargetRotation = targetAngle;
        _dampedTargetRotationPassedTime = 0f;
    }

    private void RotateTowardsTargetRotation()
    {
        float currentYAngle = GetCurrentRotationAngle();

        if (currentYAngle == _currentTargetRotation)
            return;

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, _currentTargetRotation, ref _dampedTargetRotationCurrentVelocity, _timeToReachTargetRotation - _dampedTargetRotationPassedTime);
        _dampedTargetRotationPassedTime += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0, smoothedYAngle, 0);
        transform.rotation = targetRotation;
    }

    private float GetCurrentRotationAngle() => transform.rotation.eulerAngles.y;

    private void OnMove(Vector3 inputDirection)
    {
        Vector3 normalizedInputDirection = inputDirection.normalized;
        MoveVelocity(normalizedInputDirection);
    }


    private float GetDirectionAngleFrom(Vector3 inputMoveDirection)
    {
        float directionAngle = Mathf.Atan2(inputMoveDirection.x, inputMoveDirection.z) * Mathf.Rad2Deg;

        if (directionAngle < 0)
            directionAngle += FullRotation;

        return directionAngle;
    }

    private float AddCameraAngleTo(float angle)
    {
        angle += _camera.transform.eulerAngles.y;

        if (angle > FullRotation)
            angle -= FullRotation;

        return angle;
    }

    public void OnModificationUpdate(float value)
    {
        _speedRate = value;
    }
}
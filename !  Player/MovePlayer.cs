using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class MovePlayer
{
    #region
    public Player Player;
    public MovePlayer (Player player)
    {
        Player = player;
    }
    #endregion

    [Header("Movement Speed")]
    public float DefaultSpeed = 2f;
    public float WalkingSpeed = 1f;
    public float RunningSpeed = 2f;
    public float ModifiedSpeed = 2f;

    [Header("Turn Smoothing")]
    public float AdjustAngle = 80f;
    public Vector2 JoyStickInput;
    public Vector3 currentTargetRotation;
    public Vector3 rotationTime = new Vector3(0f, 0.14f, 0f);
    public Vector3 dampedRotationVelocity;
    public Vector3 dampedRotationTime;

    [Header("Minimap Position")]
    public Vector3 MinimapPosOffset = new Vector3(-12f, 10f, -14f);
    public Vector3 MinimapRotation = new Vector3(25f, 40f, 0f);

    [Header("Movement State")]
    public PlayerMovementState PlayerState;
    public MovementType MovementType = MovementType.Run;
    public bool StoryMode;

    [Header("Switch Button")]
    public Button SwitchMovementButton;


    public void Start()
    {
        SwitchMovementButton = GameObject.Find("SwitchMVButton").GetComponent<Button>();

        SwitchMovementButton.onClick.AddListener(SwitchMovement);
    }

    private void SwitchMovement()
    {
        if (MovementType == MovementType.Walk)
        {
            MovementType = MovementType.Run;
        }
        else
        {
            MovementType = MovementType.Walk;
        }
    }

    public void Update()
    {
        PositionMinimap();
        if (Player.PlayerJoyStick.IsUsingJoyStick)
        {
            if (MovementType == MovementType.Walk)
            {
                PlayerState = PlayerMovementState.Walking;
                ModifiedSpeed = DefaultSpeed * WalkingSpeed;
            }

            if (MovementType == MovementType.Run)
            {
                PlayerState = PlayerMovementState.Running;
                ModifiedSpeed = DefaultSpeed * RunningSpeed;
            }
        }
        else
        {
            PlayerState = PlayerMovementState.Idle;
        }
    }

    private void PositionMinimap()
    {
        Player.MinimapCameraTransform.position = Player.transform.position + MinimapPosOffset;
        Player.MinimapCameraTransform.eulerAngles = MinimapRotation;
    }

    public void FixedUpdate()
    {
        if (StoryMode) { return; }

        JoyStickInput = new Vector2(Player.PlayerJoyStick.Horizontal, Player.PlayerJoyStick.Vertical);

        Vector3 movementDirection = GetMovementDirection();

        if (JoyStickInput == Vector2.zero)
        {
            RotateTowardsTargetDirection();
            return;
        }

        float targetRotationAngle = Rotate(movementDirection);
        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationAngle);
        float movementSpeed = ModifiedSpeed;
        Vector3 currentPlayerVelocity = GetPlayerHorizontalVelocity();

        Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerVelocity, ForceMode.VelocityChange);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 movementDirection = new Vector3(JoyStickInput.x, 0f, JoyStickInput.y);
        movementDirection.Normalize();
        return movementDirection;
    }

    private float Rotate(Vector3 direction)
    {
        float DirectionAngle = UpdateTargetRotation(direction);
        RotateTowardsTargetDirection();
        return DirectionAngle;
    }

    private float UpdateTargetRotation(Vector3 direction, bool RotateCamera = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (RotateCamera)
        {
            directionAngle = AddCameraRotationAngle(directionAngle);
        }

        if (directionAngle != currentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
        }

        return directionAngle;
    }

    private float AddCameraRotationAngle(float angle)
    {
        angle += Player.MainCameraTransform.eulerAngles.y;

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        currentTargetRotation.y = targetAngle;
        dampedRotationTime.y = 0f;
    }

    private void RotateTowardsTargetDirection()
    {
        float currentYAngle = Player.Rigidbody.rotation.eulerAngles.y;

        if (currentYAngle == currentTargetRotation.y)
        {
            return;
        }

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation.y, ref dampedRotationVelocity.y, rotationTime.y - dampedRotationTime.y);

        dampedRotationTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

        Player.Rigidbody.MoveRotation(targetRotation);
    }

    private Vector3 GetTargetRotationDirection(float targetRotationAngle)
    {
        return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
    }

    private Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = Player.Rigidbody.velocity;

        playerHorizontalVelocity.y = 0f;

        return playerHorizontalVelocity;
    }
}

public enum PlayerMovementState { Idle, Walking, Running }
public enum MovementType { Walk, Run }

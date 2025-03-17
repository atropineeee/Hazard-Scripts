using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Usefull Player Components")]
    [field: SerializeField] public Rigidbody Rigidbody;
    [field: SerializeField] public CapsuleCollider CapCollider;
    [field: SerializeField] public Animator Animator;

    [field: Header("Camera Player Components")]
    [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; private set; }
    [field: SerializeField] public Transform MainCameraTransform { get; private set; }
    [field: SerializeField] public Transform MinimapCameraTransform { get; private set; }
    [field: SerializeField] public Camera MainCamera { get; private set; }

    [field: Header("Other Player Scripts")]
    [field: SerializeField] public MovePlayer MovePlayer;
    [field: SerializeField] public AnimatePlayer AnimatePlayer;
    [field: SerializeField] public MoveCamera MoveCamera;
    [field: SerializeField] public RainMov MoveRain;

    [field: Header("JoyStick Script")]

    [field: SerializeField] public VariableJoystick PlayerJoyStick;
    private bool Started = false;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        CapCollider = GetComponent<CapsuleCollider>();
        Animator = GetComponent<Animator>();

        VirtualCamera = GameObject.Find("PVCam").GetComponent<CinemachineVirtualCamera>();
        MainCameraTransform = GameObject.Find("PMCam").GetComponent<Transform>();
        MainCamera = GameObject.Find("PMCam").GetComponent<Camera>();
        MinimapCameraTransform = GameObject.Find("MMCam").GetComponent<Transform>();
        PlayerJoyStick = GameObject.Find("PlayerJoyStick").GetComponent<VariableJoystick>();

        MovePlayer = new MovePlayer(this);
        AnimatePlayer = new AnimatePlayer(this);
        MoveCamera = new MoveCamera(this);
        MoveRain = new RainMov(this);

        StartOtherScripts();
    }


    private void Awake()
    {
        
    }

    private void StartOtherScripts()
    {
        MovePlayer.Start();
        MoveCamera.Start();
        StartCoroutine(StartPlayer());
    }

    private void Update()
    {
        if (!Started) { return; }
        MovePlayer.Update();
        AnimatePlayer.Update();
        MoveCamera.Update();
    }

    private void FixedUpdate()
    {
        if (!Started) { return; }
        MovePlayer.FixedUpdate();
    }

    IEnumerator StartPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        Started = true;
        Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Animator.Rebind();
    }
}

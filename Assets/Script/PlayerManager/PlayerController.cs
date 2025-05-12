using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float smoothSpeed = 5.0f;
    public float lookXLimit = 45.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private AudioSource footstepAudioSource;

    [HideInInspector]
    public bool canMove = true;

    private float targetRotationY;

    private AudioSource bangunAudioSource;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        footstepAudioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        if (canMove)
        {
            HandleMovement();
            HandleRotation();
            HandleFootstepSound();
        }
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal");

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        Quaternion cameraTargetRotation = Quaternion.Euler(rotationX, 0, 0);
        playerCamera.transform.localRotation = Quaternion.Slerp(playerCamera.transform.localRotation, cameraTargetRotation, Time.deltaTime * smoothSpeed);

        targetRotationY += Input.GetAxis("Mouse X") * lookSpeed;
        Quaternion playerTargetRotation = Quaternion.Euler(0, targetRotationY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerTargetRotation, Time.deltaTime * smoothSpeed);
    }

    //gatau mau pake apa ngga
    private void HandleFootstepSound()
    {
        if ((moveDirection.x != 0 || moveDirection.z != 0) && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
        else if (moveDirection.x == 0 && moveDirection.z == 0)
        {
            footstepAudioSource.Stop();
        }
    }
}

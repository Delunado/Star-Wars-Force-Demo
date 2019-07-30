using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    //Different movements speed
    public float sprintSpeed = 10f;
    public float normalMoveSpeed;
    public float crouchSpeed = 2f;

    //Camera/Look
    private Transform lookRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;

    //Crouching
    private bool isCrouching;

    //Footsteps
    private PlayerFootsteps playerFootSteps;

    private float sprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float walkVolumeMin = 0.2f;
    private float walkVolumeMax = 0.6f;

    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerFootSteps = GetComponentInChildren<PlayerFootsteps>();

        //It takes the first children in hierarchy
        lookRoot = transform.GetChild(0);
    }

    private void Start()
    {
        normalMoveSpeed = playerMovement.Speed;

        MovementAudioSettings(walkStepDistance, walkVolumeMin, walkVolumeMin);
    }

    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if (!isCrouching)
        {
            if (Input.GetButtonDown(ActionsTags.SPRINT))
            {
                playerMovement.Speed = sprintSpeed;

                MovementAudioSettings(sprintStepDistance, sprintVolume, sprintVolume);
            }

            if (Input.GetButtonUp(ActionsTags.SPRINT))
            {
                playerMovement.Speed = normalMoveSpeed;

                MovementAudioSettings(walkStepDistance, walkVolumeMin, walkVolumeMin);
            }
        }
    }

    void Crouch()
    {
        if (Input.GetButtonDown(ActionsTags.CROUCH))
        {
            //If we are crouching, we stand up
            if (isCrouching)
            {
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.Speed = normalMoveSpeed;

                MovementAudioSettings(crouchStepDistance, crouchVolume, crouchVolume);

                isCrouching = false;
            } else //If we are standing up, we start to crouch 
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.Speed = crouchSpeed;

                MovementAudioSettings(walkStepDistance, walkVolumeMin, walkVolumeMin);

                isCrouching = true;
            }
        }
    }

    private void MovementAudioSettings(float stepDistance, float volumeMin, float volumeMax)
    {
        playerFootSteps.StepDistance = stepDistance;
        playerFootSteps.VolumeMin = volumeMin;
        playerFootSteps.VolumeMax = volumeMax;
    }
}

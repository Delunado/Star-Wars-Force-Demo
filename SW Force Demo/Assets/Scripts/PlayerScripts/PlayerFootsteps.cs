using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    //Audio
    private AudioSource footstepSound;
    [SerializeField] private AudioClip[] footstepClips;

    private float volumeMin;
    public float VolumeMin { get => volumeMin; set => volumeMin = value; }

    private float volumeMax;
    public float VolumeMax { get => volumeMax; set => volumeMax = value; }

    //Character
    private CharacterController characterController;

    private float accumulatedDistance;

    private float stepDistance;
    public float StepDistance { get => stepDistance; set => stepDistance = value; }

    void Awake()
    {
        footstepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if (!characterController.isGrounded)
            return;

        //If the character is moving and touching the ground.
        if (characterController.velocity.sqrMagnitude > 0)
        {
            //The accumulated distance is how far can you walk until a step sounds.
            accumulatedDistance += Time.deltaTime;

            if (accumulatedDistance > StepDistance)
            {
                footstepSound.volume = Random.Range(VolumeMin, VolumeMax);
                footstepSound.clip = footstepClips[Random.Range(0, footstepClips.Length)];
                footstepSound.Play();

                accumulatedDistance = 0f;
            }
        } else
        {
            accumulatedDistance = 0f;
        }
    }
}

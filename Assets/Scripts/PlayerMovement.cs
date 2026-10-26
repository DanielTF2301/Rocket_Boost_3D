using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotate;

    [Header("Player Movement Forces")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 2.5f;

    [Header("Player Movement Particle Systems")]
    [SerializeField] private ParticleSystem thrustParticleSystem;
    [SerializeField] private ParticleSystem leftThrustParticleSystem;
    [SerializeField] private ParticleSystem rightThrustParticleSystem;

    [Header("Music settings")]
    [SerializeField] private AudioClip thrustAudioSource;
    [SerializeField] private AudioClip leftThrustAudioSource;
    [SerializeField] private AudioClip rightThrustAudioSource;
    Rigidbody rb;
    AudioSource audioSource;

    void OnEnable()
    {
        thrust.Enable();
        rotate.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate

        float rotationValue = rotate.ReadValue<float>();

        if (rotationValue < 0)
        {
            RotateRight();
        }
        else if (rotationValue > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopRotating()
    {
        leftThrustParticleSystem.Stop();
        rightThrustParticleSystem.Stop();
    }

    private void RotateLeft()
    {
        transform.Rotate(Vector3.back * rotationForce);
        if (!leftThrustParticleSystem.isPlaying)
        {
            leftThrustParticleSystem.Play();
            rightThrustParticleSystem.Stop();
        }
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.forward * rotationForce);
        if (!rightThrustParticleSystem.isPlaying)
        {
            rightThrustParticleSystem.Play();
            leftThrustParticleSystem.Stop();
        }
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        //TODO: Add audio source for thrusting
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudioSource);
        }
        rb.AddRelativeForce(Vector3.up * moveForce);

        if (!thrustParticleSystem.isPlaying)
        {
            thrustParticleSystem.Play();
        }
    }

    private void StopThrusting()
    {
        thrustParticleSystem.Stop();
        audioSource.Stop();
    }
}

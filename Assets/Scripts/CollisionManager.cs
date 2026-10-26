using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField] private AudioClip explosionAudioSource;
    [SerializeField] private AudioClip landingAudioSource;

    [Header("Collision Particle Systems")]
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private ParticleSystem landingParticleSystem;

    AudioSource audioSource;

    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) return;

        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("Collided with LaunchPad");
                break;
            case "LandingPad":
                ManagePadCollision(landingAudioSource);
                break;
            case "Explosion":
                ManagePadCollision(explosionAudioSource);
                break;
            default:
                break;
        }
    }

    private void ManagePadCollision(AudioClip audioClip)
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
        GetComponent<PlayerMovement>().enabled = false;

    }
}

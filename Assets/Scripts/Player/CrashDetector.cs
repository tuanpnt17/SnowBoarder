using UnityEngine;

public class CrashDetectopr : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _crashEffect;

    [SerializeField]
    private AudioClip _crashSFX;

    bool hasCrashed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" && !hasCrashed)
        {
            hasCrashed = true;
            FindFirstObjectByType<PlayerController>().DisableControls();
            _crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(_crashSFX);
            GameManager.Instance.HandleCrash();
        }
    }
}

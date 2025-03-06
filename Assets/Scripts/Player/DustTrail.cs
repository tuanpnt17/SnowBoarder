using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _dustParticles;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _dustParticles.transform.position = other.contacts[0].point;
            _dustParticles.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _dustParticles.Stop();
        }
    }
}

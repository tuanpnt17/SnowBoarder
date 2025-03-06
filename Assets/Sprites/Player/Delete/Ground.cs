using UnityEngine;

public class Ground : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.SetSpeedMultiplier(0.7f); // 30% slower
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.SetSpeedMultiplier(1f); // Reset to normal speed
        }
    }
}

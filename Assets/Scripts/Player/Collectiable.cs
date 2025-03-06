using UnityEngine;

public class Collectiable : MonoBehaviour
{
    [SerializeField]
    private GameObject _helmetPrefab;

    [SerializeField]
    private float _helmetDuration = 10f;

    [SerializeField]
    private AudioClip _collectSound;

    private Vector3 _helmetLocalPos = new Vector3(1.609f, 0.937f, 0f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GetComponent<AudioSource>().PlayOneShot(_collectSound);
            GameManager.Instance.CollectCoin(other.transform.position);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("HelmetBonus"))
        {
            EquipHelmet();
            Destroy(other.gameObject);
        }
    }

    private void EquipHelmet()
    {
        Vector3 helmetWorldPos = transform.TransformPoint(_helmetLocalPos);
        GameObject helmet = Instantiate(_helmetPrefab, helmetWorldPos, transform.rotation);
        helmet.transform.parent = transform;
        Destroy(helmet, _helmetDuration);
    }
}

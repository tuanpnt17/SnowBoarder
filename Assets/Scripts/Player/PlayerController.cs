using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _torqueAmount = 20f;

    [SerializeField]
    private float _boostSpeed = 50f;

    [SerializeField]
    private float _baseSpeed = 20f;

    [SerializeField]
    private float _speedMultiplier = 1f;

    private Rigidbody2D _rb2d;
    private SurfaceEffector2D _surfaceEffector2D;
    private bool _canMove = true;
    private Vector2 _previousRight;
    private float _angle;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        _previousRight = transform.right;
    }

    private void Update()
    {
        if (_canMove)
        {
            RotatePlayer();
            RespondToBoost();
            CheckRotationForPoints();
        }
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Wind"))
    //    {
    //        SetSpeedMultiplier(0.7f);
    //    }
    //    else if (other.gameObject.CompareTag("Snow"))
    //    {
    //        SetSpeedMultiplier(0.8f);
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Wind") || other.gameObject.CompareTag("Snow"))
    //    {
    //        SetSpeedMultiplier(1f);
    //    }
    //}

    public void DisableControls()
    {
        _canMove = false;
    }

    public void EnableControls()
    {
        _canMove = true;
    }

    public void SetSpeedMultiplier(float newMultiplier)
    {
        _speedMultiplier = newMultiplier;
    }

    private void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _surfaceEffector2D.speed = _boostSpeed * _speedMultiplier;
        }
        else
        {
            _surfaceEffector2D.speed = _baseSpeed * _speedMultiplier;
        }
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb2d.AddTorque(_torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb2d.AddTorque(-_torqueAmount);
        }
    }

    private void CheckRotationForPoints()
    {
        Vector2 currentRight = transform.right;
        _angle += Vector2.SignedAngle(_previousRight, currentRight);
        _previousRight = currentRight;
        if (Mathf.Abs(_angle) >= 360f)
        {
            GameManager.Instance.CompleteRotation(transform.position + new Vector3(1.6f, 2.6f, 0));
            _angle -= 360f * Mathf.Sign(_angle);
        }
    }
}

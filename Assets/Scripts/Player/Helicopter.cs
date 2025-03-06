using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float _dropDelay = 1.5f;

    [SerializeField]
    private float _moveSpeed = 5f;

    private Vector3 _dropPosition;
    private bool _isMoving = true;
    private bool _hasDropped = false;
    private Rigidbody2D _playerRg2d;
    private float _originGravityScale;

    private void Start()
    {
        _playerRg2d = _player.GetComponent<Rigidbody2D>();
        _originGravityScale = _playerRg2d.gravityScale;
        _playerRg2d.gravityScale = 0;
        _playerRg2d.GetComponent<PlayerController>().DisableControls();

        if (GameManager.Instance.skipStart)
        {
            _isMoving = false;
            DropPlayer();
        }
        else
        {
            _dropPosition = transform.position;
            transform.position += new Vector3(-15, 5, 0);
        }
    }

    private void Update()
    {
        if (_isMoving)
        {
            MoveToDropPosition();
        }
    }

    void MoveToDropPosition()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _dropPosition,
            _moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, _dropPosition) < 0.1f && !_hasDropped)
        {
            _isMoving = false;
            Invoke(nameof(DropPlayer), _dropDelay);
        }
    }

    void DropPlayer()
    {
        _hasDropped = true;
        _player.transform.parent = null;
        _playerRg2d.gravityScale = _originGravityScale;
        _playerRg2d.GetComponent<PlayerController>().EnableControls();
    }
}

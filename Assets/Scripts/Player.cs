using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField]
    Damageable damageable;

    [SerializeField]
    Transform spriteTransform;

    [SerializeField]
    AnimationHandler _animationHandler;

    [SerializeField]
    Rigidbody2D _rigidbody;

    [SerializeField]
    float _jumpForce;

    [SerializeField]
    PlayerState _playerState;

    [SerializeField]
    bool _isOnTheGround;

    [SerializeField]
    Transform _resourcePlaceHolder;

    [SerializeField]
    GameResourceUnit _carried_resource;

    [SerializeField]
    GameResourceUnit _colliding_resource;

    public int Health { get => damageable.GetHealth(); }

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _isOnTheGround = true;
        _playerState = PlayerState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && _playerState != PlayerState.DEAD)
        {
            _playerState = PlayerState.DEAD;
            _animationHandler.SetAnimationState((int)_playerState);
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl) && _isOnTheGround && !_carried_resource)
        {
            _playerState = PlayerState.BUILD;
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && _isOnTheGround && !_carried_resource)
        {
            _playerState = PlayerState.CHOP;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && _isOnTheGround && _playerState == PlayerState.BUILD)
        {
            _playerState = PlayerState.IDLE;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) && _isOnTheGround && _playerState == PlayerState.CHOP)
        {
            _playerState = PlayerState.IDLE;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && (_playerState != PlayerState.BUILD && _playerState != PlayerState.CHOP) && _colliding_resource)
        {
            _playerState = PlayerState.IDLE_CARRYING;
            _animationHandler.SetAnimationState((int)_playerState);
            _carried_resource = _colliding_resource;
            _carried_resource.gameObject.transform.parent = transform;
            _carried_resource.gameObject.transform.localPosition = _resourcePlaceHolder.localPosition;
            _colliding_resource = null;
        }

        if (Input.GetKey(KeyCode.RightArrow) && (_playerState != PlayerState.BUILD && _playerState != PlayerState.CHOP))
        {
            if (_carried_resource)
                _playerState = PlayerState.RUN_CARRYING;
            else
                _playerState = PlayerState.RUN;
            transform.position += Vector3.right * Time.deltaTime;
            spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && (_playerState != PlayerState.BUILD && _playerState != PlayerState.CHOP))
        {
            if (_carried_resource)
                _playerState = PlayerState.RUN_CARRYING;
            else
                _playerState = PlayerState.RUN;
            transform.position += Vector3.left * Time.deltaTime;
            spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_playerState != PlayerState.BUILD && _playerState != PlayerState.CHOP)
        {
            if (_carried_resource)
                _playerState = PlayerState.IDLE_CARRYING;
            else
                _playerState = PlayerState.IDLE;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _isOnTheGround && (_playerState != PlayerState.BUILD && _playerState != PlayerState.CHOP))
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _isOnTheGround = false;
        }


        _animationHandler.SetAnimationState((int)_playerState);
    }

    public void DealDamage(int damagePoints)
    {
        damageable.DealDamage(damagePoints);
    }

    public GameResourceUnit GetCarriedResource()
    {
        if (!_carried_resource)
            return null;

        if (_playerState == PlayerState.IDLE_CARRYING)
            _playerState = PlayerState.IDLE;
        else if (_playerState == PlayerState.RUN_CARRYING)
            _playerState = PlayerState.RUN;
        _animationHandler.SetAnimationState((int)_playerState);

        GameResourceUnit resource = _carried_resource;
        _carried_resource = null;

        return resource;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            _isOnTheGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("resource_unit") && !_colliding_resource)
        {
            if (collision.GetComponent<GameResourceUnit>().isOnTheGround)
            {
                _colliding_resource = collision.GetComponent<GameResourceUnit>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("resource_unit") && _colliding_resource)
        {
            _colliding_resource = null;
        }
    }
}

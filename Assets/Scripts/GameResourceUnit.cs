using UnityEngine;

public class GameResourceUnit : MonoBehaviour
{
    public GameResourceType type;
    public int value;
    public bool isOnTheGround = false;

    [SerializeField]
    Rigidbody2D _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the ground
        if (collision.gameObject.CompareTag("ground") && !isOnTheGround)
        {
            // Stop the movement
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            isOnTheGround = true;
        }
    }
}

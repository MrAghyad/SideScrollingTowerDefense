using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AIState
{
    SPAWNING,
    IDLE,
    RUN,
    ATTACK,
    DEAD,
}

public class Archer : MonoBehaviour
{
    public EnemyCastle enemyCastle;

    public LayerMask raycastMask;

    public Arrow arrowPrefab;

    [SerializeField]
    Damageable damageable;

    [SerializeField]
    Transform sightEnd;

    [SerializeField]
    Transform arrowSpawnPoint;

    [SerializeField]
    float speed = 5;

    [SerializeField]
    AnimationHandler animationHandler;

    [SerializeField]
    AIState _state;

    public int Health { get => damageable.GetHealth(); }

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        _state = AIState.SPAWNING;
    }

    public void SetEnemyCastle(EnemyCastle targettedEnemyCastle)
    {
        enemyCastle = targettedEnemyCastle;
    }    

    public void SetState(AIState newState)
    {
        _state = newState;
        animationHandler.SetAnimationState((int)_state);
    }

    private void Update()
    {
        if (Health <= 0 && _state != AIState.DEAD)
        {
            SetState(AIState.DEAD);
            return;
        }

        if (_state == AIState.RUN && enemyCastle && enemyCastle.Health > 0)
        {
            Vector3 targetPosition = new Vector3(enemyCastle.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else if ((enemyCastle && enemyCastle.Health <= 0) || (!enemyCastle))
        {
            _state = AIState.IDLE;
            animationHandler.SetAnimationState((int)_state);
        }

        // Calculate direction from the current object to the sight_end object
        Vector2 direction = sightEnd.position - transform.position;

        // Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, raycastMask);
        if (hit.collider != null)
        {
            if((hit.collider.CompareTag("enemy") || hit.collider.CompareTag("enemy_castle")))
            {
                // Draw a red line if hit
                Debug.DrawLine(transform.position, hit.point, Color.red);

                _state = AIState.ATTACK;
                animationHandler.SetAnimationState((int)_state);
            }
            else
            {
                _state = AIState.RUN;
                animationHandler.SetAnimationState((int)_state);
            }
        }
        else
        {
            // Draw a green line if no hit
            Debug.DrawLine(transform.position, transform.position + (Vector3)direction, Color.green);
            if(_state == AIState.ATTACK)
            {
                _state = AIState.RUN;
                animationHandler.SetAnimationState((int)_state);
            }
        }
    }

    public void DealDamage(int damagePoints)
    {
        damageable.DealDamage(damagePoints);
    }

    public void Attack()
    {
        Arrow createdArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity).GetComponent<Arrow>();
        createdArrow.SetEndPoint(sightEnd.position.x);
    }

}

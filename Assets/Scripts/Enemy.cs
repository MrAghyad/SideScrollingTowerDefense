using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public PlayerCastle playerCastle;

    [SerializeField]
    AnimationHandler animationHandler;

    [SerializeField]
    Transform spriteTransform;

    [SerializeField]
    AIState _state;

    [SerializeField]
    float speed = 5;

    [SerializeField]
    Damageable damageable;

    public int Health { get => damageable.GetHealth(); }


    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }

   private void Start()
    {
        _state = AIState.SPAWNING;
    }

    public void SetDirection(Vector3 direction)
    {
        spriteTransform.localScale = direction;
    }

    public void SetPlayerCastle(PlayerCastle playerCastleRef)
    {
        playerCastle = playerCastleRef;
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


        if (_state == AIState.RUN && playerCastle && playerCastle.Health > 0)
        {
            Vector3 targetPosition = new Vector3(playerCastle.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else if ((playerCastle && playerCastle.Health <= 0) || (!playerCastle))
        {
            _state = AIState.IDLE;
            animationHandler.SetAnimationState((int)_state);
        }
    }
    public void DealDamage(int damagePoints)
    {
        damageable.DealDamage(damagePoints);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.transform.position.x < transform.position.x)
            {
                SetState(AIState.ATTACK);
            }
        }
        else if (collision.gameObject.CompareTag("Archer"))
        {
            SetState(AIState.ATTACK);
        }

        else if (collision.gameObject.CompareTag("player_castle"))
        {
            SetState(AIState.ATTACK);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetState(AIState.RUN);
        }
        else if (collision.gameObject.CompareTag("Archer"))
        {
            SetState(AIState.RUN);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameResource : MonoBehaviour
{
    public Damageable damageable;
    public int value;
    public GameResourceUnit gameResourceUnitPrefab;
    private Animator _animator;

    public float spawnForce = 3f; // force applied to spawned objects

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayChopAnimation()
    {
        _animator.SetBool("isBeingChopped", true);
    }

    public void PlayIdleAnimation()
    {
        _animator.SetBool("isBeingChopped", false);
    }

    public int Health
    {
        get => damageable.GetHealth();
    }

    public void DealDamage(int damagePoints, DamageDirection damageDirection)
    {

        PlayChopAnimation();

        damageable.DealDamage(damagePoints);

        if (Health <= 0)
        {
            SpawnResources(damageDirection);
            Destroy(gameObject); // Optionally destroy the resource object
        }
    }

    private void SpawnResources(DamageDirection damageDirection)
    {
        GameResourceUnit spawnedResource = Instantiate(gameResourceUnitPrefab, transform.position, Quaternion.identity);
        spawnedResource.enabled = false;

        spawnedResource.value = value;
        float spawn_pos_x = 0.2f;
        if (damageDirection == DamageDirection.LEFT)
        {
            spawn_pos_x = -0.2f;
        }

        Vector2 spawnDirection = new Vector2(spawn_pos_x, 0.2f).normalized; // Random horizontal direction, always upward vertically
        spawnedResource.GetComponent<Rigidbody2D>().AddForce(spawnDirection * spawnForce, ForceMode2D.Impulse);

        spawnedResource.enabled = true;
    }
}
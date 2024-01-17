using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    int _health;

    [SerializeField]
    float maxHealth = 50f;

    private void Awake()
    {
        _health = (int)maxHealth;
        healthBar.value = _health / maxHealth;
    }

    public void DealDamage(int damagePoints)
    {
        _health = Mathf.Max(0, _health - damagePoints);

        healthBar.value = _health / maxHealth;
    }

    public void Heal(int healingPoints)
    {
        _health = Mathf.Min((int)maxHealth, _health + healingPoints);

        healthBar.value = _health / maxHealth;
    }

    public void OnDestroyStart()
    {
        Destroy(healthBar);
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D)
        {
            Destroy(rigidbody2D);
        }

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        if(boxCollider2D)
        {
            Destroy(boxCollider2D);
        }
           
    }

    public int GetHealth()
    {
        return _health;
    }

    public void OnDestroyEnd()
    {
        gameObject.SetActive(false);
    }
}

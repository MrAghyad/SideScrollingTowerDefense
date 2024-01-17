using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damagePoints;

    [SerializeField]
    float endPoint;


    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime;

        if(transform.position.x >= endPoint)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("entered enemy");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.DealDamage(damagePoints);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("enemy_castle"))
        {
            EnemyCastle enemyCastle = collision.gameObject.GetComponent<EnemyCastle>();
            if (enemyCastle)
            {
                enemyCastle.DealDamage(damagePoints);
            }
            Destroy(gameObject);
        }
    }

    public void SetEndPoint(float point)
    {
        endPoint = point;
    }
}

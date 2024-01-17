using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    PlayerCastle playerCastle;

    [SerializeField]
    Damageable damageable;


    public int Health { get => damageable.GetHealth(); }

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }

    void Start()
    {
        StartCoroutine(SpwanEnemies());
    }

    public void DealDamage(int damagePoints)
    {
        damageable.DealDamage(damagePoints);
    }



    private IEnumerator SpwanEnemies()
    {
        while (playerCastle && playerCastle.Health > 0)
        {
            yield return new WaitForSeconds(Random.Range(10f, 20f));


            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            Enemy enemy = enemyObj.GetComponentInChildren<Enemy>();
            enemy.SetDirection(spawnPoint.localScale);
            enemy.SetPlayerCastle(playerCastle);
        }
        
    }
}

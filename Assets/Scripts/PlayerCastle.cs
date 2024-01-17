using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerCastle : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    ArmyResources[] armyResources;

    [SerializeField]
    EnemyCastle enemyCastle;

    [SerializeField]
    int meatCount;

    [SerializeField]
    int woodCount;

    [SerializeField]
    Damageable damageable;

    public int Health { get => damageable.GetHealth(); }

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }

    public void Heal(int healingPoints)
    {
        damageable.Heal(healingPoints);
    }

    public void DealDamage(int damagePoints)
    {
        damageable.DealDamage(damagePoints);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && armyResources.Length > 0 && armyResources.Any(army => army.armyType == ArmyType.ARCHER))
        {
            CreateArmyman(ArmyType.ARCHER);
        }
    }

    private void CreateArmyman(ArmyType armyType)
    {
        ArmyResources armyResource = armyResources.First(army => army.armyType == armyType);
        if (armyResource.requiredMeat <= meatCount && armyResource.requiredWood <= woodCount)
        {
            GameObject prefab = armyResource.prefab;
            GameObject newArcherObj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            Archer archer = newArcherObj.GetComponentInChildren<Archer>();
            archer.SetEnemyCastle(enemyCastle);
        }
        else
        {
            Debug.Log($"Cannot create {armyType.ToString()}, no enough resources");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player)
            {
                GameResourceUnit resource = player.GetCarriedResource();
                if(resource)
                {
                    if (resource.type == GameResourceType.MEAT)
                        meatCount += resource.value;
                    else if (resource.type == GameResourceType.WOOD)
                        woodCount += resource.value;

                    Destroy(resource.gameObject);
                }
            }    
        }
        
    }
}

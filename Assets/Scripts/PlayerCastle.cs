using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCastle : MonoBehaviour
{
    [SerializeField]
    int meatCount;

    [SerializeField]
    int woodCount;


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

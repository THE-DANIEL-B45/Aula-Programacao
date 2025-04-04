using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public SOWeapon properties;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerControllerAulaStateMachine player = collision.gameObject.GetComponent<PlayerControllerAulaStateMachine>();
            player.GiveWeapon(properties);


            Destroy(gameObject);
        }
    }
}

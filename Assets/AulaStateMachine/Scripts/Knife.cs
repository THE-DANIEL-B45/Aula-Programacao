using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControllerAulaStateMachine>().canAttack = true;
            Destroy(gameObject);
        }
    }
}

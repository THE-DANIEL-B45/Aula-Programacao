using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakAttackHitboxAulaStateMachine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();

        if(enemy != null )
        {
            if(enemy.currentState == EnemyController.EnemyState.Patrol)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

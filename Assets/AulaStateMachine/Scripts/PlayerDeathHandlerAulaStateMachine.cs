using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandlerAulaStateMachine : MonoBehaviour
{
    public float restartDelay = 1.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if(enemy != null)
        {
            enemy.currentState = EnemyController.EnemyState.Patrol;

            StartCoroutine(RestartGameAfterDelay());
        }
    }

    IEnumerator RestartGameAfterDelay()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Default");

        Collider2D collider = GetComponent<Collider2D>();

        if(collider != null)
        {
            collider.enabled = false;
        }

        PlayerController playerController = GetComponent<PlayerController>();
        if(playerController != null)
        {
            playerController.enabled = false;
        }

        yield return new WaitForSeconds(restartDelay);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

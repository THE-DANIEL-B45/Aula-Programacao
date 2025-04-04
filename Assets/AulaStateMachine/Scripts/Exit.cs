using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject gameOverPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerControllerAulaStateMachine player = collision.GetComponent<PlayerControllerAulaStateMachine>();

            if (player == null || player.keys != 3 || player.enemiesDefeated != 12) return;

            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }
    }

    public void PlayAgainButton()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

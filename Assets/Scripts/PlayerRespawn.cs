using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerRespawn : MonoBehaviour
{
    public float threshold = -3f;

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                ResetScene();
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Time.timeScale = 1f;

                SceneManager.LoadScene(1);
            }
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            ResetScene();
        }
    }

    private void ResetScene()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
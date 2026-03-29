using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Fall Settings")]
    [Tooltip("The Y position the player must fall below to die.")]
    public float threshold = -3f;

    [Header("UI Elements")]
    [Tooltip("Drag your FallLose pop-up panel here")]
    public GameObject fallLoseUI;

    private bool hasFallen = false;

    void Start()
    {
        if (fallLoseUI != null)
        {
            fallLoseUI.SetActive(false);
        }
    }

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
        if (!hasFallen && transform.position.y < threshold)
        {
            PlayerFell();
        }
    }

    private void PlayerFell()
    {
        hasFallen = true;

        if (fallLoseUI != null)
        {
            fallLoseUI.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    private void ResetScene()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
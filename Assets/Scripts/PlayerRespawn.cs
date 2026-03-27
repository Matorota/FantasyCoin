using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; 

public class PlayerRespawn : MonoBehaviour
{
    [Tooltip("The Y position the player must fall below to restart the level.")]
    public float threshold = -3f;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetScene();
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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
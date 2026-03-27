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
        // Check if the player has fallen below the threshold
        if (transform.position.y < threshold)
        {
            ResetScene();
        }
    }

    // A helper method to easily reload the scene from anywhere
    private void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
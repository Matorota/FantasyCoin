using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class GameEnd : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("Game Ended!");

        // Stop time (freeze game)
        Time.timeScale = 0f;

        // You can also:
        // Show win UI
        // Load next level
        // Play animation
    }
}
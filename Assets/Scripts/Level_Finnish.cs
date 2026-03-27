using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class GameEnd : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("Game Ended!");

        Time.timeScale = 0f;
    }
}
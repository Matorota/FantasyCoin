using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMeniuManager : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene("MainMeniu");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Explicitly stating UnityEngine bypasses any confusing namespace errors!
        UnityEngine.Application.Quit();
    }
}
using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class MainMeniuManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
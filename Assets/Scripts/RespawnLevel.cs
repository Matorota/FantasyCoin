using UnityEngine;
using UnityEngine.SceneManagement; // Add this line

public class RespawnLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene(4);
    }
}

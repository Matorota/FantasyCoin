using UnityEngine;

public class WinPopUp : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Drag your WinPopUp panel here")]
    public GameObject winPopUpUI;

    void Start()
    {
        if (winPopUpUI != null)
        {
            winPopUpUI.SetActive(false);
        }
    }

    public void LevelComplete()
    {
        if (winPopUpUI != null)
        {
            winPopUpUI.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}
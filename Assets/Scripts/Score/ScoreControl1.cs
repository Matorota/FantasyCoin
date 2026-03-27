using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] GameObject scoreBox;

    [SerializeField] WinPopUp winPopUpDialog;

    public static int coinsLeft = 0;
    private static int totalCoins = 0;
    private bool gameEnded = false;

    void Start()
    {
        totalCoins = FindObjectsOfType<CoinControl>().Length;
        coinsLeft = totalCoins;
    }

    void Update()
    {
        int coinsCollected = totalCoins - coinsLeft;

        scoreBox.GetComponent<TMP_Text>().text =
            "Score: " + coinsCollected + " / " + totalCoins;

        if (coinsLeft <= 0 && !gameEnded)
        {
            gameEnded = true;

            if (winPopUpDialog != null)
            {
                winPopUpDialog.LevelComplete();
            }
            else
            {
                UnityEngine.Debug.LogWarning("Remember to assign the WinPopUp script in the Inspector!");
            }
        }
    }
}
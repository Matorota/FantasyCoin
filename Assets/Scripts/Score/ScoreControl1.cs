using UnityEngine;
using TMPro;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] GameObject scoreBox;
    [SerializeField] GameEnd gameEnd;

    public static int coinsLeft = 0;
    private static int totalCoins = 0;

    void Start()
    {
        totalCoins = FindObjectsOfType<CoinControl>().Length;
        coinsLeft = totalCoins;
    }

    bool gameEnded = false;

    void Update()
    {
        int coinsCollected = totalCoins - coinsLeft;

        // \n creates a new line under the score
        scoreBox.GetComponent<TMP_Text>().text =
            "Score: " + coinsCollected + " / " + totalCoins + "\nPress R to restart game";

        if (coinsLeft <= 0 && !gameEnded)
        {
            gameEnded = true;
            gameEnd.EndGame();
        }
    }
}
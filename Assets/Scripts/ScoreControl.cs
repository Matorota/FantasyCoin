using UnityEngine;
using TMPro;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] GameObject scoreBox;

    public static int coinsLeft = 0;
    private static int totalCoins = 0;

    void Start()
    {
        totalCoins = FindObjectsOfType<CoinControl>().Length;
        coinsLeft = totalCoins;
    }

    void Update()
    {
        int coinsCollected = totalCoins - coinsLeft;
        scoreBox.GetComponent<TMP_Text>().text = "Score: " + coinsCollected + " / " + totalCoins;
    }
}
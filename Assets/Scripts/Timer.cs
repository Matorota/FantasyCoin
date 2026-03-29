using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Timer Setup")]
    [SerializeField] GameObject timeBox;
    [SerializeField] int timeLeft = 30;
    [SerializeField] bool takingSecond = false;

    [Header("Lose pop-up")]
    [Tooltip("Drag your Times Up panel here")]
    public GameObject timesUpUI;

    private bool gameEnded = false;

    void Start()
    {
        if (timesUpUI != null)
        {
            timesUpUI.SetActive(false);
        }
    }

    void Update()
    {
        if (gameEnded) return;

        timeBox.GetComponent<TMPro.TMP_Text>().text = "TIME LEFT: " + timeLeft;

        if (timeLeft <= 0)
        {
            TimeRanOut();
        }
        else if (takingSecond == false)
        {
            StartCoroutine(RemoveSecond());
        }
    }

    IEnumerator RemoveSecond()
    {
        takingSecond = true;
        yield return new WaitForSeconds(1);
        timeLeft -= 1;
        takingSecond = false;
    }

    private void TimeRanOut()
    {
        gameEnded = true;

        if (timesUpUI != null)
        {
            timesUpUI.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}
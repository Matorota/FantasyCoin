using UnityEngine;

public class CoinControl : MonoBehaviour
{
    [SerializeField] int rotationSpeed = 2;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        ScoreControl.coinsLeft -= 1;
        Destroy(gameObject);
    }
}
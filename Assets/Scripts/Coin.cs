using UnityEngine;

public class Coin : CoinPickup
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float rotationAmount = 100f;
    ScoreManager scoreManager;
    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationAmount * Time.deltaTime, 0f);
    }

    protected override void OnPickUp()
    {
        scoreManager.IncreaseScore(100);
    }


}



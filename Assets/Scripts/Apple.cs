using UnityEngine;

public class Apple : CoinPickup
{
    ChunkGeneration chunkGeneration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chunkGeneration = FindFirstObjectByType<ChunkGeneration>();
    }
    protected override void OnPickUp()
    {
        chunkGeneration.SpeedUpLevel(0.1f);
        Destroy(gameObject);
        Debug.Log("Power up!");
    }
}
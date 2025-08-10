using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework.Constraints;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] int numberOfStartingChunks = 12;
    [SerializeField] float heightOfFence = 10f;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] Transform player;
    [SerializeField] GameObject fence;
    [SerializeField] float fenceSpawnChance = 0.5f;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject apple;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float heightOfApple = 1f;
    [SerializeField] float minimumTranslationSpeed = 0.2f;
    [SerializeField] float maximumTranslationSpeed = 3f;
    [SerializeField] ParticleSystem particleSystem;
    SpeedUpCinemachineCamera speedUpCinemachineCamera;
    float chunkLength = 10f;
    [SerializeField] float translationSpeed = 0.5f;
    float heightOfCoin = 1f;

    
    bool coinsSpawned = false;
    //int index = 0;
    List<float> lanes = new List<float> { -2.5f, 0, 2.5f };
    List<float> emptyLanes = new List<float> { -2.5f, 0f, 2.5f};
    List<GameObject> chunkPrefabs = new List<GameObject>();

    void Start()
    {
        GenerateChunks();
        speedUpCinemachineCamera = FindFirstObjectByType<SpeedUpCinemachineCamera>();
        // MoveChunks();
    }

    // Update is called once per frame
    void Update()
    {
        MoveChunks();

        DestroyAndAddChunk();
        //GenerateFences();
        //DeleteChunk();
    }

    public void SpeedUpLevel(float speedChange)
    {

        translationSpeed = Mathf.Clamp(translationSpeed + speedChange, minimumTranslationSpeed, maximumTranslationSpeed);
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedChange);
        speedUpCinemachineCamera.ChangeFOV(speedChange);
        if (speedChange > 0)
        {
            particleSystem.Play();
        }
    }

    void GenerateChunks()
    {
        for (int i = 0; i < numberOfStartingChunks; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, chunkLength * i);
            GameObject chunkObject = Instantiate(chunkPrefab, spawnPosition, Quaternion.identity, chunkParent);
            chunkPrefabs.Add(chunkObject);
            int numberOfAvailableLanes = 3;
            List<float> emptyLanes = new List<float> { -2.5f, 0f, 2.5f };
            for (int j = 0; j < 3; j++)
            {
                if (numberOfAvailableLanes == 1) break;
                if (Random.value > fenceSpawnChance) continue;
                //int selectedLane = Random.Range(0, 3);
                emptyLanes[j] = 1;
                numberOfAvailableLanes--;
                Vector3 fenceSpawnPosition = new Vector3(lanes[j], chunkObject.transform.position.y + heightOfFence, chunkObject.transform.position.z);
                GameObject fenceInstantiated = Instantiate(fence, fenceSpawnPosition, Quaternion.identity);
                fenceInstantiated.transform.parent = chunkObject.transform;
            }
            int randomIndex = Random.Range(0, 3);
            //coinsSpawned = false;
            if (emptyLanes[randomIndex] != 1)
            {
                coinsSpawned = true;
                for (int k = 0; k < 5; k++)
                {
                    if (Random.value < coinSpawnChance)
                    {
                        Vector3 coinSpawnPosition = new Vector3(emptyLanes[randomIndex], chunkObject.transform.position.y + heightOfCoin, chunkObject.transform.position.z + 4f - 2f * k);
                        Instantiate(coin, coinSpawnPosition, Quaternion.identity, chunkObject.transform);
                    }
                }
                emptyLanes[randomIndex] = 1;
            }
            int randomAppleIndex = Random.Range(0, 3);
            if (emptyLanes[randomAppleIndex] != 1)
            {
                Vector3 appleSpawnPosition = new Vector3(emptyLanes[randomAppleIndex], chunkObject.transform.position.y + heightOfApple, chunkObject.transform.position.z);
                Instantiate(apple, appleSpawnPosition, Quaternion.identity, chunkObject.transform);
            }
            
        }
    }

    void MoveChunks()
    {

        for (int i = 0; i < chunkPrefabs.Count; i++)
        {
            chunkPrefabs[i].transform.Translate(0f, 0f, -translationSpeed * chunkLength * Time.deltaTime);

        }

    }

    void DestroyAndAddChunk()
    {
        if (chunkPrefabs[0].transform.position.z < Camera.main.transform.position.z - chunkLength )
        {
            Destroy(chunkPrefabs[0]);
            chunkPrefabs.RemoveAt(0);
            float spawnZPos = chunkPrefabs[chunkPrefabs.Count - 1].transform.position.z + chunkLength;
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, spawnZPos);
            GameObject AddedChunkPrefab = Instantiate(chunkPrefab, spawnPosition, Quaternion.identity, chunkParent);
            chunkPrefabs.Add(AddedChunkPrefab);
            int numberOfAvailableLanes = 3;
            List<float> emptyLanes = new List<float> { -2.5f, 0f, 2.5f };
            for (int j = 0; j < 3; j++)
            {
                if (numberOfAvailableLanes == 1) break;
                if (Random.value > fenceSpawnChance) continue;
                //int selectedLane = Random.Range(0, 3);
                //emptyLanes.RemoveAt(j);
                emptyLanes[j] = 1;
                numberOfAvailableLanes--;
                Vector3 fenceSpawnPosition = new Vector3(lanes[j], AddedChunkPrefab.transform.position.y + heightOfFence, AddedChunkPrefab.transform.position.z);
                GameObject fenceInstantiated = Instantiate(fence, fenceSpawnPosition, Quaternion.identity);
                fenceInstantiated.transform.parent = AddedChunkPrefab.transform;
            }
            int randomIndex = Random.Range(0, 3);
            if (emptyLanes[randomIndex] != 1)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (Random.value < coinSpawnChance)
                    {
                        Vector3 coinSpawnPosition = new Vector3(emptyLanes[randomIndex], AddedChunkPrefab.transform.position.y + heightOfCoin, AddedChunkPrefab.transform.position.z + 4f - 2f * k);
                        Instantiate(coin, coinSpawnPosition, Quaternion.identity, AddedChunkPrefab.transform);
                    }

                }
                emptyLanes[randomIndex] = 1;
            }
            int randomAppleIndex = Random.Range(0, 3);
            if (emptyLanes[randomAppleIndex] != 1)
            {
                Vector3 appleSpawnPosition = new Vector3(emptyLanes[randomAppleIndex], AddedChunkPrefab.transform.position.y + heightOfApple, AddedChunkPrefab.transform.position.z);
                Instantiate(apple, appleSpawnPosition, Quaternion.identity, AddedChunkPrefab.transform);
            }

        }


    }

}

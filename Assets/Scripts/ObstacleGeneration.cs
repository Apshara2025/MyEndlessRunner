using System.Collections;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject player;
    [SerializeField] float heightForObstacleGeneration = 4f;
    [SerializeField] float distanceForObstacleGeneration = 40f;
    [SerializeField] float delay = 10f;
    [SerializeField] float forceInZDirection = 10f;

    Rigidbody myRigidBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(SpawnObstacles());
    }

    // Update is called once per frame
    void Update()
    {

    }



    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float randomXPosition = Random.Range(-4f, 4f);
            Vector3 spawnPosition = new Vector3(randomXPosition, heightForObstacleGeneration, distanceForObstacleGeneration);
            yield return new WaitForSeconds(delay);
            GameObject currentObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            myRigidBody = currentObstacle.GetComponent<Rigidbody>();
            Vector3 force = new Vector3(randomXPosition, 0f, -forceInZDirection);
            myRigidBody.AddRelativeForce(force);

        }
    }
}

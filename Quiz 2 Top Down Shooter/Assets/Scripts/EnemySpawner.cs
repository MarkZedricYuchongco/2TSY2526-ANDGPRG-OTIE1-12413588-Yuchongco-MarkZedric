using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private float spawnDistance = 15f;

    private Transform playerTransform;
    private float nextSpawnTime;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        int spawnCount = Random.Range(1, 4);

        for (int i = 0; i < spawnCount; i++)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2);

            float spawnX = playerTransform.position.x + spawnDistance * Mathf.Cos(randomAngle);
            float spawnZ = playerTransform.position.z + spawnDistance * Mathf.Sin(randomAngle);

            Vector3 spawnPosition = new Vector3(spawnX, playerTransform.position.y, spawnZ);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned an enemy at: " + spawnPosition);
        }
    }
}

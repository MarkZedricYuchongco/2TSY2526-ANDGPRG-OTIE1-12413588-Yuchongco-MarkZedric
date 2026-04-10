using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [System.Serializable]
    public class Wave
    {
        public int groundCount;
        public int airCount;
        public int bossCount;
        public float timeBetweenSpawns = 0.5f;
    }

    [Header("Monster Prefabs")]
    public GameObject groundMonsterPrefab;
    public GameObject airMonsterPrefab;
    public GameObject bossMonsterPrefab;

    [Header("Wave Configuration")]
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 10f;

    [Header("Player Stats Configuration")]
    public int health = 100;
    public int gold = 500;

    private int currentWaveIndex = 0;
    private List<GameObject> activeMonsters = new List<GameObject>();

    public TextMeshProUGUI gameInfo;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI waveCountdownText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (waves.Length > 0)
        {
            StartCoroutine(SpawnWaves());
        }
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {

            yield return StartCoroutine(SpawnWaveRoutine(waves[currentWaveIndex]));

            yield return new WaitUntil(() => activeMonsters.Count == 0);

            Debug.Log($"Wave cleared. Next wave in {timeBetweenWaves}s.");

            currentWaveIndex++;

            if (currentWaveIndex < waves.Length)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        Debug.Log("All waves cleared.");
    }

    IEnumerator SpawnWaveRoutine(Wave wave)
    {
        for (int i = 0; i < wave.groundCount; i++)
        {
            SpawnMonster(groundMonsterPrefab);
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }

        for (int i = 0; i < wave.airCount; i++)
        {
            SpawnMonster(airMonsterPrefab);
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }

        for (int i = 0; i < wave.bossCount; i++)
        {
            SpawnMonster(bossMonsterPrefab);
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }
    }

    void SpawnMonster(GameObject prefab)
    {
        if (prefab == null) return;

        GameObject monster = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        activeMonsters.Add(monster);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Game Over!");
        }
    }

    void Update()
    {
        activeMonsters.RemoveAll(monster => monster == null);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (gameInfo != null)
        {
            gameInfo.text = $"Health: {health}\n" +
                            $"Wave: {currentWaveIndex + 1}/{waves.Length}\n" +
                            $"Monsters Left: {activeMonsters.Count}";
        }

        if (goldText != null)
        {
            goldText.text = $"Gold: {gold}";
        }

        if (waveCountdownText != null)
        {
            float timeToNextWave = timeBetweenWaves - (Time.time % timeBetweenWaves);
            if (timeToNextWave > 0 && activeMonsters.Count <= 0)
            {
                waveCountdownText.text = $"Next Wave In: {timeToNextWave:F1}s";
            }
            else
            {
                waveCountdownText.text = "";
            }
        }
    }
}

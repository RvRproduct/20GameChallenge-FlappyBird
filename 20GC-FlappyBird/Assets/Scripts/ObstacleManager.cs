using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    [SerializeField] private int MAXOBSTACLEPOOLSIZE = 10;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float intialSpawnYMin = -10.3f;
    [SerializeField] private float intialSpawnYMax = -6.3f;
    [SerializeField] private float maxSpawnRateTimer = 2.0f;
    private float currentSpawnRateTime;

    private List<GameObject> obstaclesPool = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentSpawnRateTime = maxSpawnRateTimer;
    }

    private void Start()
    {
        for (int obstacle = 0; obstacle < MAXOBSTACLEPOOLSIZE; obstacle++)
        {
            obstaclesPool.Add(Instantiate(obstaclePrefab));
            obstaclesPool[obstacle].SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameStarted())
        {
            SpawnTimer();
        }
        
    }

    private void SpawnTimer()
    {
        currentSpawnRateTime += Time.deltaTime;
        if (currentSpawnRateTime >= maxSpawnRateTimer)
        {
            SpawnObstacle();
            currentSpawnRateTime = 0.0f;
        }
    }

    public void SpawnObstacle()
    {
        foreach (GameObject obstacle in  obstaclesPool)
        {
            if (!obstacle.activeInHierarchy)
            {
                obstacle.SetActive(true);
                obstacle.transform.position = RandomSpawnPosition();
                break;
            }
        }
    }

    private Vector3 RandomSpawnPosition()
    {
        float randomYPosition = Random.Range(intialSpawnYMin, intialSpawnYMax);

        Vector3 randomSpawnLocation = new Vector3(spawnLocation.position.x, randomYPosition, spawnLocation.position.z);

        return randomSpawnLocation;
    }



}

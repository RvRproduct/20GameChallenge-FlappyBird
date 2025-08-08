using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance;

    [SerializeField] private int MAXBACKGROUNDPOOLSIZE = 3;
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private Transform spawnLocation;
    private List<GameObject> backgroundPool = new List<GameObject>();

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
    }

    private void Start()
    {
        for (int background = 0; background < MAXBACKGROUNDPOOLSIZE; background++)
        {
            backgroundPool.Add(Instantiate(backgroundPrefab));
            backgroundPool[background].SetActive(false);
        }

        SpawnBackground(Vector3.zero);
    }

    public Vector3 GetSpawnLocation()
    {
        return spawnLocation.position;
    }

    public void SpawnBackground(Vector3 _spawnPosition)
    {
        foreach (GameObject background in backgroundPool)
        {
            if (!background.activeInHierarchy)
            {
                background.SetActive(true);
                background.transform.position = _spawnPosition;
                break;
            }
        }
    }
}

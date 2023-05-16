using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject SkeletonPrefab;
    [SerializeField]
    private GameObject EyePrefab;
    [SerializeField]
    private GameObject GoblinPrefab;
    [SerializeField]
    private float SkeletonSpawnTimer;
    [SerializeField]
    private float EyeSpawnTimer;
    [SerializeField]
    private float GoblinSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(SkeletonSpawnTimer, SkeletonPrefab));
        StartCoroutine(SpawnEnemy(GoblinSpawnTimer, GoblinPrefab));
        StartCoroutine(SpawnEnemy(EyeSpawnTimer, EyePrefab));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(0, 5f), Random.Range(0f, 6f), 0f), Quaternion.identity);
        }
    }
}
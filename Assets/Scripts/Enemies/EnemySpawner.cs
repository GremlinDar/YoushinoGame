using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 8f;
    public float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(enemyPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
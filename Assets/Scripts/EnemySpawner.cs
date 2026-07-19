using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnTime = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnTime);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
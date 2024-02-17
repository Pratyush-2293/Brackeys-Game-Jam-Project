using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float moveSpeed = 5f;
    public int damage = 10;
    public float spawnRadius = 5f; // Adjust this value to control the spawn distance from the player

    private GameObject[] enemies;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
 
        Vector3 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
        spawnPosition.z = 0; 

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.SetActive(true);

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.moveSpeed = moveSpeed;
        enemyMovement.damage = damage;
    }

    private void Update()
    {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public GameObject[] GetEnemies()
    {
        return enemies;
    }
}

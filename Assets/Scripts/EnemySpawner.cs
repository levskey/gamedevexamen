using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1.5f;// zet spawn snelheid
    public float xRange = 8f; // range van het spawnen
    public float spawnY = 6f; // andere range van het spawnen

    void Start()
    {
        RestartSpawner();
    }

    public void SpawnEnemyPublic()
    {
        Vector2 pos = new Vector2(Random.Range(-xRange, xRange), spawnY);
        GameObject go = Instantiate(enemyPrefab, pos, Quaternion.Euler(0, 0, 180f));

        Enemy enemy = go.GetComponent<Enemy>();
        enemy.ApplySpeed(Enemy.SpeedBonus);
    }
    public void RestartSpawner()
    {
        CancelInvoke();
        InvokeRepeating(nameof(SpawnEnemyPublic), 0.5f, spawnRate);
    }
}
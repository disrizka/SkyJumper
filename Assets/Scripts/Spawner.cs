using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject coinPrefab;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 1f;
    public float coinSpawnChance = 0.5f;
    public int minCoins = 1;
    public int maxCoins = 3;
    public float coinSpacing = 1.5f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        float height = Random.Range(minHeight, maxHeight);
        pipes.transform.position += Vector3.up * height;

        if (Random.value <= coinSpawnChance && coinPrefab != null)
        {
            SpawnCoins(pipes.transform.position);
        }
    }

    private void SpawnCoins(Vector3 pipePosition)
    {
        int coinCount = Random.Range(minCoins, maxCoins + 1);
        float startY = -(coinCount - 1) * coinSpacing / 2f;

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 coinPos = pipePosition + new Vector3(0, startY + (i * coinSpacing), 0);
            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }
    }
}
using UnityEngine;

public class spawner : MonoBehaviour
{
    [Header("Prefab Coin")]
    public GameObject prefabCoin;
    [Header("Area Spawning")]
    public Vector3 areaCenter = Vector3.zero;
    public Vector3 areaSize = new Vector3(10, 0, 10);

    [Header("Jumlah Coin")]
    public int jumlahCoin = 20;

    void Start()
    {
        SpawnSampah();
    }

    void SpawnSampah()
    {
        for (int i = 0; i < jumlahCoin; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                0.5f,
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            Vector3 spawnPos = areaCenter + randomPos;

            GameObject prefab = prefabCoin;

            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(areaCenter, areaSize);
    }
}

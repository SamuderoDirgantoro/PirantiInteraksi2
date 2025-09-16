using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefab musuh yang akan dimunculkan
    public GameObject enemyPrefab;

    // Jarak minimum dari pemain untuk spawn musuh
    public float minSpawnDistance = 20f;
    // Jarak maksimum dari pemain untuk spawn musuh
    public float maxSpawnDistance = 30f;

    // Kecepatan spawn musuh
    public float spawnInterval = 3f;

    // Referensi ke transform pemain
    public Transform playerTransform;

    private float timer;

    void Start()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }

        if (playerTransform == null)
        {
            Debug.LogError("Player tidak ditemukan! Pastikan objek pemain memiliki tag 'Player'.");
            return;
        }

        timer = spawnInterval;
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector3 playerPos = playerTransform.position;


        float randomAngle;

        int segment = Random.Range(0, 3);

        switch (segment)
        {
            case 0: // Sisi Kiri: Sudut dari 90 hingga 180 derajat
                randomAngle = Random.Range(90f, 180f);
                break;
            case 1: // Sisi Atas: Sudut dari 0 hingga 180 derajat (atau kita bisa persempit)
                randomAngle = Random.Range(0f, 180f);
                break;
            case 2: // Sisi Kanan: Sudut dari 0 hingga 90 derajat
                randomAngle = Random.Range(0f, 90f);
                break;
            default:
                randomAngle = 0f;
                break;
        }

        // Konversi sudut dari derajat ke radian untuk fungsi Mathf.Cos dan Mathf.Sin
        randomAngle *= Mathf.Deg2Rad;

        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        float spawnX = playerPos.x + randomDistance * Mathf.Cos(randomAngle);
        float spawnY = playerPos.y + randomDistance * Mathf.Sin(randomAngle);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // Cek apakah ada collider di posisi spawn untuk menghindari tumpang tindih (opsional)
        if (Physics2D.OverlapCircle(spawnPosition, 1f) == null)
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
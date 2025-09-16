using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f; // Durasi peluru sebelum hancur

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Atur kecepatan peluru saat dibuat
        // Arah akan diatur dari skrip menembak

        // Hancurkan objek setelah waktu tertentu
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah peluru menabrak musuh
        if (other.CompareTag("Enemy"))
        {
            // Hancurkan musuh
            Destroy(other.gameObject);
            // Hancurkan peluru
            Destroy(gameObject);
        }
    }
}
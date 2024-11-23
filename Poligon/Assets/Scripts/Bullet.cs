using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1000f;   // Merminin çıkış hızı
    public float lifeTime = 5f;         // Merminin yok olacağı süre

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Yerçekimini devre dışı bırak (düz hareket için)
        rb.useGravity = false;

        // Mermiyi ileri doğru fırlat (düz bir çizgide)
        rb.velocity = transform.forward * bulletSpeed;

        // Belirli bir süre sonra mermiyi yok et
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Çarptığı cismi yok et
        Destroy(collision.gameObject);

        // Mermiyi yok et
        Destroy(gameObject);
    }
}
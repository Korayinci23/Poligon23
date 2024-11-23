using UnityEngine;

public class NPCShooting : MonoBehaviour
{
    public GameObject bulletPrefab;      // Mermi prefabı
    public Transform firePoint;          // Merminin çıkış noktası
    public float shootingInterval = 2.0f;// Atış aralığı
    public int numberOfShots = 5;        // Yapılacak atış sayısı

    private int shotsFired = 0;

    public void StartShooting()
    {
        shotsFired = 0;
        InvokeRepeating("Shoot", 1.0f, shootingInterval);
    }

    void Shoot()
    {
        if (shotsFired < numberOfShots)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * 10f; // Mermiyi düz bir şekilde hareket ettir
            shotsFired++;
        }
        else
        {
            CancelInvoke("Shoot");
            GetComponent<NPCController>().OnShootingFinished();
        }
    }
}
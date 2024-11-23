using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Aşınma değişkenleri
    public float wearRate = 0.1f;          // Her atışta aşınma oranı (%)
    public float wearLevel = 0.0f;         // Mevcut aşınma seviyesi (%)

    // Kirlenme değişkenleri
    public float dirtRate = 0.2f;          // Her atışta kirlenme oranı (%)
    public float dirtLevel = 0.0f;         // Mevcut kirlenme seviyesi (%)

    // Performans düşüşü için eşikler
    private const float wearThreshold1 = 20.0f;
    private const float wearThreshold2 = 40.0f;
    private const float wearThreshold3 = 60.0f;
    private const float wearThreshold4 = 100.0f;

    private const float dirtThreshold1 = 20.0f;
    private const float dirtThreshold2 = 40.0f;
    private const float dirtThreshold3 = 60.0f;
    private const float dirtThreshold4 = 100.0f;

    public void Fire()
    {
        // Aşınma ve kirlenme hesaplaması
        ApplyWear();
        ApplyDirt();

        // Performans güncellemesi
        UpdateWeaponPerformance();

        // Silahın aşırı aşınmış veya kirlenmiş olup olmadığını kontrol et
        if (wearLevel >= wearThreshold4)
        {
            Debug.Log("Silah aşırı aşınmış, parça tamamen bozuldu! Değiştirilmesi gerekiyor.");
            // Silahı kullanılamaz hale getir
            DisableWeapon();
        }

        if (dirtLevel >= dirtThreshold4)
        {
            Debug.Log("Silah tamamen kirlenmiş, temizlenene kadar kullanılamaz.");
            // Silahı kullanılamaz hale getir
            DisableWeapon();
        }
    }

    private void ApplyWear()
    {
        wearLevel += wearRate;
        wearLevel = Mathf.Clamp(wearLevel, 0f, 100f); // Aşınma seviyesi %100'ü geçmesin
        Debug.Log($"Silah aşındı: {wearLevel}%");
    }

    private void ApplyDirt()
    {
        dirtLevel += dirtRate;
        dirtLevel = Mathf.Clamp(dirtLevel, 0f, 100f); // Kirlenme seviyesi %100'ü geçmesin
        Debug.Log($"Silah kirlendi: {dirtLevel}%");
    }

    private void UpdateWeaponPerformance()
    {
        if (wearLevel >= wearThreshold3 || dirtLevel >= dirtThreshold3)
        {
            Debug.Log("Silah performansı ciddi şekilde düştü, sıkışmalar başlayabilir.");
            // Performans düşüşü için gerekli işlemler
        }
        else if (wearLevel >= wearThreshold2 || dirtLevel >= dirtThreshold2)
        {
            Debug.Log("Silah performansı belirgin şekilde düştü.");
            // Performans düşüşü için gerekli işlemler
        }
        else if (wearLevel >= wearThreshold1 || dirtLevel >= dirtThreshold1)
        {
            Debug.Log("Silah performansı hafifçe düştü.");
            // Performans düşüşü için gerekli işlemler
        }
    }

    private void DisableWeapon()
    {
        // Silahı devre dışı bırakmak için kullanılacak işlemler
        // Örneğin, ateşleme fonksiyonunu kapatabilir veya animasyonları devre dışı bırakabilirsiniz
        enabled = false;
    }

    public void CleanWeapon()
    {
        dirtLevel = 0f;
        Debug.Log("Silah temizlendi.");
        enabled = true; // Silah tekrar kullanılabilir hale gelir
    }

    public void RepairWeapon()
    {
        wearLevel = 0f;
        Debug.Log("Silah tamir edildi.");
        enabled = true; // Silah tekrar kullanılabilir hale gelir
    }
}
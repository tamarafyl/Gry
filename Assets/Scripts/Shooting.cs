using UnityEngine;

public class ShootingCtrl : MonoBehaviour
{
    public float range = 100f; // Дистанція пострілу
    public Camera cam; // Перетягніть сюди вашу камеру в Inspector
    
    // Опціонально: Ефекти
    public ParticleSystem muzzleFlash; // Ефект спалаху на дулі (якщо є)
    public GameObject impactEffect; // Що з'являється при влучанні (напр. дірка, іскри)

    void Update()
    {
        // Перевірка натискання Ctrl (за замовчуванням Fire2 в Unity Input)
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 1. Програємо спалах від пострілу (якщо додано)
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;
        // 2. Випускаємо невидимий промінь ТОЧНО з центру екрану
        // ViewportToWorldPoint(0.5, 0.5) — це центр камери
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        
        // Стріляємо вперед від камери
        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, range))
        {
            Debug.Log("Влучання точно в центр екрану: " + hit.transform.name);

            // 3. Ефект влучання (опціонально)
            if (impactEffect != null)
            {
                // Створюємо ефект у точці влучання, повернутий до нас
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f); // Видаляємо ефект через 2 секунди
            }

            // 4. Логіка влучання у тварину
            // Шукаємо скрипт саме на BoarPrefab (використовуємо GetComponentInParent)
            AnimalAI animal = hit.transform.GetComponentInParent<AnimalAI>();
            
            if (animal != null)
            {
                // Викликаємо метод смерті з ТВОГО скрипту
                animal.OtrzymajObrazenia(); 
            }
        }
    }
}
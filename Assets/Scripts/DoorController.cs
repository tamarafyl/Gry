using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isOpen", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Перевіряємо ключ у GameManager
            if (GameManager.instance != null && GameManager.instance.hasHuntingKey)
            {
                anim.SetBool("isOpen", true);
                Debug.Log("Ключ є! Двері відчиняються.");
            }
            else
            {
                Debug.Log("Двері замкнені. Потрібен ключ!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isOpen", false);
            Debug.Log("Двері зачиняються.");
        }
    }
}
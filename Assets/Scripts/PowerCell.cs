using UnityEngine;

public class PowerCell : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Inventory inv = col.GetComponent<Inventory>();
            if (inv != null)
                inv.CellPickup();

            Destroy(gameObject);
        }
    }
}

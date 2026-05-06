using UnityEngine;

public class TidyObject : MonoBehaviour
{
    public float removeTime = 3.0f;

    void Start()
    {
        Destroy(gameObject, removeTime);
    }

    void OnCollisionEnter(Collision theObject)
    {
        // Jeœli kokos uderzy w teren – zmieniamy jego nazwê
        if (theObject.gameObject.name == "Terrain")
        {
            gameObject.name = "ground_coconut";
        }
    }
}

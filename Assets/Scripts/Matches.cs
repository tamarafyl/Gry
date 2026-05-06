using UnityEngine;

public class Matches : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("MatchPickup");
            Destroy(gameObject);
        }
    }
}

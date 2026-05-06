using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    bool doorIsOpen = false;
    float doorTimer = 0.0f;
    public float doorOpenTime = 3.0f;
    public AudioClip doorOpenSound;
    public AudioClip doorShutSound;

    GameObject currentDoor;

    void Update()
    {
        if (doorIsOpen)
        {
            doorTimer += Time.deltaTime;
            if (doorTimer > doorOpenTime)
            {
                Door(currentDoor, false); 
                doorTimer = 0.0f;
            }
        }
    }

    
    /*
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "playerDoor" && !doorIsOpen)
        {
            currentDoor = hit.gameObject;
            Door(currentDoor, true); // Otwórz drzwi
        }
    }
    */

    void Door(GameObject door, bool open)
    {
        doorIsOpen = open;

        AudioSource src = door.GetComponent<AudioSource>();
        if (src != null)
        {
            if (open && doorOpenSound != null)
                src.PlayOneShot(doorOpenSound);
            else if (!open && doorShutSound != null)
                src.PlayOneShot(doorShutSound);
        }

        if (door.transform.parent != null)
        {
            Animation parentAnim = door.transform.parent.GetComponent<Animation>();
            if (parentAnim != null)
            {
                if (open)
                    parentAnim.Play("dooropen");
                else
                    parentAnim.Play("doorshut");
            }
        }
    }
}

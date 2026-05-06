using UnityEngine;
using TMPro; 

public class TriggerZone : MonoBehaviour
{
    [Header("Door Sounds")]
    public AudioClip doorOpenSound;
    public AudioClip doorShutSound;
    public AudioClip lockedSound;

    [Header("Door Light")]
    public Light doorLight;

    [Header("UI")]
    public TMP_Text textHints;

    private bool doorIsOpen = false;
    private Animation doorAnim;
    private AudioSource doorAudio;

    void Start()
    {
        doorAnim = GetComponentInParent<Animation>();
        doorAudio = GetComponentInChildren<AudioSource>();

        if (doorLight != null)
            doorLight.color = Color.red;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Inventory inv = other.GetComponent<Inventory>();
        if (inv == null) return;

        if (Inventory.charge >= 4 && !doorIsOpen)
        {
            doorIsOpen = true;
            doorAnim.Play("dooropen");

            if (doorAudio != null && doorOpenSound != null)
                doorAudio.PlayOneShot(doorOpenSound);

            if (doorLight != null)
            {
                doorLight.enabled = true;
                doorLight.color = Color.green;
            }

            GameObject powerGUI = GameObject.Find("PowerGUI");
            if (powerGUI != null)
            {
                Destroy(powerGUI);
            }

            Debug.Log("Drzwi otwarte, wszystkie ogniwa zebrane.");
        }

        else if (Inventory.charge == 0)
        {
            if (doorIsOpen) return;

            if (textHints != null)
                textHints.SendMessage("ShowHint", "Te drzwi wyglądają na zamknięte, \n być może generator wymaga \n odpowiedniego zasilania...");

            Transform doorTransform = transform.Find("door");
            if (doorTransform != null)
            {
                AudioSource doorAudioSource = doorTransform.GetComponent<AudioSource>();
                if (doorAudioSource != null && lockedSound != null)
                    doorAudioSource.PlayOneShot(lockedSound);
            }

            inv.HUDon();
            inv.UpdateGenerator();

            Debug.Log("Drzwi zamknięte — potrzeba więcej ogniw.");
        }

        else if (Inventory.charge > 0 && Inventory.charge < 4)
        {
            if (doorIsOpen) return;

            if (textHints != null)
                textHints.SendMessage("ShowHint", "Drzwi ani drgną … \n pewnie potrzebują więcej mocy...");

            Transform doorTransform = transform.Find("door");
            if (doorTransform != null)
            {
                AudioSource doorAudioSource = doorTransform.GetComponent<AudioSource>();
                if (doorAudioSource != null && lockedSound != null)
                    doorAudioSource.PlayOneShot(lockedSound);
            }


            inv.HUDon();
            inv.UpdateGenerator();

            Debug.Log("Drzwi zamknięte — potrzeba więcej ogniw (postęp).");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || !doorIsOpen) return;

        doorIsOpen = false;
        doorAnim.Play("doorshut");

        if (doorAudio != null && doorShutSound != null)
            doorAudio.PlayOneShot(doorShutSound);

        Debug.Log("Drzwi zamknęły się.");
    }
}
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TargetCollision : MonoBehaviour
{
    bool beenHit = false;
    Animation targetRoot;

    public AudioClip hitSound;
    public AudioClip resetSound;
    public float resetTime = 3.0f;

    void Start()
    {
        // pobieramy komponent Animation z "dziadka"
        targetRoot = transform.parent.transform.parent.GetComponent<Animation>();
    }

    void OnCollisionEnter(Collision theObject)
    {
        if (beenHit == false && theObject.gameObject.name == "coconut")
        {
            StartCoroutine("targetHit");
        }
    }

    IEnumerator targetHit()
    {
        // 1. Trafienie celu
        GetComponent<AudioSource>().PlayOneShot(hitSound);
        targetRoot.Play("down");
        beenHit = true;

        // Zwiêkszenie licznika trafieñ
        CoconutWin.targets++;

        // Czekaj okreœlony czas, zanim cel wróci do stanu pocz¹tkowego
        yield return new WaitForSeconds(resetTime);

        // 2. Powrót celu do stanu pocz¹tkowego
        GetComponent<AudioSource>().PlayOneShot(resetSound);
        targetRoot.Play("up");

        // Zmniejszenie licznika trafieñ
        CoconutWin.targets--;

        // Cel znów gotowy do trafienia
        beenHit = false;
    }

}

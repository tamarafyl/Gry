using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoconutThrower : MonoBehaviour
{
    public static bool canThrow = false;       

    public AudioClip throwSound;              
    public Rigidbody coconutPrefab;            
    public float throwSpeed = 30.0f;         

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canThrow)
        {
            GetComponent<AudioSource>().PlayOneShot(throwSound);

            Rigidbody newCoconut = Instantiate(coconutPrefab, transform.position, transform.rotation) as Rigidbody;

            newCoconut.name = "coconut";

            newCoconut.linearVelocity = transform.forward * throwSpeed;

            Physics.IgnoreCollision(transform.root.GetComponent<Collider>(),
                                    newCoconut.GetComponent<Collider>(), true);
        }
    }
}

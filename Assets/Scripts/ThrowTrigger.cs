using UnityEngine;
using UnityEngine.UI;

public class ThrowTrigger : MonoBehaviour
{
    // Publiczna zmienna do przypisania obiektu celownika w Inspectorze
    public RawImage crosshair;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Gracz mo¿e rzucaæ orzechami
            CoconutThrower.canThrow = true;

            // W³¹cz celownik
            if (crosshair != null)
            {
                crosshair.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Gracz nie mo¿e rzucaæ orzechami
            CoconutThrower.canThrow = false;

            // Wy³¹cz celownik
            if (crosshair != null)
            {
                crosshair.enabled = false;
            }
        }
    }
}

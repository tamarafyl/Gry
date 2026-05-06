using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static int charge = 0;

    [Header("Audio")]
    public AudioClip collectSound;

    [Header("HUD")]
    public Texture2D[] hudCharge;
    public RawImage chargeHudGUI;

    [Header("Generator")]
    public Texture2D[] meterCharge;
    public Renderer meter;

    [Header("Matches")]
    public RawImage matchHudGUI;
    private bool haveMatches = false;

    [Header("Hints")]
    public TextHints textHints;

    [Header("Fire State")]
    private bool fireLit = false;

    void Start()
    {
        charge = 0;
        if (chargeHudGUI != null)
            chargeHudGUI.enabled = false;

        if (matchHudGUI != null)
            matchHudGUI.enabled = false;
    }

    public void CellPickup()
    {
        charge++;
        charge = Mathf.Clamp(charge, 0, hudCharge.Length - 1);

        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        HUDon();
        UpdateHUD();
        UpdateGenerator();

        Debug.Log("Collected power cell. Total charge: " + charge);
    }

    public void HUDon()
    {
        if (chargeHudGUI != null && !chargeHudGUI.enabled)
            chargeHudGUI.enabled = true;
    }

    void UpdateHUD()
    {
        if (chargeHudGUI != null)
            chargeHudGUI.texture = hudCharge[charge];
    }

    public void UpdateGenerator()
    {
        if (meter != null)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            meter.GetPropertyBlock(block);
            block.SetTexture("_MainTex", meterCharge[charge]);
            meter.SetPropertyBlock(block);
        }
    }

    public void MatchPickup()
    {
        haveMatches = true;

        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (matchHudGUI != null)
            matchHudGUI.enabled = true;

        Debug.Log("Collected matches.");
    }

    public bool HasMatches()
    {
        return haveMatches;
    }

    public void UseMatches()
    {
        if (haveMatches)
        {
            haveMatches = false;

            if (matchHudGUI != null)
                matchHudGUI.enabled = false;

            Debug.Log("Matches used.");
        }
        else
        {
            Debug.Log("No matches to use.");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (col.gameObject.name == "campfire")
        {
            if (haveMatches && !fireLit)
            {
                LightFire(col.gameObject);
            }
            else if (!haveMatches && !fireLit)
            {
                if (textHints != null)
                    textHints.ShowHint(
                        "Mógłbym rozpalić ognisko do wezwania pomocy.\nTylko czym...");
            }
        }
    }

    void LightFire(GameObject campfire)
    {
        ParticleSystem[] fireEmitters = campfire.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem emitter in fireEmitters)
        {
            emitter.Play();
        }

        AudioSource fireAudio = campfire.GetComponent<AudioSource>();
        if (fireAudio != null)
            fireAudio.Play();

        UseMatches();

        fireLit = true; 

        Debug.Log("Campfire lit!");
    }
}

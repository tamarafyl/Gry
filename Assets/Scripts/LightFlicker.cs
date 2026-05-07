using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light myLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    void Start() => myLight = GetComponent<Light>();

    void Update()
    {
        myLight.intensity = Random.Range(minIntensity, maxIntensity);
    }
}
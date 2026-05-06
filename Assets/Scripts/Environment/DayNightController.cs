using UnityEngine;

public class DayNightController : MonoBehaviour
{
    [Header("Настройки Солнца")]
    public Light sun;
    public Color dayColor = Color.white;
    public Color nightColor = new Color(0.1f, 0.1f, 0.3f); // Синеватый
    public float dayIntensity = 1.5f;
    public float nightIntensity = 0.1f;

    [Header("Настройки Неба и Тумана")]
    public Color dayAmbient = Color.gray;
    public Color nightAmbient = Color.black;
    public float dayFogDensity = 0.01f;
    public float nightFogDensity = 0.05f;

    [Range(0, 1)] public float timeOfDay = 1; // 1 - день, 0 - ночь

    public float nightExposure = 0.1f;
    public float dayExposure = 0.6f;

    void Update()
    {
        // Плавная смена всех параметров в зависимости от ползунка timeOfDay
        sun.color = Color.Lerp(nightColor, dayColor, timeOfDay);
        sun.intensity = Mathf.Lerp(nightIntensity, dayIntensity, timeOfDay);
        
        RenderSettings.ambientLight = Color.Lerp(nightAmbient, dayAmbient, timeOfDay);
        RenderSettings.fogDensity = Mathf.Lerp(nightFogDensity, dayFogDensity, timeOfDay);

        // Вращение солнца (опционально)
        sun.transform.rotation = Quaternion.Euler(200f - timeOfDay * 90f, 30f, 0);


        float currentExposure = Mathf.Lerp(nightExposure, dayExposure, timeOfDay);
        RenderSettings.skybox?.SetFloat("_Exposure", currentExposure);
    }
}
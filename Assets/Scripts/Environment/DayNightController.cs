using UnityEngine;

public class DayNightController : MonoBehaviour
{
    [Header("Настройки Солнца")]
    public Light sun;
    public Color dayColor = Color.white;
    public Color nightColor = new Color(0.1f, 0.1f, 0.3f);
    public float dayIntensity = 1.5f;
    public float nightIntensity = 0.1f;

    [Header("Настройки Неба и Тумана")]
    public Color dayAmbient = Color.gray;
    public Color nightAmbient = Color.black;
    public float dayFogDensity = 0.01f;
    public float nightFogDensity = 0.05f;

    public Color dayEquator = Color.gray;
    public Color nightEquator = Color.black;

    public Color dayGround = new Color(0.3f, 0.4f, 0.3f); 
    public Color nightGround = Color.black;

    [Header("Таймер (2 хв день + 2 хв ніч)")]
    [Range(0, 1)] public float timeOfDay = 1; 
    public float cycleDurationMinutes = 4f; 

    public float nightExposure = 0.1f;
    public float dayExposure = 0.6f;

    void Update()
    {
    
        float speed = (2 * Mathf.PI) / (cycleDurationMinutes * 60f);
     
        timeOfDay = (Mathf.Cos(Time.time * speed) + 1) / 2;

      
        if (GameManager.instance != null)
        {
            
            GameManager.instance.UpdateDayStatus(timeOfDay > 0.5f);
        }

       
        sun.color = Color.Lerp(nightColor, dayColor, timeOfDay);
        sun.intensity = Mathf.Lerp(nightIntensity, dayIntensity, timeOfDay);
        
        RenderSettings.ambientLight = Color.Lerp(nightAmbient, dayAmbient, timeOfDay);
        RenderSettings.fogDensity = Mathf.Lerp(nightFogDensity, dayFogDensity, timeOfDay);
  
        RenderSettings.ambientIntensity = Mathf.Lerp(0.2f, 1.5f, timeOfDay);
        RenderSettings.reflectionIntensity = Mathf.Lerp(0.2f, 1.0f, timeOfDay);
        RenderSettings.ambientSkyColor = Color.Lerp(nightAmbient, dayAmbient, timeOfDay);
        RenderSettings.ambientEquatorColor = Color.Lerp(nightEquator, dayEquator, timeOfDay);
        RenderSettings.ambientGroundColor = Color.Lerp(nightGround, dayGround, timeOfDay);

        sun.transform.rotation = Quaternion.Euler(200f - timeOfDay * 90f, 30f, 0);

        float currentExposure = Mathf.Lerp(nightExposure, dayExposure, timeOfDay);
        RenderSettings.skybox?.SetFloat("_Exposure", currentExposure);
    }
}
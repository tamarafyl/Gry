using UnityEngine;

public class PlayerPositionRestorer : MonoBehaviour
{
    void Awake()
    {
        // Перевіряємо, чи повернулися ми щойно з мінігри
        if (PlayerPrefs.GetInt("ShouldRestorePosition", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("ReturnPosX");
            float y = PlayerPrefs.GetFloat("ReturnPosY");
            float z = PlayerPrefs.GetFloat("ReturnPosZ");
            float rotY = PlayerPrefs.GetFloat("ReturnRotY");

            // Вимикаємо контролер (якщо він є), щоб фізика не заважала телепортації
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            // Переміщуємо гравця
            transform.position = new Vector3(x, y, z);
            transform.rotation = Quaternion.Euler(0, rotY, 0);

            if (cc != null) cc.enabled = true;

            // Скидаємо мітку, щоб при наступному звичайному запуску гри не телепортувало
            PlayerPrefs.SetInt("ShouldRestorePosition", 0);
            PlayerPrefs.Save();
            
            Debug.Log("Позиція гравця відновлена!");
        }
    }
}
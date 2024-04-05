using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Importar la librería para cambiar los assets del UI de Unity
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text TimeText;
    public Sprite SpendLives;
    public Image[] livesImage;

    int lives = 3;
    int time;

    // Start is called before the first frame update
    void Start()
    {
        time = GameControl.Instance.timeToWin;
        // If we don't have lives, set to 3
        lives = PlayerPrefs.GetInt("lives", 3);
        // Inicializar el texto
        ActiveText();
    }

    // Actualizar texto de tiempo restante
    public void ActiveText()
    {
        TimeText.text = "Remaining Time: " + time;
    }

    // Función que actualiza el tiempo después de un segundo.
    // Cuando se acaba el tiempo, cambia a la escena final
    // La función se llama a sí misma para actualizar al siguiente segundo
    IEnumerator MatchTime()
    {
        // Every second we update the time with one second less
        yield return new WaitForSeconds(1);
        time -= 1;
        ActiveText();
        if (time == 0)
        {
            GameControl.Instance.ActiveEndScene();
        }
        else
        {
            StartCoroutine(MatchTime());
        }
    }

    // El timer se actualiza de manera asincrónica
    public void StartTimer()
    {
        StartCoroutine(MatchTime());
    }

    // Función para actualizar las vidas en el UI
    public void UpdateLives()
    {
        lives = GameControl.Instance.GetCurrentLives();
        if (lives > 0)
        {
            livesImage[lives].sprite = SpendLives;
        }
        GameControl.Instance.checkGameOver();
    }


    // Función llamada por el botón de Home
    public void ReturnToMenu()
    {
        GameControl.Instance.GoToMenu();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Para cambiar la escena a la escena final
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Tiempo para ganar
    public int timeToWin = 15;
    // Instancias de controladores
    static public GameControl Instance;
    public UIController UIController;
    public SFXManager SFXManager;

    // Función llamada automaticamente por Unity antes de iniciar el juego
    private void Awake()
    {
        // Reiniciar estado de vidas y referencias a instancias de controladores
        StopAllCoroutines();
        PlayerPrefs.SetInt("lives", 3);
        PlayerPrefs.SetInt("TimeToWin", PlayerPrefs.GetInt("TimeToWin", timeToWin));
        Instance = this;
        Instance.SetReferences();
        DontDestroyOnLoad(this.gameObject);
    }

    // Función para encontrar las instancias a controladores si no están definidas
    void SetReferences()
    {
        // Si la instancia no existe
        if (UIController == null)
        {
            // Crearla
            UIController = FindAnyObjectByType<UIController>();
        }
        if (SFXManager == null)
        {
            SFXManager = FindAnyObjectByType<SFXManager>();
        }
        timeToWin = PlayerPrefs.GetInt("TimeToWin", 15);
        init();
    }

    // Función para iniciar la cuenta atrás
    void init()
    {
        if (UIController != null)
        {
            UIController.StartTimer();
        }
    }

    // Función para obtener las vidas actuales
    public int GetCurrentLives()
    {
        // If lives aren't initialized, set as 3
        return PlayerPrefs.GetInt("lives", 3);
    }

    // Función para disminuir una vida
    // También detecta si se nos acaban las vidas para pasar a la pantalla final
    public void SpendLives()
    {
        if (GetCurrentLives() > 0)
        {
            // Update the amount of lives by decreasing the current amount
            int newLives = GetCurrentLives() - 1;
            PlayerPrefs.SetInt("lives", newLives);
            UIController.UpdateLives();
        }
        else
        {
            ActiveEndScene();
        }
    }

    // Función para checar si se nos acabaron las vidas
    // Si es así, ir a la pantalla final
    public void checkGameOver()
    {
        if (PlayerPrefs.GetInt("lives", 3) < 1)
        {
            ActiveEndScene();
        }
    }

    // Función para cambiar a la pantalla final
    public void ActiveEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    // Función para cambiar a la pantalla inicial
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}

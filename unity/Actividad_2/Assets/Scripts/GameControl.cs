using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Estas instancias se usan para recargar el juego a su estado inicial
    static public GameControl Instance;
    public GameObject birdBehaviour;
    public PlayerControl playerControl;
    public UIController uIController;
    public SFXManager SFXManager;

    public int pointsToWin = 3;

    // Awake se llama cuando se está inicializando el juego, antes que Start
    private void Awake()
    {
        // Player Prefs: manera de compartir información entre los scripts
        // sin tener que mandar a llamar nada a otras instancias o algo.
        // Los player prefs se quedan guardados entre escenas.
        // Toda la información de una escena se destruye al quitarla
        // Con ellos guardamos si el jugador ganó o no para la siguiente escena
        PlayerPrefs.SetInt("PointsToWin", pointsToWin);
        // PlayerPrefs.GetInt("PointsToWin", variable);

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Función para cambiar entre escenas
    // Correr la función para abrir la escena final
    public void ActiveEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void checkGameOver(int _currentScore)
    {
        // Actualizar el score directo en PlayerPrefs
        PlayerPrefs.SetInt("score", _currentScore);
        // Checar si se ganó
        if (_currentScore >= pointsToWin)
        {
            ActiveEndScene();
        }
    }
}

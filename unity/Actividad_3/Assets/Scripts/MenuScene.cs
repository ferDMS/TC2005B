using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para cambiar de escenas
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    // Funci�n para el bot�n de Play, para cargar la escena de juego
    public void StartToPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Funci�n para el bot�n de Exit, para cerrar la aplicaci�n
    public void ExitGame()
    {
        // Aqu� vamos a tener que quitar esta l�nea cuando exportemos y descomentar la otra
        // UnityEditor.EditorApplication.isPlaying = false;
        // Cuando exportemos el juego el isPlaying ya no va estar, simplemente vamos a tener que cerrar el juego
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para cambiar de escenas
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    // Función para el botón de Play, para cargar la escena de juego
    public void StartToPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Función para el botón de Exit, para cerrar la aplicación
    public void ExitGame()
    {
        // Aquí vamos a tener que quitar esta línea cuando exportemos y descomentar la otra
        // UnityEditor.EditorApplication.isPlaying = false;
        // Cuando exportemos el juego el isPlaying ya no va estar, simplemente vamos a tener que cerrar el juego
        Application.Quit();
    }
}

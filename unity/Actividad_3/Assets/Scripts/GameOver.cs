using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Para usar los elementos de UI
using UnityEngine.UI;
// Para cambiar entre escenas
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Variables para definir las instancias directo en Unity
    public Text winLoseText;
    public GameObject ninaSprite;
    public SFXManager sound;

    // Al llegar a la pantalla, checar con que estado llegamos a la pantalla final
    void Start()
    {
        // Si llegamos con por lo menos una vida, ganamos
        if (PlayerPrefs.GetInt("lives",3)>0)
        {
            // Reproducir animación, texto y audio de perder
            SetWinAnimation();
            winLoseText.text = "Congratulations!! :D";
            sound.Winning();
        }
        // Si llegamos sin vidas, perdimos
        else
        {
            // Reproducir animación, texto y audio de perder
            SetLoseAnimation();
            winLoseText.text = "Game Over :(";
            sound.Losing();
        }
    }

    // Función para reproducir animación ganadora
    void SetWinAnimation()
    {
        ninaSprite.GetComponent<Animator>().SetTrigger("isWinning");
    }

    // Función para reproducir animación perdedora
    void SetLoseAnimation()
    {
        ninaSprite.GetComponent<Animator>().SetTrigger("isDying");
    }

    // Función para el botón de play
    public void StartToPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Función para el botón de exit
    public void ExitGame()
    {
        // Aquí vamos a tener que quitar esta línea cuando exportemos y descomentar la otra
        // UnityEditor.EditorApplication.isPlaying = false;
        // Cuando exportemos el juego el isPlaying ya no va estar, simplemente vamos a tener que cerrar el juego
        Application.Quit();
    }
}

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
            // Reproducir animaci�n, texto y audio de perder
            SetWinAnimation();
            winLoseText.text = "Congratulations!! :D";
            sound.Winning();
        }
        // Si llegamos sin vidas, perdimos
        else
        {
            // Reproducir animaci�n, texto y audio de perder
            SetLoseAnimation();
            winLoseText.text = "Game Over :(";
            sound.Losing();
        }
    }

    // Funci�n para reproducir animaci�n ganadora
    void SetWinAnimation()
    {
        ninaSprite.GetComponent<Animator>().SetTrigger("isWinning");
    }

    // Funci�n para reproducir animaci�n perdedora
    void SetLoseAnimation()
    {
        ninaSprite.GetComponent<Animator>().SetTrigger("isDying");
    }

    // Funci�n para el bot�n de play
    public void StartToPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Funci�n para el bot�n de exit
    public void ExitGame()
    {
        // Aqu� vamos a tener que quitar esta l�nea cuando exportemos y descomentar la otra
        // UnityEditor.EditorApplication.isPlaying = false;
        // Cuando exportemos el juego el isPlaying ya no va estar, simplemente vamos a tener que cerrar el juego
        Application.Quit();
    }
}

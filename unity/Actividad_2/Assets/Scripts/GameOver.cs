using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public Text winText;
    // Controlador del sonido (que se tiene que volver a poner porque es una nueva escena) 
    public SFXManager sound;

    // Start is called before the first frame update
    void Start()
    {
        // Si los puntos obtenidos son mayores que los puntos para ganar, entonces ganamos
        if(PlayerPrefs.GetInt("score") >= PlayerPrefs.GetInt("PointsToWin"))
        {
            winText.text = "Congratulations!! :D";
        }
        // Sonido de final de juego
        sound.endGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Variables para definir clips de audio en Unity
    public AudioClip coin;
    public AudioClip win;
    public AudioClip lose;

    // Función para reproducir sonido de moneda
    public void getCoin()
    {
        AudioSource.PlayClipAtPoint(coin, Camera.main.transform.position, 0.5f);
    }

    // Función para reproducir sonido de ganar
    public void Winning()
    {
        AudioSource.PlayClipAtPoint(win, Camera.main.transform.position, 0.5f);
    }

    // Función para reproducir sonido de perder
    public void Losing()
    {
        AudioSource.PlayClipAtPoint(lose, Camera.main.transform.position, 0.5f);
    }
}

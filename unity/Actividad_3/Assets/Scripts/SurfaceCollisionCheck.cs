using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceCollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto colisionando es un jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Habilitar su capacidad para saltar
            collision.gameObject.GetComponent<PlayerControl>().canJump = true;
            //  Debug.Log("habilitar salto");
        }
    }
}

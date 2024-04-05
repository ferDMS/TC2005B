using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    // Variable de velocidad del p�jaro definida despu�s de instanciarlo
    public float velocity;

    void Update()
    {
        // �nica l�nea necesaria para mover al p�jaro con cierta velocidad definida por el spawner.
        this.transform.position += Vector3.left * Time.deltaTime * velocity;
        // Si el p�jaro se va muy a la izquierda, eliminarlo
        if (transform.position.x < -30)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    // Detectar si chocamos con Nina
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cuando choque con el personaje, que se destruya
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reproducir sonido de moneda (hit)
            GameControl.Instance.SFXManager.getCoin();
            // Restar vidas
            GameControl.Instance.SpendLives();
            // Destruir p�jaro
            GameObject.Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    // Variable de velocidad del pájaro definida después de instanciarlo
    public float velocity;

    void Update()
    {
        // Única línea necesaria para mover al pájaro con cierta velocidad definida por el spawner.
        this.transform.position += Vector3.left * Time.deltaTime * velocity;
        // Si el pájaro se va muy a la izquierda, eliminarlo
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
            // Destruir pájaro
            GameObject.Destroy(this.gameObject);
        }
    }
}

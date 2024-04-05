using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    // Valor predeterminado para el tipo de ave
    public BirdName nameBird = BirdName.blue;

    // Slider de puntos entre 0 y 10
    // Esto es cuánto vale cada pájaro, se puede editar en Unity
    [Range(0, 10)] public int points = 0;

    // Enumeración de las tags dentro del juego
    public enum BirdName
    {
        blue, pollo, aguila, gallina, buho
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checar que el tag del elemento con el chocó sea el jugador
        // Solo si es con un jugador se va a destruir el objeto
        if (collision.gameObject.CompareTag("Player")) {
            // Sumar los puntos del pajaro a los puntos del uicontroller
            GameControl.Instance.uIController.AddPoints(points);
            // Destruir el objeto
            GameObject.Destroy(this.gameObject);
            // Para llamar el sonido del SFXManager de cuando se obtiene una moneda
            GameControl.Instance.SFXManager.getCoin();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

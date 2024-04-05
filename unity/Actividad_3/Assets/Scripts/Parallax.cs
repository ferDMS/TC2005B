using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startpos;
    public float parallaxEffect;
    public float shift = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar variables
        startpos = transform.position.x; // Posición inicial de sprite central
        // Checar su ancho para ver cada cuanto debemos hacer el efecto
        length = GetComponent<SpriteRenderer>().bounds.size.x * shift; 
    }

    // Update más lento que el original
    private void LateUpdate()
    {
        // Definir variables para cálculo de efecto parallax
        float temp = Camera.main.transform.position.x * (1 - parallaxEffect);
        float dist = Camera.main.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startpos + dist,
            transform.position.y, transform.position.z);
        // Ampliar hacia la derecha
        if (temp > startpos + length)
        {
            startpos += length;
        }
        // Ampliar hacia la izqiuerda
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}

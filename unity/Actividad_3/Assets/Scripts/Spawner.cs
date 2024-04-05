using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Variable donde se va a instanciar el p�jaro
    public GameObject birdGameObject;
    // M�ximo y m�nimo de altura
    public float maxHeight;
    public float minHeight;
    // Maximo y m�nimo de tiempo
    // Este define la dificultad del juego
    public float timeToSpawnMin;
    public float timeToSpawnMax;

    // Funci�n de Unity pero no se corre sola
    IEnumerator SpawnerTimer()
    {
        // Tiempo a esperar para generar nuevos p�jaros, generado aleatoriamente entre min y max
        yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax));
        // Iniciar instancia con altura aleatoria
        Instantiate(
            birdGameObject,
            new Vector3(
                transform.position.x,
                transform.position.y + Random.Range(minHeight, maxHeight),
                0),
            Quaternion.identity);
        // D�nde lo instanc�a
        // Como lo instanc�a
        // Qu� usa para instanciarlo: Quaternion, f�sica

        // Llamar nuevamente la rutina para crear un loop infinito
        StartCoroutine(SpawnerTimer());
    }

    void Start()
    {
        // Llamar la funci�n que no se corre sola de arriba por primera vez
        StartCoroutine(SpawnerTimer());
    }
}

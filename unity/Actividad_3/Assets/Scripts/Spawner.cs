using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Variable donde se va a instanciar el pájaro
    public GameObject birdGameObject;
    // Máximo y mínimo de altura
    public float maxHeight;
    public float minHeight;
    // Maximo y mínimo de tiempo
    // Este define la dificultad del juego
    public float timeToSpawnMin;
    public float timeToSpawnMax;

    // Función de Unity pero no se corre sola
    IEnumerator SpawnerTimer()
    {
        // Tiempo a esperar para generar nuevos pájaros, generado aleatoriamente entre min y max
        yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax));
        // Iniciar instancia con altura aleatoria
        Instantiate(
            birdGameObject,
            new Vector3(
                transform.position.x,
                transform.position.y + Random.Range(minHeight, maxHeight),
                0),
            Quaternion.identity);
        // Dónde lo instancía
        // Como lo instancía
        // Qué usa para instanciarlo: Quaternion, física

        // Llamar nuevamente la rutina para crear un loop infinito
        StartCoroutine(SpawnerTimer());
    }

    void Start()
    {
        // Llamar la función que no se corre sola de arriba por primera vez
        StartCoroutine(SpawnerTimer());
    }
}

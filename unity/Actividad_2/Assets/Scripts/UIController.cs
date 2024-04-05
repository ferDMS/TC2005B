using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Importar librería para usar una Unity UI
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text amountPoints;
    string amountText = "Points: ";
    int currentScore = 0;

    public void ActiveScore()
    {
        amountPoints.text = amountText + "--";
    }

    // _ porque la variable viene de otro lado
    // Se llama dentro de BirdBehaviour cuando existe un choque válido
    public void AddPoints(int _points)
    {
        currentScore += _points;
        printScore();
    }

    public void printScore()
    {
        amountPoints.text = amountText + currentScore.ToString();
        // Checar si ya ganamos o no dependiente de score actual
        GameControl.Instance.checkGameOver(currentScore);
    }

    // Start is called before the first frame update
    void Start()
    {
        ActiveScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

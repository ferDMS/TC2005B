using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Librería para manejar textos de UI
using UnityEngine.UI;

// Librería para conectarse a un servidor por HTTP
using UnityEngine.Networking;

// Librería para hacer parsing the JSON
using Newtonsoft.Json;

using System.Linq;

public class DialogueController : MonoBehaviour
{
    public Text pageText;
    private int pageNo;

    public Text dialogueText;
    string[] Sentences = { "", "", "", "", "" };

    private int Index = 0;

    // Tiempo a esperar entre caracteres al escribir una página, en segundos
    // Se usa para poder generar una animación de que se escribe el libro
    public float DialogueSpeed = 0.05f;

    // Tiempo a esperar antes de empezar a escribir una nueva página, en segundos
    public float DelayToWrite = 0.5f;

    private int book;


    // Start is called before the first frame update
    void Start()
    {
        book = PlayerPrefs.GetInt("book_no");

        // Llamar la función de obtener datos de las páginas del libro
        StartCoroutine(GetData());

        // Llamar la función para escribir el contenido de la página
        // Utiliza la variable de index, que salva la página en la que nos encontramos
        StartCoroutine(WriteSentence());
    }

    // Mi propia implementación de GetData, para obtener información de un solo libro
    // en vez de todos a través de una llamada API que regresa un único libro especificado
    IEnumerator GetData()
    {
        // Preparar llamada a API con URL, ignorando certificado SSL
        string JSONurl = "https://localhost:7166/api/book/" + book + "/pages";
        UnityWebRequest request = UnityWebRequest.Get(JSONurl);
        request.useHttpContinue = true;
        var cert = new ForceAceptAll();
        request.certificateHandler = cert;
        cert?.Dispose();

        // Hacer llamada a la API.
        yield return request.SendWebRequest();

        // Si falla, desplegar el error
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error Downloading: " + request.error);
        }
        // Si es exitosa, actualizar información con el libro seleccíonado
        else
        {
            // Convertir el JSON de la resupuesta a la estructura de libro y guardar la selección en variables PlayerPrefs
            List<Page> pageList = JsonConvert.DeserializeObject<List<Page>>(request.downloadHandler.text);
            
            // Dependiendo de cuantas páginas tenemos, estamos reestructurando el arreglo de dialogues
            if(pageList.Count > 0)
            {
                Sentences = Enumerable.Repeat<string>("", pageList.Count).ToArray<string>();

                // Actualizar lista de dialogues local con la lista de contenidos encontrados en la llamada a la API
                for (var i = 0; i < pageList.Count; i++)
                {
                    Sentences[i] = pageList[i].Content;
                }
            }
        }
    }

    IEnumerator WriteSentence()
    {
        pageNo = Index + 1;
        pageText.text = pageNo.ToString();
        yield return new WaitForSeconds(DelayToWrite);

        foreach(char Character in Sentences[Index].ToCharArray()) {
            dialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
    }

    // Función que se llamará por los botones para cambiar de página
    void NextSentence()
    {
        // Si existe una siguiente página posible de desplegar en
        // el índice (nuevo) al que buscamos ir
        if (Index < Sentences.Length && Index >= 0)
        {
            // Borrar
            dialogueText.text = Sentences[Index];
            dialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
    }

    // Función llamada por el botón de ir a siguiente página
    public void Next()
    {
        if (Index < Sentences.Length - 1)
        {
            StopAllCoroutines();
            Index++;
            NextSentence();
        }
    }

    public void Past()
    {
        if (Index >= 0 + 1)
        {
            StopAllCoroutines();
            Index--;
            NextSentence();
        }
    }
}

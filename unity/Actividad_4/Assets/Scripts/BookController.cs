using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class BookController : MonoBehaviour
{
    static public BookController Instance;
    public SelectCover SelectCover;
    // MySelectCover MySelectCover;
    public SelectBook SelectBook;
    public Text nameText;
    public Text authorText;
    public int BookSelection;

    public void Awake()
    {
        Instance = this;
        Instance.SetReferences();
        DontDestroyOnLoad(this.gameObject);

        // If returning from book preview, remain on the same book previewd before
        if (PlayerPrefs.HasKey("book_no"))
        {
            Select(PlayerPrefs.GetInt("book_no"));
        }
        // Set initial player prefs (variables across scenes) if first time opening library as first book
        else
        {
            Select(1);
        }
    }

    void SetReferences()
    {
        if (SelectCover == null)
        {
            SelectCover = FindObjectOfType<SelectCover>();
        }

        if (SelectBook == null)
        {
            SelectBook = FindObjectOfType<SelectBook>();
        }
    }

    public void Select(int _selection)
    {
        BookSelection = _selection;
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        string JSONurl = "https://localhost:7166/api/books";
        UnityWebRequest request = UnityWebRequest.Get(JSONurl);
        request.useHttpContinue = true;
        var cert = new ForceAceptAll();
        request.certificateHandler = cert;
        cert?.Dispose();

        yield return request.SendWebRequest();
        if(request.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log("Error Downloading: " + request.error);
        }
        else
        {
            List<Book> bookList = new List<Book>();
            bookList = JsonConvert.DeserializeObject<List<Book>>(request.downloadHandler.text);
            LoadBookInfo(BookSelection, bookList);
            PlayerPrefs.SetInt("book_no", BookSelection);
        }
    }

    public void LoadBookInfo(int idBook, List<Book> bookList)
    {
        // Aqui depende de como hayamos definido el nombre de la variable
        // que guarda el nombre del libro dentro de la estructura de Book
        string book = bookList[idBook - 1].Title;
        PlayerPrefs.SetString("book_name", book);
        nameText.text = book;

        // Al igual que arriba "Author" se usa porque así se llama la propiedad
        // dentro de la estructura de Book
        string author = bookList[idBook - 1].Author;
        PlayerPrefs.SetString("author", author);
        authorText.text = author;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void PreviewBook()
    {
        SceneManager.LoadScene("BookPreview");
    }

    public void MyLibrary()
    {
        SceneManager.LoadScene("My Library");
    }
}

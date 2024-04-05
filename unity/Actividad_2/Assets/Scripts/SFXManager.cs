using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip coins;
    public AudioClip endSound;

    public void getCoin()
    {
        AudioSource.PlayClipAtPoint(coins, Camera.main.transform.position, 0.5f);
    }

    public void endGame()
    {
        AudioSource.PlayClipAtPoint(endSound, Camera.main.transform.position, 0.5f);
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

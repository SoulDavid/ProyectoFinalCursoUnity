using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.Instance.SwapMusic(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PassToTheGame()
    {
        MusicManager.Instance.SwapMusic(0);
        SceneManager.LoadScene("Game");
    }
}

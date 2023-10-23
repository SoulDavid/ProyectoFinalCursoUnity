using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance;
    public static MusicManager Instance { get => instance; }

    [SerializeField] private AudioClip[] musicStates;
    private AudioSource myAudioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        SwapMusic(0);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapMusic(int state)
    {
        myAudioSource.clip = musicStates[state];
        myAudioSource.Play();
    }
}

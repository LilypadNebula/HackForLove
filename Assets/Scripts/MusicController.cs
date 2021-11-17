using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip angyMusic;
    public AudioClip mainMusic;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAngy()
    {
        source.clip = angyMusic;
        source.Play();
    }

    public void PlayMain()
    {
        source.clip = mainMusic;
        source.Play();
    }
}

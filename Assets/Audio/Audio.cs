using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update
    public static Audio Instance;
    public AudioClip clip1;
    public AudioClip clip2;
    private AudioSource player;
    void Start()
    {
        Instance = this;
        player = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {


        
    }
    public void AudioDie()
    {
        player.PlayOneShot(clip1);
        player.PlayOneShot(clip2);
    }
}

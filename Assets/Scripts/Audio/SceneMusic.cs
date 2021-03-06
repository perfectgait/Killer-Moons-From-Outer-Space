using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] string musicToPlay;

    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        if (!string.IsNullOrEmpty(musicToPlay))
        {
            audioManager.PlayMusic(musicToPlay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

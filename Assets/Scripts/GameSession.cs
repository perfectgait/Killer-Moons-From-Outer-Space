using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.PlayMusic("Level 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

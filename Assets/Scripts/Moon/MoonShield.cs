using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonShield : MonoBehaviour
{
    [SerializeField] string sfxName;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager.PlaySoundEffect(sfxName);
    }
}

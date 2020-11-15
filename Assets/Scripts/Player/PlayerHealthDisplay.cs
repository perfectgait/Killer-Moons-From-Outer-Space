using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    private Player player;
    private Text playerHealthText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerHealthText = GetComponent<Text>();

        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (player)
        {
            playerHealthText.text = player.GetHealth().ToString();
        }
        else
        {
            playerHealthText.text = "0";
        }
    }
}

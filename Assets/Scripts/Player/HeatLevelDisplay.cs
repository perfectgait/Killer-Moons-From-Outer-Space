using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatLevelDisplay : MonoBehaviour
{
    private Player player;
    private Minigun minigun;
    private Slider heatLevelSlider;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        minigun = FindObjectOfType<Minigun>();
        heatLevelSlider = GetComponent<Slider>();

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
            heatLevelSlider.value = player.GetHeatLevel();
        }
        else
        {
            heatLevelSlider.value = 0.0f;
        }
    }
}

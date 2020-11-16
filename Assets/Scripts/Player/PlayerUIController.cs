using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] Slider heatLevelSlider;

    // Start is called before the first frame update
    void Start()
    {
        heatLevelSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowHeatLevelSlider()
    {
        heatLevelSlider.gameObject.SetActive(true);
    }
}

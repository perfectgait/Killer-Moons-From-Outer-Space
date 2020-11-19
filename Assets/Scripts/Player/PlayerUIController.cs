using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] Slider heatLevelSlider;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        heatLevelSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.timeSinceLevelLoad;
    }

    public void ShowHeatLevelSlider()
    {
        heatLevelSlider.gameObject.SetActive(true);
    }

#if UNITY_EDITOR
    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        GUI.Label(new Rect(10, 40, 1000, 1000), niceTime);
    }
#endif
}

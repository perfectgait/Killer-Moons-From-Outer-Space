using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] string healthBarName;

    private Health health;
    private Slider healthBar;
    private bool hasHealthBar = false;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        if (!string.IsNullOrEmpty(healthBarName))
        {
            healthBar = GameObject.Find(healthBarName).GetComponent<Slider>();
            hasHealthBar = true;
            healthBar.maxValue = health.GetHealth();
        }

        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHealthBar)
        {
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        healthBar.value = health.GetHealth();
    }
}

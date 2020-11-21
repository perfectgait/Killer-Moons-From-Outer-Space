using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] GameObject objectWithHealth;

    private Slider healthBar;
    private Health objectHealth;
    private bool hasNecessaryComponents = false;

    // Start is called before the first frame update
    void Start()
    {
        if (objectWithHealth == null)
        {
            Debug.LogError("Health Bar needs a GameObject with a Health component to function properly");
            return;
        }
        objectHealth = objectWithHealth.GetComponent<Health>();
        if (objectHealth == null)
        {
            Debug.LogError("Health Bar needs a GameObject with a Health component to function properly");
            return;
        }

        hasNecessaryComponents = true;
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = objectHealth.GetHealth();
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasNecessaryComponents)
        {
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        healthBar.value = objectHealth.GetHealth();
    }
}

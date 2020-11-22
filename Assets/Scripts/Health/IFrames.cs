using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFrames : MonoBehaviour
{
    [SerializeField] float iframeDuration;

    private bool isInvulnerable;
    private int currentIframe;

    // Start is called before the first frame update
    void Start()
    {
        isInvulnerable = false;
        currentIframe = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvulnerable)
        {
            currentIframe++;
        }

        if (currentIframe >= iframeDuration)
        {
            currentIframe = 0;
            isInvulnerable = false;
        }
    }

    public void Damage()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            currentIframe = 0;
        }
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }
}

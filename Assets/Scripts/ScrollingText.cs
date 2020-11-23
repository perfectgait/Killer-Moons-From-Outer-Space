using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] float startTime = 2f;
    [SerializeField] float endTime = 10f;
    [SerializeField] float scrollSpeed = 1f;
    [SerializeField] bool shouldHideNavigationUntilEnd = true;

    GameObject navigation;

    private float currentScrollSpeed;
    private float scrollSpeedIncrease = 5f;

    // Start is called before the first frame update
    void Start()
    {
        navigation = GameObject.Find("Navigation");
        navigation.SetActive(!shouldHideNavigationUntilEnd);

        currentScrollSpeed = scrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBeforeScrollStart())
        {
            return;
        }

        HandleScrollSpeed();
        HandleHiddenNavigation();
        ScrollText();
    }

    private bool IsBeforeScrollStart()
    {
        return Time.timeSinceLevelLoad <= startTime;
    }

    // TODO: Since this is entirely time based, we won't stop
    // scrolling at the right point if we've pressed the button to speed it up
    private bool ShouldStopScrolling()
    {
        return Time.timeSinceLevelLoad >= endTime;
    }

    private void HandleScrollSpeed()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            currentScrollSpeed = scrollSpeed * scrollSpeedIncrease;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            currentScrollSpeed = scrollSpeed;
        }
    }

    private void HandleHiddenNavigation()
    {
        if (shouldHideNavigationUntilEnd && ShouldStopScrolling())
        {
            navigation.transform.position = new Vector2(transform.position.x, -0.55f);
            navigation.SetActive(true);
        }
    }

    private void ScrollText()
    {
        if (!ShouldStopScrolling())
        {
            transform.position += Vector3.up * currentScrollSpeed * Time.deltaTime;
        }
    }
}

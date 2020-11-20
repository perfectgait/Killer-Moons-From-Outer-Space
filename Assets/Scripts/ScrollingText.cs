using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] float startTime = 2f;
    [SerializeField] float endTime = 10f;
    [SerializeField] float scrollSpeed = 1f;

    GameObject navigation;

    // Start is called before the first frame update
    void Start()
    {
        navigation = GameObject.Find("Navigation");
        navigation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad <= startTime)
        {
            return;
        }


        if (Time.timeSinceLevelLoad >= endTime)
        {
            navigation.transform.position = new Vector2(transform.position.x, -0.55f);
            navigation.SetActive(true);
        }
        else
        {
            transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
        }
    }
}

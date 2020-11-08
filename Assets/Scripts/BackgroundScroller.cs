using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    Material backgroundMaterial;
    //Vector2 offset;


    // Start is called before the first frame update
    void Start()
    {
        backgroundMaterial = GetComponent<MeshRenderer>().material;
        //offset = new Vector2(scrollSpeed, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //localOffset = new Vector2()
        //backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;

        float offset = scrollSpeed * Time.time;
        backgroundMaterial.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}

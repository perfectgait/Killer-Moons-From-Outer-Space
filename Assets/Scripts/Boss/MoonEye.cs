using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonEye : MonoBehaviour
{
    private Player player;
    private bool shouldFollowPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        shouldFollowPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFollowPlayer && player)
        {
            FollowPlayer();
        }
    }

    public void StopFollowingPlayer()
    {
        shouldFollowPlayer = false;
    }

    private void FollowPlayer()
    {
        // @see https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
        Vector3 difference = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Subtract 180 degrees so the eye faces the player on its left
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 180);
    }
}

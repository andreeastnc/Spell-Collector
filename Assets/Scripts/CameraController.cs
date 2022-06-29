using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour
{
    private Transform player;
    private float smoothing = 10f;

    private float minX = -1;
    private float maxX = 93;
    private float minY = 0;
    private float maxY = 18;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;  
        if (SceneManager.GetActiveScene().name == "Level2v1")
        {
            minX = -1;
            maxX = 93;
            minY = 0;
            maxY = 18;
        }
        else if (SceneManager.GetActiveScene().name == "Level2v2")
        {
            minX = -7;
            maxX = 114;
            minY = 0;
            maxY = 18;
        }
        else if (SceneManager.GetActiveScene().name == "Level2v3")
        {
            minX = -1;
            maxX = 114;
            minY = 0;
            maxY = 18;
        }
    }

    void LateUpdate()
    {
        if (transform.position != player.position)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y + 1, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}

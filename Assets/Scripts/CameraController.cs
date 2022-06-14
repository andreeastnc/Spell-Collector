using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private float smoothing = 10f;

    private float minX = -1;
    private float maxX = 132;
    private float minY = 0;
    private float maxY = 18;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
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

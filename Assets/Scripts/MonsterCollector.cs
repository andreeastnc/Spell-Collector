using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Destroy(collision.gameObject);
        }
    }
}

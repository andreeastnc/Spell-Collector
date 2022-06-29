using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;

    private Animator anim = null;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            if (anim.runtimeAnimatorController.name != "Mushroom")
                anim.SetBool("walking", false);
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
        if (anim.runtimeAnimatorController.name != "Mushroom")
            anim.SetBool("walking", true);
    }
}

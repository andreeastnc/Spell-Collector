using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomSide;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonster());
    }

    // Update is called once per frame
    IEnumerator SpawnMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 4));

            randomSide = Random.Range(0, 2);

            spawnedMonster = Instantiate(monster);

            if (randomSide == 0) //left side
            {
                spawnedMonster.transform.position = leftPos.position;
                spawnedMonster.GetComponent<MonsterMovement>().speed = Random.Range(5, 12);
            }
            else //right side
            {
                spawnedMonster.transform.position = rightPos.position;
                spawnedMonster.GetComponent<MonsterMovement>().speed = -Random.Range(5, 12);
                spawnedMonster.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}

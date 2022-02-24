using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//helper script to make sure spawning enemies does not exceed a certain number. also keep track of how much enemies there are.

public class Spawner : MonoBehaviour {
    public int numEnemies;

    public GameObject enemyPrefab;
    public GameObject bystander;
    public GameObject crateEnemy;
    public GameObject _enemy;


    // Use this for initialization
    void Start () {
        numEnemies = 1;
	}

    public void spawnEnemy(Vector3 loc)
    {
        if(numEnemies < 7)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = loc;
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
            numEnemies++;
        }
    }

    public void spawnBystander(Vector3 loc)
    {
        if(numEnemies < 7)
        {
            _enemy = Instantiate(bystander) as GameObject;
            _enemy.transform.position = loc;
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
            numEnemies++;
        }
    }

    public void spawnCrateEnemy()
    {
        if (numEnemies < 7)
        {
            _enemy = Instantiate(crateEnemy) as GameObject;
            Debug.Log("Spawned Enemy2");
            int chooseLocation = Random.Range(1, 2);
            if (chooseLocation == 1)
            {
                _enemy.transform.position = new Vector3(2.91f, 1, -6.95f);
            }
            else
            {
                _enemy.transform.position = new Vector3(6.61f, 1, 1.67f);
            }
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
            numEnemies++;
        }
    }
}

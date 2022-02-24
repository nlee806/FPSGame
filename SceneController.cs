using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//spawns enemies at the start of the game.
public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bystander;
    [SerializeField] private GameObject crateEnemy;
    [SerializeField] private GameObject bossPrefab;
    public int numberEnemies = 0;
    public int numToBoss = 0;

    private GameObject enemySpawn;
    private GameObject bySpawn;
    private GameObject crateSpawns;


    private GameObject _enemy;


    private void Update()
    {
        if(numberEnemies < 10)
        {
            int number = Random.Range(1, 3);
            if(number == 1)
            {
                _enemy = Instantiate(enemyPrefab) as GameObject;
                numberEnemies++;
            }
            else if (number == 2)
            {
                _enemy = Instantiate(bystander) as GameObject;
                numberEnemies++;
            }
        }
        if ((numToBoss/2)>10) {//kill 10 people, even bystanders
                Messenger.Broadcast(GameEvent.LEVEL);
            _enemy = Instantiate(bossPrefab) as GameObject;
            //boss level

        }

    }

    private void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("enemyspawn").Length; i++){
            enemySpawn = GameObject.FindGameObjectsWithTag("enemyspawn")[i];
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = enemySpawn.transform.position;
            numberEnemies++;

        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("bystanderspawn").Length; i++)
        {
            bySpawn = GameObject.FindGameObjectsWithTag("bystanderspawn")[i];
            _enemy = Instantiate(bystander) as GameObject;
            _enemy.transform.position = bySpawn.transform.position;
            numberEnemies++;

        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("crateenemyspawn").Length; i++)
        {
            Debug.Log("found crate enemies");
            crateSpawns = GameObject.FindGameObjectsWithTag("crateenemyspawn")[i];
            _enemy = Instantiate(crateEnemy) as GameObject;
            _enemy.transform.position = crateSpawns.transform.position;
            numberEnemies++;
        }




        Debug.Log("spawning enemy");

    }

    public void respawn()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("enemyspawn").Length; i++)
        {
            enemySpawn = GameObject.FindGameObjectsWithTag("enemyspawn")[i];
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = enemySpawn.transform.position;
            numberEnemies++;

        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("bystanderspawn").Length; i++)
        {
            bySpawn = GameObject.FindGameObjectsWithTag("bystanderspawn")[i];
            _enemy = Instantiate(bystander) as GameObject;
            _enemy.transform.position = bySpawn.transform.position;
            numberEnemies++;

        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("crateenemyspawn").Length; i++)
        {
            Debug.Log("found crate enemies");
            crateSpawns = GameObject.FindGameObjectsWithTag("crateenemyspawn")[i];
            _enemy = Instantiate(crateEnemy) as GameObject;
            _enemy.transform.position = crateSpawns.transform.position;
            numberEnemies++;
        }




        Debug.Log("spawning enemy");
    }
}
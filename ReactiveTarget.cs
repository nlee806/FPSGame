using UnityEngine;
using System.Collections;

//sets behavior of enemies when they die.
public class ReactiveTarget : MonoBehaviour
{
    public int injure = 0;
    public GameObject controller;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bystander;
    private GameObject _enemy;
    private GameObject enemySpawn;


    public void Start()
    {
        controller = GameObject.FindGameObjectWithTag("controller");
    }

    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            Debug.Log("Killed Enemy");
            behavior.SetAlive(false);
        }
        Debug.Log("Enemy Dead");
        StartCoroutine(Die());
        Debug.Log("New Enemy Spawned");
        StartCoroutine(spawnEnemy());
    }

    private IEnumerator Die()
    {
        Spawner spawnSript = controller.GetComponent<Spawner>();
        spawnSript.numEnemies--;

        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }

    private IEnumerator spawnEnemy()
    {
        Spawner spawnSript = controller.GetComponent<Spawner>();

        int number = Random.Range(1, 3);
        for (int i = 0; i < number; i++)
        {
            int spawnSlot = Random.Range(0, GameObject.FindGameObjectsWithTag("enemyspawn").Length);
            Vector3 spawnLocation = GameObject.FindGameObjectsWithTag("enemyspawn")[spawnSlot].transform.position;

            int enemyType = Random.Range(1, 3);
            if (enemyType == 1)
            {
                spawnSript.spawnEnemy(spawnLocation);
            }
            else if (enemyType == 2) {
                spawnSript.spawnCrateEnemy();
            }
            else
            {
                spawnSript.spawnBystander(spawnLocation);
            }
            yield return new WaitForSeconds(3);
        }

        
    }
}

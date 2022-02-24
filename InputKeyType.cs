using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//resets the game. Moves player back to spawn and give player full health.
public class InputKeyType : MonoBehaviour {

    public GameObject player;
    public GameObject spawn;

    public GameObject[] enemy;
    public GameObject[] crateEnemy;
    public GameObject[] bystander;
    public GameObject controller;
    public SceneController sceneScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("spawn");
        sceneScript = controller.GetComponent<SceneController>();

    }
    // Update is called once per frame
    void Update () {
        

        if (Input.GetButtonDown("reset"))
        {
            enemy = GameObject.FindGameObjectsWithTag("enemy");
            for(int i = 0; i < enemy.Length; i++)
            {
                Destroy(enemy[i]);
            }

            crateEnemy = GameObject.FindGameObjectsWithTag("crateenemy");
            for (int i = 0; i < crateEnemy.Length; i++)
            {
                Destroy(crateEnemy[i]);
            }

            bystander = GameObject.FindGameObjectsWithTag("bystander");
            for (int i = 0; i < bystander.Length; i++)
            {
                Destroy(bystander[i]);
            }
            sceneScript.respawn();

            PlayerCharacter playerScript = player.GetComponent<PlayerCharacter>();
            playerScript._health = 25;
            playerScript.Reset();

            player.transform.position = spawn.transform.position;

            Debug.Log(playerScript._health);
        }
	}
}

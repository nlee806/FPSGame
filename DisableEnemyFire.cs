using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//disables enemy fire when player is dead.

public class DisableEnemyFire : MonoBehaviour {

    bool dead = false;
    WanderingAI deadenemyfire;
	
	// Update is called once per frame
	void Update () {
        if (dead == true)
        {
            deadenemyfire = GetComponent<WanderingAI>();
            deadenemyfire.PlayerDead();
            Debug.Log("enemy fire disabled");
        }
    }

    public void Death() {
        dead = true;
    }
}

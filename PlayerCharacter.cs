using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


//perform player behaviors such as taking damage. keeps track of whether player is alive or current health of player.
public class PlayerCharacter : MonoBehaviour
{
    public int _health;
    FPSInput deadcontrols;
    RayShooter deadgun;
    public bool isAlive;
    GameObject[] enemy1;
    GameObject[] crateEnemy;
    WanderingAI_original wao;
    CrateEnemyBehavior ceb;

    public AudioSource soundSource;
    public AudioClip playerDieSound;


    //    DisableEnemyFire disableenemyfire;

    void Start()
    {
        _health = 25;
        isAlive = true;
        enemy1 = GameObject.FindGameObjectsWithTag("enemy");
        crateEnemy = GameObject.FindGameObjectsWithTag("enemy");
        
    }

    public void Hurt(int damage)
    {
        
        if (_health <= 0)
        {
            soundSource.clip = playerDieSound;
            StartCoroutine(delayedSwitch());

            Debug.Log("Game Over");
            Messenger.Broadcast(GameEvent.GAME_OVER);
            isAlive = false;
            deadcontrols = GetComponent<FPSInput>();
            deadcontrols.PlayerDead();
            deadgun = GetComponentInChildren<RayShooter>();
            deadgun.PlayerDead();
            //disable enemy fire
            for (int a = 0; a < enemy1.Length; a++)
            {
                GameObject nearbyEnemy = enemy1[a];
                    wao = nearbyEnemy.GetComponent<WanderingAI_original>();
                    wao.disableFire = true;
                    Debug.Log("Enemy Fire Disabled");
            }            
                for (int a = 0; a < crateEnemy.Length; a++)
            {
                GameObject nearbyEnemy = crateEnemy[a];

                    ceb = nearbyEnemy.GetComponent<CrateEnemyBehavior>();
                    ceb.disableFire = true;
                    Debug.Log("Crate Enemy Fire Disabled");
            }

            //            disableenemyfire = GetComponent<DisableEnemyFire>();
            //            disableenemyfire.Dead();

            //stop game, stop processing player moves, player shooting, enemy attack
            //player rotation, enemy moves allowed

            
            SceneManager.LoadScene("gameoverscene");
        }
        else
        {
            _health -= damage;
            Messenger.Broadcast(GameEvent.HEALTHNEG);
            Debug.Log("Health: " + _health);
        }
    }

    IEnumerator delayedSwitch()
    {
        soundSource.Play();
        yield return new WaitForSeconds(playerDieSound.length / 2);
    SceneManager.LoadScene("gameoverscene");
    }
    public void Help(int assistance) {
        _health += assistance;
        Messenger.Broadcast(GameEvent.HEALTHPOS);
        Debug.Log("Health: " + _health);
    }
    public void Reset() {
        isAlive = true;
        deadcontrols = GetComponent<FPSInput>();
        deadcontrols.PlayerNotDead();
        deadgun = GetComponentInChildren<RayShooter>();
        deadgun.PlayerNotDead();
        for (int a = 0; a < enemy1.Length; a++)
        {
            GameObject nearbyEnemy = enemy1[a];
            if ((nearbyEnemy.transform.position - this.transform.position).sqrMagnitude < 10 * 10)
            {
                WanderingAI_original wao = nearbyEnemy.GetComponent<WanderingAI_original>();
                wao.disableFire = false;
                Debug.Log("Enemy Fire Disabled");
            }
        }

        for (int a = 0; a < crateEnemy.Length; a++)
        {
            GameObject nearbyEnemy = crateEnemy[a];
            if ((nearbyEnemy.transform.position - this.transform.position).sqrMagnitude < 10 * 10)
            {
                CrateEnemyBehavior ceb = nearbyEnemy.GetComponent<CrateEnemyBehavior>();
                ceb.disableFire = false;
                Debug.Log("Crate Enemy Fire Disabled");
            }
        }
    }

}

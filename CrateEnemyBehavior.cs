using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Crate Enemies should stand behind the crates and shoot fireballs at the player when they get close.
public class CrateEnemyBehavior : MonoBehaviour {

    public GameObject[] alertedEnemy1;
    public GameObject[] alertedCrateEnemy;
    public GameObject player;
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
    private bool _alive;
    public float obstacleRange = 5.0f;
    public float fireRate = 0.75F;
    private float nextFire = 0.0F;
    private GameObject controller;
    public bool alert;
    public bool disableFire = false;
    private GameObject nearbyEnemy;

    // Use this for initialization
    void Start () {
        _alive = true;
        alert = false;
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.FindGameObjectWithTag("controller");
        alertedEnemy1 = GameObject.FindGameObjectsWithTag("enemy");
        alertedCrateEnemy = GameObject.FindGameObjectsWithTag("enemy");
        Spawner spawnSript = controller.GetComponent<Spawner>();
        spawnSript.numEnemies++;
    }
	
	// Update is called once per frame
	void Update () {
		if (_alive){
            if (((player.transform.position - this.transform.position).sqrMagnitude < 7 * 7) || alert == true)
            {
                this.transform.LookAt(player.transform);
                Ray ray = new Ray(transform.position, transform.forward);

                //alert other enemies around it
                for (int a = 0; a < alertedEnemy1.Length; a++)
                {
                    nearbyEnemy = alertedEnemy1[a];
                    if ((nearbyEnemy.transform.position - this.transform.position).sqrMagnitude < 7 * 7)
                    {
                        WanderingAI_original wao = nearbyEnemy.GetComponent<WanderingAI_original>();
                        wao.alertMe();
                        wao.alert = true; //send alert
                    }
                }
                for (int a = 0; a < alertedCrateEnemy.Length; a++)
                {
                    nearbyEnemy = alertedCrateEnemy[a];
                    if ((nearbyEnemy.transform.position - this.transform.position).sqrMagnitude < 12 * 12)
                    {
                        CrateEnemyBehavior ceb = nearbyEnemy.GetComponent<CrateEnemyBehavior>();
                        ceb.alertMe();
                        Debug.Log("Alert Sent");
                        ceb.alert = true; //send alert
                    }
                }

                RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                if (_fireball == null && disableFire == false)
                {

                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.0f);
                    _fireball.transform.rotation = transform.rotation;
                }

                //else if (hit.distance < obstacleRange)
                //{
                //    float angle = Random.Range(-110, 110);
                //    transform.Rotate(0, angle, 0);
                //}
            }

            }


        }
        alert = false; //stop being alerted when out of range
	}
    public void alertMe()
    {
        Debug.Log("Alerted Crate Enemy!");
    }
}

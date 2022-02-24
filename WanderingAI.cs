using UnityEngine;
using System.Collections;

//allows the enemies to move around and shoot at the player.
public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public GameObject player;
    public float fireRate = 0.7f;
    private float nextFire = 0.0F;


    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
    

    private bool _alive;

    bool playerdead = false;

    void Start()
    {
        _alive = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_alive)
        {
            if ((player.transform.position - this.transform.position).sqrMagnitude < 5* 5)
            {
                this.transform.LookAt(player.transform);
                Ray ray = new Ray(transform.position, transform.forward);
                if (playerdead == false)
                {
                    RaycastHit hit;
                    if (Physics.SphereCast(ray, 0.75f, out hit) && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        GameObject hitObject = hit.transform.gameObject;
                        if (hitObject.GetComponent<PlayerCharacter>())
                        {
                            if (_fireball == null)
                            {

                                _fireball = Instantiate(fireballPrefab) as GameObject;
                                _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.0f);
                                _fireball.transform.rotation = transform.rotation;
                            }
                        }
                        else if (hit.distance < obstacleRange)
                        {
                            float angle = Random.Range(-110, 110);
                            transform.Rotate(0, angle, 0);
                        }
                    }
                }
            }
            else
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    public void PlayerDead() {
        playerdead = true;
    }
}

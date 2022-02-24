using UnityEngine;
using System.Collections;

//set behavior of enemies

public class WanderingAI_original : MonoBehaviour {
	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;
    public GameObject player;

    private bool _alive;
    public bool alert;
    public float fireRate = 0.5f;
    private float nextFire = 0.0F;
    private PlayerCharacter playerScript;
    public bool disableFire = false;
    public GameObject[] platform;
    Vector3 direction = new Vector3(0, 0, 0);
    private bool grounded = true;
    private CharacterController _charController;

    void Start() {
		_alive = true;
        _charController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerCharacter>();
        platform = GameObject.FindGameObjectsWithTag("platform");
        alert = false;
    }
	
	void Update() {
		if (_alive) {
            //allow for jumping
            for (int a = 0; a < platform.Length; a++) {
                GameObject jumpMe = platform[a];
                Vector3 dir = (jumpMe.transform.position - this.transform.position).normalized;
                float dot = Vector3.Dot(dir, this.transform.forward);
                if (((jumpMe.transform.position - this.transform.position).sqrMagnitude < 2 * 2) && (dot == 1)) {
                    Debug.Log("Spotted Jumping Platform");
                    if (_charController.isGrounded)
                    {
                        direction.y = 4f;
                        grounded = false;
                        Debug.Log("Enemy Jumping");
                        direction.y -= -9.8f * Time.deltaTime;
                        Vector3 _velocity = (direction) * speed;
                        _charController.Move(_velocity * Time.deltaTime);
                    }
                if (_charController.isGrounded)
                {
                    grounded = true;
                }
            }

            }
            //look for player
            if (((player.transform.position - this.transform.position).sqrMagnitude < 7 * 7 && playerScript.isAlive)||(alert==true&&playerScript.isAlive)){
                this.transform.LookAt(player.transform);
                transform.Translate(0, 0, speed * Time.deltaTime);

                if(Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    if (_fireball == null&&disableFire==false)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }

                alert = false; //reset if alert is out of range
            }
            else
            {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null&&disableFire==false)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
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
	}
    public void alertMe() {
        Debug.Log("Alerted Enemy!");
    }
	public void SetAlive(bool alive) {
		_alive = alive;
	}
}

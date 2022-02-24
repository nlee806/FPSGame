using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


//switches the gun of the player and allows the player to shoot.
public class RayShooter : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource; //player object
    [SerializeField] private AudioClip hitWallSound; //audio clip
    [SerializeField] private AudioClip hitEnemySound; //audio clip

    private Camera _camera;
    Vector3 sphereSize = new Vector3(0.5f, 0.5f, 0.2f);
    private int switcher = 0;
    bool playerdead = false;
    private bool rapidfire;
    public ParticleSystem muzzleFlash;
    public GameObject bulletImpact;

    //    int injure = 0;

    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rapidfire = false;
        soundSource.volume = 1;        
    }

    void OnGUI()
    {
        int size = 12; 
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update(){
        //toggles gun, bigger bullets on right click
        if (Input.GetMouseButtonDown(1)) {
            if (!rapidfire)
            {
                rapidfire = true;
                sphereSize = new Vector3(0.3f, 0.3f, 0.3f);
                Debug.Log("Full auto on");
            }
            else if (rapidfire) {
                rapidfire = false;
                sphereSize = new Vector3(0.5f, 0.5f, 0.2f);
                Debug.Log("Full auto off");
            }
            switcher = switcher+1;
        }

        if (rapidfire)
        {
            if (Input.GetMouseButton(0))
            {
                if (playerdead == false)
                {
                    muzzleFlash.Play();
                    Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
                    Ray ray = _camera.ScreenPointToRay(point);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject hitObject = hit.transform.gameObject;
                        EnemyDie target = hitObject.GetComponent<EnemyDie>();
                        if (target != null)
                        {
                            print("reacting");
                            target.reactToHit();
                            AudioSource.PlayClipAtPoint(hitEnemySound, transform.position);
                            soundSource.PlayOneShot(hitEnemySound);
                            
                            // soundSource.clip = hitEnemySound;
                            // soundSource.Play();

                            //target.injure = target.injure + 1;
                            //if (target.injure == 2)
                            //{
                            //    target.ReactToHit();
                            //}
                            //else if (target.injure == 1)
                            //{
                            //    //change to injured material
                            //    Debug.Log("Injured enemy!");
                            //    //change to injured material
                            //}
                            //else
                            //{

                            //}
                        }
                        else
                        {

                            StartCoroutine(SphereIndicatorSlow(hit.point));
                            soundSource.PlayOneShot(hitWallSound);

                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (playerdead == false)
                {
                    muzzleFlash.Play();
                    Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
                    Ray ray = _camera.ScreenPointToRay(point);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject hitObject = hit.transform.gameObject;
                        EnemyDie target = hitObject.GetComponent<EnemyDie>();
                        print("working");
                        print(hitObject);

                        if (target != null)
                        {
                            print("reacting");
                            target.reactToHit();
                            AudioSource.PlayClipAtPoint(hitEnemySound, transform.position);
                            soundSource.PlayOneShot(hitEnemySound);
                            
                            // soundSource.clip = hitEnemySound;
                            // soundSource.Play();

                            //target.reactToHit();
                            target.injure = target.injure + 1;
                            if (target.injure == 2)
                            {
                                target.reactToHit();
                            }
                            else if (target.injure == 1)
                            {
                                //change to injured material
                                Debug.Log("Injured enemy!");
                                //change to injured material
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            StartCoroutine(SphereIndicator(hit.point));
                            soundSource.PlayOneShot(hitWallSound);
                        }
                    }
                }
            }
        }

    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.Instantiate(bulletImpact);
        sphere.transform.position = pos;
        sphere.transform.localScale = sphereSize;

        yield return new WaitForSeconds(2);

        Destroy(sphere);
    }

    private IEnumerator SphereIndicatorSlow(Vector3 pos)
    {
        GameObject sphere = GameObject.Instantiate(bulletImpact);
        sphere.transform.position = pos;
        sphere.transform.localScale = sphereSize;

        StartCoroutine(slowFireRate());

        yield return new WaitForSeconds(2);

        Destroy(sphere);
    }

    private IEnumerator slowFireRate()
    {

        yield return new WaitForSeconds(3);
    }
    public void PlayerDead() {
        playerdead = true;
    }
    public void PlayerNotDead() {
        playerdead = false;
    }
    public void setAlive()
    {
        playerdead = false;
    }
}
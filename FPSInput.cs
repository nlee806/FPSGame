using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


// basic WASD-style movement control, handles running
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float runspeed = 12.0f;
    bool runswitch = false;
    bool duckswitch = false;
    bool jumpswitch = false;
    public float crouchspeed = 3.0f;
    public float gravity = -9.8f;
    public bool playerdead = false;
    int countTime;

    private CharacterController _charController;
    //   private FirstPersonController chMotor;
    /*   private Transform tr;
       private float dist;
       private bool crouching = false;*/
    private float dist;
    private float normalHeight;
    private bool grounded = true;
    private float timeHold;
    private bool timeKeep;
    Camera mainCamera;

    float rotationSpeed = 1000f;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cameraObject;
    [SerializeField] private Camera cameraCloseObject;
    bool introScene;
    bool introSceneClose;
    bool winner;
    Vector3 direction = new Vector3(0,0,0);
    private Stopwatch timer;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        //       chMotor = GetComponent<CharacterMotor>();
        /*       tr = transform;*/
        normalHeight = _charController.height;
        dist = (_charController.height / 2)+0.2f; //calculate distance to ground
        mainCamera = GetComponentInChildren<Camera>();
        mainCamera.gameObject.SetActive(false);
        cameraCloseObject.gameObject.SetActive(false);
        cameraObject.gameObject.SetActive(true);
        introScene = true;
        introSceneClose = true;
        winner = false;
        timeKeep = true;
        countTime = 0;
    }

    void Update()
    {
        //Vector3 myLocation = transform.position;
        //SaveLoad.SaveLocation(myLocation);
        //STARTING ANIMATION: 
 //Note: the wrong camera is on this animation, supposed to be up panning down on scene, not at ground level.
        if (introScene == true && introSceneClose == true && Time.time <= 15)
        { 
            cameraObject.transform.RotateAround(Vector3.zero, Vector3.up, (20 * Time.deltaTime));
        }
        if (introScene == true && introSceneClose == true && Time.time > 15) {
            introScene = false;
                introSceneClose = false;
              //  cameraCloseObject.gameObject.SetActive(false);
                cameraObject.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
        }
        winner = UIController.Win();
        if (winner == true) { //ENDING ANIMATION
            if (timeKeep == true)
            {
                //  timeHold = Time.time;
                // timer.Start();
                cameraObject.gameObject.SetActive(true);
                mainCamera.gameObject.SetActive(false);
                timeKeep = false;
            }
            float xValue = player.transform.position.x;
            float yValue = player.transform.position.y;
            float zValue = player.transform.position.z;
            Vector3 rotateAroundMe = new Vector3(xValue, yValue, zValue);
            Vector3 poseCamera = new Vector3(xValue, yValue, zValue + 7);
            cameraObject.transform.position = poseCamera;
            if (countTime <= 1000) {
                cameraObject.transform.RotateAround(rotateAroundMe, Vector3.up, 20 * Time.deltaTime);
                countTime++;
                if (countTime == 1000) {
                    cameraObject.gameObject.SetActive(false);
                    mainCamera.gameObject.SetActive(true);
                }
            }
        }

        if (introScene == false && introSceneClose == false && winner == false)
        {

            if (Input.GetKeyDown(KeyCode.I))
            { //help
              //Messenger.Broadcast(GameEvent.);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            { //settings
                Messenger.Broadcast(GameEvent.OPEN_MENU);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            { //quit
              //Messenger.Broadcast(GameEvent.LEVEL);
              //prompt save
                Application.Quit();
            }

            if (playerdead == false)
            {
                //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);            
                //allows running when pressing left alt
                if (Input.GetKeyDown(KeyCode.LeftAlt) && _charController.isGrounded)
                {
                    if (runswitch == false && duckswitch != true)
                    {
                        UnityEngine.Debug.Log("Running");
                        speed = runspeed;
                        runswitch = true;
                    }
                    else if (runswitch == true)
                    {
                        UnityEngine.Debug.Log("Walking");
                        speed = 6.0f;
                        runswitch = false;
                    }
                }

                //allows ducking when pressing Left Control
                if (Input.GetKeyDown(KeyCode.LeftControl) && _charController.isGrounded)
                {
                    if (duckswitch == false && runswitch != true)
                    {
                        UnityEngine.Debug.Log("Ducking");
                        speed = crouchspeed;
                        _charController.height = dist;
                        mainCamera.transform.localPosition = new Vector3(0, -0.3f, 0);
                        duckswitch = true;
                    }
                    else if (duckswitch == true)
                    {
                        UnityEngine.Debug.Log("Standing Up");
                        speed = 6.0f;
                        _charController.height = normalHeight;
                        mainCamera.transform.localPosition = new Vector3(0, 0.3f, 0);
                        duckswitch = false;
                    }

                }

                //allows jumping when pressing Space
                if (Input.GetButtonDown("Jump") && _charController.isGrounded)
                {
                    direction.y = 3f;
                    grounded = false;
                    UnityEngine.Debug.Log("Jumping");
                    direction.y -= gravity * Time.deltaTime;
                    Vector3 _velocity = (direction) * 6.0f;
                    _charController.Move(_velocity * Time.deltaTime);
                }
                if (_charController.isGrounded)
                {
                    grounded = true;
                }

                float deltaX = Input.GetAxis("Horizontal") * speed;
                float deltaZ = Input.GetAxis("Vertical") * speed;
                Vector3 movement = new Vector3(deltaX, 0, deltaZ);
                movement = Vector3.ClampMagnitude(movement, speed);

                movement.y = gravity;

                movement *= Time.deltaTime;
                movement = transform.TransformDirection(movement);
                _charController.Move(movement);
            }
        }
    }
    public void PlayerDead() {
        playerdead = true;
    }
    public void PlayerNotDead() {
        playerdead = false;
    }
}
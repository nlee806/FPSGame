using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class RelativeMovement : MonoBehaviour {

    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip walking;
   // [SerializeField] private Transform target;
    private Transform target;
    private ControllerColliderHit _contact;
    public float pushForce = 3.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;
    public float moveSpeed = 6.0f;
    private CharacterController _charController;

    void Start(){
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
        soundSource.volume = 1;
    }

    // Update is called once per frame
    void Update () {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0) {
            movement.x = horInput;
            movement.y = vertInput;
            //soundSource.clip = Resources.Load("hard_shoes_1-daniel_simon") as AudioClip;
            soundSource.clip = walking;
            soundSource.Play();
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            transform.rotation = Quaternion.LookRotation(movement);
        }
        if (_charController.isGrounded) {
            if (Input.GetButtonDown("Jump")){
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
            }
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed<terminalVelocity) {
                _vertSpeed = terminalVelocity;
            }
        }
        movement.y = _vertSpeed;
        movement *= Time.deltaTime;
        _charController.Move(movement);
	}
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic) {
            body.velocity = hit.moveDirection * pushForce;
        }
    }

}

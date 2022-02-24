using UnityEngine;
using System.Collections;
using UnityEngine.AI;
//Bystanders should move away from the player when the player is in its field of vision

public class BystanderBehavior : MonoBehaviour
{
    private Animator _animator;
//    private float _speed = 0.01f;
    public float speed = 0.01f;
    public float obstacleRange = 5.0f;
    public GameObject player;

    private bool _alive;

    bool playerdead = false;

    public Transform[] patrolPoints;

    private int currentControlPointIndex = 0;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        _alive = true;
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Speed", speed);
        _animator.SetBool("Jumping", false);
        MoveToNextPatrolPoint();
    }

    void Update()
    {
        if (_alive)
        {
            speed = 0.01f;
            _animator.SetBool("Jumping", false);
            _animator.SetFloat("Speed", speed);
            if ((player.transform.position - this.transform.position).sqrMagnitude < 7 * 7)
            {

                StartCoroutine(RunAway());

            }
            else
            {
                //if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
                //{
                //    MoveToNextPatrolPoint();
                //}
                //transform.Translate(0, 0, speed * Time.deltaTime);
                //Ray ray = new Ray(transform.position, transform.forward);
                //RaycastHit hit;
                //if (Physics.SphereCast(ray, 0.75f, out hit))
                //{
                //    GameObject hitObject = hit.transform.gameObject;
                //    if (hitObject.GetComponent<PlayerCharacter>())
                //    {

                //    }
                //    else if (hit.distance < obstacleRange)
                //    {
                //        float angle = Random.Range(-110, 110);
                //        transform.Rotate(0, angle, 0);
                //    }
                //}
            }


        }
    }
    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length > 0)
        {
            navMeshAgent.destination = patrolPoints[currentControlPointIndex].position;

            currentControlPointIndex++;
            currentControlPointIndex %= patrolPoints.Length;
        }
    }


    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    public void PlayerDead()
    {
        playerdead = true;
    }
    public IEnumerator RunAway() {
        this.transform.LookAt(player.transform);
        this.transform.Rotate(0, 90, 0);
        _animator.SetBool("Jumping", false);
        speed = 7.5f; //7.5f
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
        _animator.SetFloat("Speed", speed);

        yield return new WaitForSeconds(1);
    }

}

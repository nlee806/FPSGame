using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script to allow player to teleport when they touch the teleport object.
public class Teleport : MonoBehaviour {
    [SerializeField] private AudioSource soundSource; //player object
    [SerializeField] private AudioClip transportMe; //audio clip
    GameObject[] teleportArray;
    private int prevNum = -1;
    [SerializeField] private int xlowerlimit;
    [SerializeField] private int xupperlimit;
    [SerializeField] private int ylowerlimit;
    [SerializeField] private int yupperlimit;
    [SerializeField] private int zlowerlimit;
    [SerializeField] private int zupperlimit;
    //    var destination: Vector3;

    void Start()
    {
        //teleportArray = GameObject.FindGameObjectsWithTag("TeleportTo");
    }

    void OnTriggerEnter(Collider other) // (other:Collider)
    {
        if (other.transform.tag == "Player")
        {
            soundSource.volume = 1;

            Vector3 destination = new Vector3(Random.Range(xlowerlimit, xupperlimit), Random.Range(ylowerlimit, yupperlimit), Random.Range(zlowerlimit, zupperlimit));
            Debug.Log("pos: xlow"+xlowerlimit+"xup"+xupperlimit+"zlow"+zlowerlimit+"zup"+zupperlimit);
            AudioSource.PlayClipAtPoint(transportMe, transform.position);
            other.transform.position = destination;
            /*
            int number;
            do{
                number = Random.Range(1, teleportArray.Length);
            } while (number == prevNum);
            prevNum = number;
            transform.position = teleportArray[number].transform.position;
    */
        }
}



 }

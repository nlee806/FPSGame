using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds items to the players inventory.
public class CollectibleItem : MonoBehaviour {
    [SerializeField] private AudioSource soundSource; //player object
    [SerializeField] private AudioClip pickup; //audio clip
    [SerializeField] private string itemName;
    int assistance = 5;

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player")
        {
            soundSource.volume = 1;
            Debug.Log("Item collected: " + itemName);
            Managers.Inventory.AddItem(itemName);
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            //health increased after acquiring pickup objects
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(pickup, transform.position);
                player.Help(assistance);
                Destroy(this.gameObject);
            }
            //pickup object removed from gameplay
        }
    }

}

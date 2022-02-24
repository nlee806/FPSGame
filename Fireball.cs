using UnityEngine;
using System.Collections;

//Fireball script to move the fireball when shot and give ranges of speed and damage.
public class Fireball : MonoBehaviour
{
    public float speed = 5.0f;
    public int damage = 1;

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
}

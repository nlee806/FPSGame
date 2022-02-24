using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

    public GameObject firstWeapon;
    public GameObject secondWeapon;
    public int currentWeapon;

	// Use this for initialization
	void Start () {
        currentWeapon = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            if(currentWeapon == 0)
            {
                currentWeapon = 1;
                firstWeapon.SetActive(false);
                secondWeapon.SetActive(true);
            }
            else if (currentWeapon == 1)
            {
                currentWeapon = 0;
                firstWeapon.SetActive(true);
                secondWeapon.SetActive(false);
            }
        }
	}
}

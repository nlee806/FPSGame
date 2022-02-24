using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class LookAtTarget : MonoBehaviour
{
    public Transform target;
//    var targetPosition :Transform;
//    var damp: int = 5;

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(2*transform.position-target.position);
        }
//        if (targetPosition) {
//            var rotationAngle = Quaternion.LookRotation(targetPosition.position - transform.position);
//            transform.rotation = Quarternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * damp);
//        }
    }
}

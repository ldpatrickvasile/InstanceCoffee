using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Patrick Vasile
 * Outer Detection Zone
 * 2021-03-08
 * */

public class OuterDetectZone : MonoBehaviour
{

    [HideInInspector]
    public Collider asTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other) {



        if (other.CompareTag("Player")) {
            asTarget = other;
            //Debug.Log("the chase begins");

            //ChangePoint();
            //GoTo();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            asTarget = null;
           // Debug.Log("the chase ends");


            //ChangePoint();
            //GoTo();
        }
    }
}

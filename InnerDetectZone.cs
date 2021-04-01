using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Patrick Vasile
 * Inner Detection Zone
 * 2021-03-08
 * */

public class InnerDetectZone : MonoBehaviour {

    [HideInInspector]
    public Collider asTarget; //target hidden



    private void OnTriggerEnter(Collider other) {



        if (other.CompareTag("Player")) { //when player tage enters trigger
            asTarget = other;
            //Debug.Log("the attack begins");

            //ChangePoint();
            //GoTo();
        }
    }

    private void OnTriggerExit(Collider other) {//when player tage enters trigger exits
        if (other.CompareTag("Player")) {
            asTarget = null;
            //Debug.Log("the attack ends");


            //ChangePoint();
            //GoTo();
        }
    }
}

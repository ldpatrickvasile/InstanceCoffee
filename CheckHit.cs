using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Patrick Vasile
 * Check Hit
 * 2021-03-20
 * */

public class CheckHit : MonoBehaviour {

    private Renderer rd;
    private Animator myParentsAnim;

    // Start is called before the first frame update
    void Start() {
        rd = this.GetComponent<Renderer>();
        myParentsAnim = this.GetComponentInParent<Animator>();
        
    }

    // Update is called once per frame
    void Update() {

    }


    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {

            rd.material.color = Color.red;
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            //Debug.Log("the chase ends");
            rd.material.color = Color.white;


        }
    }
}
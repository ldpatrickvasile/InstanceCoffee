using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Patrick Vasile
 * Camera to rotate around the player
 * 2021-03-18
 * */

public class Scr_Camera : MonoBehaviour {


    public Transform target; //target for camera to find
    public Transform tr; // self transform

    private Man_GameManager rene; //good old rene the manager

    private Camera cam; //camera

    private bool moving = false;


    [Range(1.0f, 5.0f)] // set range for distance to player
    public float camToPlayer = 5.0f; //distance from cam to player
    [Range(1.0f, 3.0f)]// set range for height of cam
    public float camHeight = 1.0f;

    private float currentPosX = 0f; //check change is position
    private float currentPosY = 0f;//check change is position

    [Range(-100, -50)]
    public int minClamp = 0; //clamp
    [Range(0, 50)]
    public int maxClamp = 20; //clamp

    private float damp = 0;





    private void Start() {

        //setup transform
        tr = this.transform;
        cam = Camera.main; // finds "main" camera

        target = GameObject.Find("Shoulder").GetComponent<Transform>();

        rene = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Man_GameManager>();


    }


    private void Update() {
        if (rene.avatar != null) {
            if (target == null) {
                target = GameObject.Find("Shoulder").GetComponent<Transform>();

            }
        }

        if(Input.GetAxisRaw("RightStickX") > 0.02 || Input.GetAxisRaw("RightStickX") < -0.02 || Input.GetAxisRaw("RightStickY") > 0.02 || Input.GetAxisRaw("RightStickY") < -0.02) {

            moving = true;
        } else {
            moving = false;
        }

        if (moving) {
            damp += 0.01f;
        if(damp >= 5f) {
                damp = 5f;
            }
        } else {
                damp = 0.5f;
        }


        currentPosX += Input.GetAxis("RightStickX") * damp; // custom buttons i set in the project
        currentPosY += Input.GetAxis("RightStickY") * damp;// custom buttons i set in the project

       if(currentPosX >= maxClamp) { //to make sure that the current position maxes at the clamp so that when we move in the alternate direction its instant

            currentPosX = maxClamp;
        }

        if (currentPosX <= minClamp) { //to make sure that the current position is minimum at the clamp so that when we move in the alternate direction its instant

            currentPosX = minClamp;
        }
    }


    private void LateUpdate() { // late update happens at the end of the update frame before the next update frame starts

        if (target != null) {
            Vector3 direction = new Vector3(0, camHeight, -camToPlayer); // - (cam to player) so that the camera is behind the character
            Quaternion rotate = Quaternion.Euler(Mathf.Clamp(currentPosX, minClamp, maxClamp), currentPosY, 0);
            tr.position = target.position + rotate * direction;

            //Vector3 newPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);


            tr.LookAt(target.position);
        }

    }
}
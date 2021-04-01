using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

/*
 * Patrick Vasile
 * SAFE SPOTS
 * 2021-03-21
 * */

public class Scr_SafeSpot : MonoBehaviour //trigger that sets where the next spawn point will be
{

    private Transform tr;
    private Transform spawnPoint;
    //public GameObject avatar;
    private Man_GameManager rene;
    public Player player;

    void Start()
    {

        tr = this.transform;
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<Transform>();
        rene = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Man_GameManager>();

    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")){
            spawnPoint.transform.position = tr.position;
            spawnPoint.transform.rotation = tr.rotation;

            player.SavePlayer();//find out why not working
        }


    }
}

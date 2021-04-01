using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Patrick Vasile
 * Inner Detection Zone
 * 2021-03-20
 * */

public class Man_GameManager : MonoBehaviour
{
    public GameObject avatar;
    public GameObject avatarRespawn;
    public Transform respawnSpot;

    public GameObject livesOne;
    public GameObject livesTwo;
    public GameObject livesThree;

    public GameObject gameOver;

    [Range(0.1f, 1.0f)]
    public float sensitivty = 0.5f;

    [HideInInspector]
    public int lives = 2;

    private bool debugBool = false;

    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.FindGameObjectWithTag("Player");
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {

        //if(avatar == null && Input.GetKeyDown(KeyCode.Return)) {

        //if (Input.GetKeyDown(KeyCode.Return)) {//Test
        if (avatar == null && Input.GetKeyDown(KeyCode.Return)) {//Test
            //Destroy(avatar);
            
            RemoveLives(1);
            //Debug.Log(lives);
            avatar = Instantiate(avatarRespawn, respawnSpot.position, avatarRespawn.transform.rotation);
                if(lives < 0) {


                gameOver.SetActive(true);
                //SceneManager.LoadScene("MainMenu");
                //UnityEditor.EditorApplication.isPlaying = false; //using for tests remove when not needed [ends game]


            }

        }

        if(Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L)) {
            Destroy(avatar);
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            debugBool = !debugBool;
            //Debug.Log(debugBool);
        }


    }

    public void RemoveLives(int value) {

        lives -= value;


        if(lives == 2) {
            livesThree.SetActive(false);
        }

        if (lives == 1) {
            livesTwo.SetActive(false);
        }
        if (lives <= 0) {
            livesOne.SetActive(false);
        }
    } 


    public float Sensitivity {
        get { return sensitivty; }
        set { sensitivty = value; }
    }

    public int Lives {
        get { return lives; }
        //set { lives = value; }
    }

    public bool DebugBool {
        get { return debugBool; }
    }

}

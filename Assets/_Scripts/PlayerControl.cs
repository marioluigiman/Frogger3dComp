﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerControl : MonoBehaviour {

    // Use this for initialization
    bool press;
    Vector2 simple_pos;
    public Transform spawn_point;
	public GameObject prefab_explosion;

    public List<GameObject> collidedobjects = new List<GameObject>();


    void Start () {
        press = false;
        simple_pos.x = 0;
        simple_pos.y = 0;
	}

    


	// Update is called once per frame
	void Update () {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");


        Vector3 temp_pos = gameObject.transform.position;


        //Horizontal Movement

        if (Horizontal > 0 && press == false && simple_pos.x < 2)
        {
            simple_pos.x += 1;
            temp_pos.x += 2.5f;
            press = true;
        }else if (Horizontal < 0 && press == false && simple_pos.x > -2)   
        {
            simple_pos.x -= 1;
            temp_pos.x -= 2.5f;
            press = true;
        }
        //Vertical Movement
        
        else if (Vertical > 0 && press == false)
        {
            simple_pos.y += 1;
            temp_pos.z += 2.5f;
            press = true;
        }
        /**else if (Vertical < 0 && press == false)
        {
            simple_pos.y -= 1;
            temp_pos.z -= 2.5f;
            press = true;
        }**/else if (Vertical == 0 && Horizontal == 0)
        {
            press = false;
        }

        gameObject.transform.position = temp_pos;

        if (collidedobjects.Count > 0)
        {
            for (int i = 0; i != collidedobjects.Count; i++)
            {
				if (collidedobjects [i].tag == "Ground-Reg") {
					break;
				} else if (collidedobjects [i].tag == "Barrel" || collidedobjects [i].tag == "Ground-Water") {
					GameObject explosion = Instantiate (prefab_explosion) as GameObject;
					explosion.transform.position = gameObject.transform.position;

					gameObject.SetActive (false);
					Invoke ("Respawn", 1.1f);
					simple_pos = new Vector2 (0f, 0f);
				}
            }

        }
        collidedobjects = new List<GameObject>();

	}

    void OnTriggerEnter(Collider other)
    {
        collidedobjects.Add(other.gameObject);
    }

	void Respawn(){
		gameObject.SetActive (true);
		gameObject.transform.position = spawn_point.transform.position;
	}




}

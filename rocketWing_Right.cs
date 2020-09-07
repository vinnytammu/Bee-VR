using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Authors: Ben Henshaw and Tammy Do 
public class rocketWing_Right : MonoBehaviour {


    public Transform cam;
    public float maxSpeed;
    Rigidbody rr;
    Vector3 zOrient;
    float mapMovement;
    public Transform player;
    Vector3 v;
    Vector3 Controller;
    public float Zvelocity;
    public float accelTime;

    Vector3 playerPos; 

    // Use this for initialization
    void Start()
    {
        rr = GetComponent<Rigidbody>();
        //zOrient = new Vector3();
        //Controller = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        maxSpeed = 5.0f;
        playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z); 
    }

    // Update is called once per frame
    void Update()
    {
        
        Controller = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        mapMovement = map(Controller.z, -1, 1, 0, maxSpeed);

        float newPos = Mathf.SmoothDamp(transform.position.z, mapMovement, ref Zvelocity, accelTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y, newPos);
       //playerPos = playerPos + new Vector3(transform.position.x, transform.position.y, newPos);
        //Debug.Log("playerPos" + playerPos); 
        //Debug.Log("new pos: " + newPos);


    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}

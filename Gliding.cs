using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour {

    //public TakeOff takeOff;

    public float playerMoveSpeed = 5;
    public bool is_landing = false;
 
    void Update()
    {
        if (this.GetComponent<PlayerMode>().playerMode == Mode.Flying)
        //if (is_landing == false && takeOff.is_flying == true)
        {

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(h, 0, v);
            dir.Normalize();

            dir = Camera.main.transform.TransformDirection(dir);

            transform.position += dir * playerMoveSpeed * Time.deltaTime;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Don't land on hive trigger
        //if (other.name.Contains("Hive")) return;

        is_landing = true;
    }


}
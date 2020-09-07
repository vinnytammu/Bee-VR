using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour {

    public bool is_flyingOff = false;

    void Update () {

        

        if(GetComponent<MyNectar>().collecting == false)
        {
            is_flyingOff = true;

        }


    }
}

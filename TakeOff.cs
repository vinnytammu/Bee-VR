using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeOff : MonoBehaviour {


    //****** Test code ******

    public bool is_flying = false;


    public int gaugeTakeOff = 0;
    public int gaugeMaxTakeOff = 3;

    public Image gauge;

    void Update()
    {
        //*** It will not work in GameStart / GameEnd state
        //***  == this player mode will work only in GamePlay state

        if (GameManager.gamestate == GameState.GameStart) return;
        if (GameManager.gamestate == GameState.GameEnd) return;



        //if sth happens; is_flying == true;

        if (Input.GetKeyUp(KeyCode.F))
        {
            gaugeTakeOff += 1;

        }

        //GaugeUI
        gauge.fillAmount = Mathf.Lerp(gauge.fillAmount, (float)gaugeTakeOff / gaugeMaxTakeOff, Time.deltaTime*5f);



        // Takeoff -> Gliding
        if (gaugeTakeOff >= gaugeMaxTakeOff)
        {
            is_flying = true;
        }
     

    }

}

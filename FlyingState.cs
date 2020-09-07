using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingState : MonoBehaviour
{
    public static float forwardVelocity = 0.0f;
    public static float forwardImpulse  = 4.0f;
    public static float forwardMaxSpeed = 15.0f;                     //1.5
    public static float forwardDecay    = 0.97f;

    bool wantPositive = true;
    float movementThreshold = 1.5f;   // TODO: test and configure 
    float flapTime = 0f;
    float timeOut = 5.0f;             //time out check for unintended flaps 

    public Transform cam;
    Rigidbody rr;

    public Transform hover;

    [SerializeField]
    hapticsController leftController;

    [SerializeField]
    hapticsController rightController;

    public bool is_landing = false;                 //check landing state

    public AudioManage audioManager; 
    int flaps_wanted = 3;
    int flaps_got = 0;

    public AudioClip impact;
    AudioSource audioSource;

    bool impactPlayed = false;
    bool ninePlay = false; 



    void Start()
    {
        rr = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        
        forwardVelocity *= forwardDecay;
        
        Vector3 velocityL = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        Vector3 velocityR = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch); 

        if (wantPositive)
        {
            if (velocityL.y > movementThreshold && velocityR.y > movementThreshold)
            {
                wantPositive = false;
            }
        }
        else
        {
            if (velocityL.y < -movementThreshold && velocityR.y < -movementThreshold)
            {
                wantPositive = true;
                forwardVelocity += forwardImpulse;
                forwardVelocity = Mathf.Clamp(forwardVelocity, 0.0f, forwardMaxSpeed);
                flaps_got++;
                return; 
            }
        }

        if (flaps_got == flaps_wanted && impactPlayed == false)          //if inital flaps met, then play audio file: How To steer
        {
            //FindObjectOfType<AudioManage>().Play("HowToSteer");
            //StartCoroutine(PlayHowToSteer()); 
            //audioManager.GetComponent<AudioSource>().PlayOneShot("HowToSteer", 1.0f);
            audioSource.PlayOneShot(impact, .7f); 
            impactPlayed = true;
            //flaps_got = 0; 
        }


        float angle = Mathf.Deg2Rad * cam.eulerAngles.y;
        float xv = Mathf.Sin(angle) * FlyingState.forwardVelocity;
        float zv = Mathf.Cos(angle) * FlyingState.forwardVelocity;
        rr.velocity = new Vector3(xv, 0, zv);
        //Debug.Log("rr: " + rr.velocity); 
    }


    private void OnTriggerEnter(Collider other)
    {
        //Don't land on hive trigger
        //if (other.name.Contains("Hive")) return;
        
        is_landing = true;
        Debug.Log(is_landing);
    }

}


/*
IEnumerator PlayHowToSteer()
{
    //audioManager.Play("HowToSteer");
    //audioSource.PlayOneShot(impact, .7f);
    //Debug.Log("play Audio files");
    //yield return new WaitForEndOfFrame(); 
    //yield break;

}*/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insta_notification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Notification");
    }

    // Update is called once per frame
   
}

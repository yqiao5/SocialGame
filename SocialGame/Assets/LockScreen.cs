﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScreen : MonoBehaviour
{

    public GameObject Tutor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutor()
    {
        Tutor.SetActive(true);
    }
    public void StopTutor()
    {
        Tutor.SetActive(false);
    }
}

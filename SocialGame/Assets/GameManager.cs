using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return _instance;
        }
    }

    private bool FirstInstaFlag = true;
    private bool InstaMianFlag = true;
    private bool InstaBackFlag = true;
    private bool FirstEmailFlag = true;
    private bool GDCEmailFlag = true;
    private bool EmailBackFlag = true;

    public GameObject InstaTutor;
    public GameObject InstaMainTutor;
    public GameObject BackTutor;
    public GameObject EmailTutor;
    public GameObject GDCEmailTutor;
    public Animation InstaNotification;
    public Animation EmailNotification;
    public GameObject SlideNotification;
    //public GameObject EmailBackTutor;
    // Start is called before the first frame update
    void Start()
    {
        InstaNotification.Play();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Notification");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickInsta()
    {
        if (FirstInstaFlag)
        {
            InstaTutor.SetActive(false);
            FirstInstaFlag = false;
            SlideNotification.SetActive(true);
        }
    }

    public void SlideInstaContent()
    {
        if (InstaMianFlag)
        {
            InstaMainTutor.SetActive(true);
        }
    }

    public void ClickInstaMain()
    {
        if (InstaMianFlag)
        {
            SlideNotification.SetActive(false);
            SlideNotification.SetActive(true);
            InstaMainTutor.SetActive(false);
            InstaMianFlag = false;
        }
    }

    public void SlideInstaMain()
    {
        if (InstaBackFlag)
        {
            BackTutor.SetActive(true);
        }
    }

    public void ClickBack()
    {
        if (InstaBackFlag)
        {
            BackTutor.SetActive(false);
            InstaBackFlag = false;
            EmailNotification.Play();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Notification");
        }
        if(!InstaBackFlag && FirstEmailFlag)
        {
            EmailTutor.SetActive(true);
        }
        if(!GDCEmailFlag && EmailBackFlag)
        {
            BackTutor.SetActive(false);
            EmailBackFlag = false;
        }
        
    }

    public void ClickEmail()
    {
        if (!InstaBackFlag && FirstEmailFlag)
        {
            EmailTutor.SetActive(false);
            FirstEmailFlag = false;
            GDCEmailTutor.SetActive(true);
        }
    }

    public void ClickGDCEmail()
    {
        if (GDCEmailFlag)
        {
            GDCEmailTutor.SetActive(false);
            GDCEmailFlag = false;
            BackTutor.SetActive(true);
            //Debug.Log("Click GDC Email.");
            BackTutor.GetComponent<UIParticleSystem>().Restart();            
        }
    }
}

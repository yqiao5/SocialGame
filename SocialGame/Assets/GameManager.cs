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

    public GameObject InstaTutor;
    public GameObject InstaMainTutor;
    public GameObject InstaBackTutor;
    // Start is called before the first frame update
    void Start()
    {
        
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
            InstaMainTutor.SetActive(false);
            InstaMianFlag = false;
        }
    }

    public void SlideInstaMain()
    {
        if (InstaBackFlag)
        {
            InstaBackTutor.SetActive(true);
        }
    }

    public void ClickBack()
    {
        if (InstaBackFlag)
        {
            InstaBackTutor.SetActive(false);
            InstaBackFlag = false;
        }
    }
}

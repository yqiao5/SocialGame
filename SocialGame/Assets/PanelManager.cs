﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    private static PanelManager _instance;
    

    public static PanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("MainScreen").GetComponent<PanelManager>();
            }
            return _instance;
        }
    }

    public enum Panel
    {
        Main,
        InstaContent,
        InstaMain,
        EmailContent,
        EmailContentGDC,
        Browse,
        Calling,
        LockScreen,
        Note,
        NoteContent,
        Message
    }
    FMOD.Studio.EventInstance BGM;
    FMOD.Studio.EventInstance Ringtone;
    public GameObject Insta_bg;
    public GameObject Insta_Content;
    public GameObject Insta_Content_Main;
    public GameObject Email_Content;
    public GameObject Email_Content_GDC;
    public GameObject Calling_Panel;
    public GameObject Browse_Panel;
    public GameObject LockScreen_Panel;
    public GameObject Note0_Panel;
    public GameObject Note1_Panel;
    public GameObject Message0_Panel;
    public GameObject Message1_Panel;
    public GameObject Note_Content_Panel;

    public GameObject MainSceneCamera;
    private Panel tempPanel = Panel.Main;
    //private Vector2 MouseUpPosition;
    //private Vector2 MouseDownPosition;
    private int day = 0;
    private Vector2 MouseFirstPosition;
    private Vector2 MouseSecondPosition;
    private bool keyDownFlag = false;
    private bool keyUpFlag = false;
    private Vector3 InstaPositionOnDown;
    private bool CallingFlag = false;
    private float CallingTimer = 0;
    private Stack<Panel> panelStack = new Stack<Panel>();
    // Start is called before the first frame update
    void Start()
    {
        BGM = FMODUnity.RuntimeManager.CreateInstance("event:/BGM");
        Ringtone = FMODUnity.RuntimeManager.CreateInstance("event:/Ringtone");
        BGM.start();
        panelStack.Push(Panel.Main);
        //LockScreen_Panel.SetActive(false);
        PushNewPanel(Panel.LockScreen);
    }

    // Update is called once per frame
    void Update()
    {
        if (keyDownFlag)
        {
            float dis = MouseSecondPosition.y - MouseFirstPosition.y;
            //keyUpFlag = false;
            //Debug.Log("dis: " + dis);
            if (panelStack.Peek() == Panel.InstaContent)
            {
                Vector3 pp = Insta_Content.transform.position;
                float limit = pp.y + dis;
                if(limit > -1790)
                {
                    limit = -1790;
                    GameManager.Instance.SlideInstaContent();
                }else if (limit < -4310)
                {
                    limit = -4310;
                }
                Insta_Content.transform.position = new Vector3(pp.x, limit, pp.z);
                //Debug.Log("Temp Y: " + Insta_Content.transform.position.y);
            }
            if(panelStack.Peek() == Panel.InstaMain)
            {
                Vector3 pp = Insta_Content_Main.transform.position;
                float limit = pp.y + dis;
                if (limit > -3150)
                {
                    limit = -3150;
                    GameManager.Instance.SlideInstaMain();
                }
                else if (limit < -4300)
                {
                    limit = -4300;
                }
                Insta_Content_Main.transform.position = new Vector3(pp.x, limit, pp.z);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("OnMouseDown");
            //MouseDownPosition = Input.mousePosition;
            MouseFirstPosition = Input.mousePosition;
            MouseSecondPosition = Input.mousePosition;
            //if (tempPanel == Panel.Insta)
            //{
            //    InstaPositionOnDown = Insta_Content.transform.position;
            //}            
            keyDownFlag = true;
        }

        if (Input.GetMouseButton(0))
        {
            MouseFirstPosition = MouseSecondPosition;
            MouseSecondPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (keyDownFlag)
            {
                //MouseUpPosition = Input.mousePosition;
                MouseFirstPosition = Input.mousePosition;
                MouseSecondPosition = Input.mousePosition;
                keyUpFlag = true;
                keyDownFlag = false;
            }
        }

        if (CallingFlag)
        {
            CallingTimer += Time.deltaTime;
            Debug.Log("In Calling");
            if (CallingTimer > 3)
            {
                CallingFlag = false;
                //Calling_Panel.SetActive(true);
                PushNewPanel(Panel.Calling);
                BGM.setParameterByName("BGM", 0.5f);
                
            }
        }
    }

    public void ShowPanel(string newPanel)
    {
        Panel panel = (Panel)Enum.Parse(typeof(Panel), newPanel);
        PushNewPanel(panel);
        switch (panel)
        {
            case Panel.Main:
                break;
            case Panel.InstaContent:
                GameManager.Instance.ClickInsta();
                break;
            case Panel.InstaMain:
                break;
            case Panel.EmailContent:
                GameManager.Instance.ClickEmail();
                break;
            case Panel.EmailContentGDC:
                GameManager.Instance.ClickGDCEmail();
                break;
            case Panel.Browse:
                break;
        }
    }

    public void BackButtonClick()
    {        
        //switch (tempPanel)
        switch (panelStack.Peek())
        {
            case Panel.Main:
                break;
            case Panel.InstaContent:
                //Insta_bg.SetActive(false);
                //Insta_Content.SetActive(false);
                //tempPanel = Panel.Main;
                //panelStack.Pop();
                PopPanel();
                GameManager.Instance.ClickBack();
                break;
            case Panel.InstaMain:
                //Insta_bg.SetActive(false);
                //Insta_Content_Main.SetActive(false);
                //tempPanel = Panel.Main;
                //panelStack.Pop();
                PopPanel();
                GameManager.Instance.ClickBack();
                break;
            case Panel.EmailContentGDC:
                //Email_Content_GDC.SetActive(false);
                //tempPanel = Panel.EmailContent;
                //panelStack.Pop();
                PopPanel();
                //GameManager.Instance.ClickBack();
                break;
            case Panel.EmailContent:
                //Email_Content.SetActive(false);
                //tempPanel = Panel.Main;
                //panelStack.Pop();
                PopPanel();
                GameManager.Instance.ClickBack();
                break;
            case Panel.Browse:
                //Browse_Panel.SetActive(false);
                //panelStack.Pop();
                PopPanel();
                break;
            case Panel.Note:
                PopPanel();
                break;
            case Panel.Message:
                PopPanel();
                break;
            case Panel.NoteContent:
                PopPanel();
                break;
        }
        Debug.Log("stack.peek(): " + panelStack.Peek());
    }

    public void InstaMainButtonClick()
    {
        //if(tempPanel == Panel.InstaContent)
        if(panelStack.Peek() == Panel.InstaContent)
        {
            //Insta_Content_Main.SetActive(true);
            //Insta_bg.SetActive(true);
            //Insta_Content.SetActive(false);
            //tempPanel = Panel.InstaMain;
            //panelStack.Pop();
            //panelStack.Push(Panel.InstaMain);
            PopPanel();
            PushNewPanel(Panel.InstaMain);
            GameManager.Instance.ClickInstaMain();
        }
    }

    public void InstaContentButtonClick()
    {
        //if (tempPanel == Panel.InstaMain)
        if (panelStack.Peek() == Panel.InstaMain)
        {
            //Insta_Content_Main.SetActive(false);
            //Insta_bg.SetActive(true);
            //Insta_Content.SetActive(true);
            //tempPanel = Panel.InstaContent;
            //panelStack.Pop();
            //panelStack.Push(Panel.InstaContent);
            PopPanel();
            PushNewPanel(Panel.InstaContent);
        }
    }
    
    public void EmailTutorEnd()
    {
        //Debug.Log("In tutor end");
        CallingFlag = true;
    }

    public void ReceiveCallButtonClick()
    {
        SceneManager.LoadScene("CallScene",LoadSceneMode.Additive);
        PopPanel();
        transform.GetComponent<CanvasGroup>().alpha = 0;
        MainSceneCamera.SetActive(false);        
        BGM.setParameterByName("BGM", 0.8f);
    }

    public void EndCall()
    {
        day = 1;
        transform.GetComponent<CanvasGroup>().alpha = 1;
        MainSceneCamera.SetActive(true);
        SceneManager.UnloadSceneAsync("CallScene");
    }

    public void ClickMessage()
    {
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        //PopPanel();
        transform.GetComponent<CanvasGroup>().alpha = 0;
        MainSceneCamera.SetActive(false);
        //BGM.setParameterByName("BGM", 0.8f);
    }

    private void PushNewPanel(Panel newPanel)
    {
        HidePanel(panelStack.Peek());
        ShowPanel(newPanel);
        panelStack.Push(newPanel);
    }

    public void PopPanel()
    {
        HidePanel(panelStack.Peek());
        panelStack.Pop();
        ShowPanel(panelStack.Peek());
    }

    private void HidePanel(Panel panel)
    {
        switch (panel)
        {
            case Panel.Browse:
                Browse_Panel.SetActive(false);
                break;
            case Panel.EmailContent:
                Email_Content.SetActive(false);
                break;
            case Panel.EmailContentGDC:
                Email_Content_GDC.SetActive(false);
                break;
            case Panel.InstaContent:
                Insta_Content.SetActive(false);
                Insta_bg.SetActive(false);
                break;
            case Panel.InstaMain:
                Insta_Content_Main.SetActive(false);
                Insta_bg.SetActive(false);
                break;
            case Panel.Calling:
                Calling_Panel.SetActive(false);
                break;
            case Panel.LockScreen:
                LockScreen_Panel.SetActive(false);
                break;
            case Panel.Note:
                if(day == 0)
                {
                    Note0_Panel.SetActive(false);
                }else if(day == 1)
                {
                    Note1_Panel.SetActive(false);
                }                
                break;
            case Panel.NoteContent:
                Note_Content_Panel.SetActive(false);
                break;
            case Panel.Message:
                if (day == 0)
                {
                    Message0_Panel.SetActive(false);
                }
                else if (day == 1)
                {
                    Message1_Panel.SetActive(false);
                }
                break;            
            case Panel.Main:
                break;
        }
    }

    private void ShowPanel(Panel panel)
    {
        switch (panel)
        {
            case Panel.Browse:
                Browse_Panel.SetActive(true);
                break;
            case Panel.EmailContent:
                Email_Content.SetActive(true);
                break;
            case Panel.EmailContentGDC:
                Email_Content_GDC.SetActive(true);
                break;
            case Panel.InstaContent:
                Insta_Content.SetActive(true);
                Insta_bg.SetActive(true);
                break;
            case Panel.InstaMain:
                Insta_Content_Main.SetActive(true);
                Insta_bg.SetActive(true);
                break;
            case Panel.LockScreen:
                LockScreen_Panel.SetActive(true);
                break;
            case Panel.Calling:
                Calling_Panel.SetActive(true);
                break;
            case Panel.Note:
                if (day == 0)
                {
                    Note0_Panel.SetActive(true);
                }
                else if (day == 1)
                {
                    Note1_Panel.SetActive(true);
                }
                break;
            case Panel.NoteContent:
                Note_Content_Panel.SetActive(true);
                break;
            case Panel.Message:
                if (day == 0)
                {
                    Message0_Panel.SetActive(true);
                }
                else if (day == 1)
                {
                    Message1_Panel.SetActive(true);
                }
                break;
            case Panel.Main:
                break;
        }
    }
}

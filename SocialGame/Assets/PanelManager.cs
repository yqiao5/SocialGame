using System;
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
        Message,
        Secret
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
    public GameObject Insta_Content_Second;
    public GameObject Secret_Panel;
    public GameObject Secret_App;
    public GameObject ReportEmail_Panel;

    public GameObject LockScreenClock;

    public GameObject MainSceneCamera;
    private Panel tempPanel = Panel.Main;
    //private Vector2 MouseUpPosition;
    //private Vector2 MouseDownPosition;
    public int day = 0;
    private Vector2 MouseFirstPosition;
    private Vector2 MouseSecondPosition;
    private bool keyDownFlag = false;
    private bool keyUpFlag = false;
    private Vector3 InstaPositionOnDown;
    private bool CallingFlag = false;
    private float CallingTimer = 0;
    private bool MessageFlag = false;
    private float MessageTimer = 0;
    private bool ReportMailFlag = false;
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
        //Debug.Log("day: " + day);
        //Debug.Log("PEEK: " + panelStack.Peek());
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
                    GameManager.Instance.SlideInstaMain();
                    
                }else if (limit < -4310)
                {
                    limit = -4310;
                }
                Insta_Content.transform.position = new Vector3(pp.x, limit, pp.z);
                //Debug.Log("Temp Y: " + Insta_Content.transform.position.y);
            }
            if(panelStack.Peek() == Panel.InstaMain && day != 4)
            {
                Vector3 pp = Insta_Content_Main.transform.position;
                float limit = pp.y + dis;
                if (limit > -3150)
                {
                    limit = -3150;
                    GameManager.Instance.SlideInstaContent();
                }
                else if (limit < -4300)
                {
                    limit = -4300;
                }
                Insta_Content_Main.transform.position = new Vector3(pp.x, limit, pp.z);
            }
            if (panelStack.Peek() == Panel.InstaMain && day == 4)
            {
                Vector3 pp = Insta_Content_Second.transform.position;
                float limit = pp.y + dis;
                if (limit > -3180)
                {
                    limit = -3180;
                    //GameManager.Instance.SlideInstaMain();
                }
                else if (limit < -4310)
                {
                    limit = -4310;
                }
                Insta_Content_Second.transform.position = new Vector3(pp.x, limit, pp.z);
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
            //Debug.Log("In Calling");
            if (CallingTimer > 1)
            {
                CallingFlag = false;
                CallingTimer = 0;
                //Calling_Panel.SetActive(true);
                PushNewPanel(Panel.Calling);
                BGM.setParameterByName("BGM", 12f);
                
            }
        }

        if (MessageFlag)
        {
            MessageTimer += Time.deltaTime;
            //Debug.Log("In Calling");
            if (MessageTimer > 1)
            {
                MessageFlag = false;
                MessageTimer = 0;
                //Calling_Panel.SetActive(true);
                BGM.setParameterByName("BGM", 37f);
                PushNewPanel(Panel.LockScreen);
                LockScreenClock.GetComponent<timerun>().SetEndFlag();
                day++;
                
                //BGM.setParameterByName("BGM", 0.5f);
            }
        }
    }

    public void ShowPanel(string newPanel)
    {
        //Debug.Log("Click ShowPanel!");
        Panel panel = (Panel)Enum.Parse(typeof(Panel), newPanel);
        PushNewPanel(panel);
        switch (panel)
        {
            case Panel.Main:                
                break;
            case Panel.InstaContent:
                
                break;
            case Panel.InstaMain:
                GameManager.Instance.ClickInsta();
                break;
            case Panel.EmailContent:
                GameManager.Instance.ClickEmail();
                break;
            case Panel.EmailContentGDC:
                GameManager.Instance.ClickGDCEmail();
                break;
            case Panel.Browse:
                break;
            case Panel.Note:
                if(day == 2)
                {
                    GameManager.Instance.ClickNote();
                }
                break;
            case Panel.Message:
                if(day == 3)
                {
                    ShowGrouptalk();
                }
                if(day == 5)
                {
                    ShowRMessage();
                }
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
                PopPanel();
                if(day == 4)
                {
                    GameManager.Instance.ShowRMessageNotification();
                    day++;
                }
                GameManager.Instance.ClickBack();
                break;
            case Panel.InstaMain:
                PopPanel();
                GameManager.Instance.ClickBack();
                if (day == 4)
                {
                    GameManager.Instance.ShowRMessageNotification();
                    day++;
                }
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
                MessageFlag = true;
                break;
            case Panel.Message:
                PopPanel();
                break;
            case Panel.NoteContent:
                PopPanel();
                break;
            case Panel.Secret:
                PopPanel();
                break;
        }
        //Debug.Log("stack.peek(): " + panelStack.Peek());
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
            GameManager.Instance.ClickInstaMain();
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
        transform.GetComponent<CanvasGroup>().interactable = false;
        MainSceneCamera.SetActive(false);        
        BGM.setParameterByName("BGM", 17f);
    }

    public void EndCall()
    {
        day = 1;
        transform.GetComponent<CanvasGroup>().alpha = 1;
        transform.GetComponent<CanvasGroup>().interactable = true;
        MainSceneCamera.SetActive(true);
        SceneManager.UnloadSceneAsync("CallScene");
    }

    public void ClickMessage()
    {
        BGM.setParameterByName("BGM", 25f);
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        //PopPanel();
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<CanvasGroup>().interactable = false;
        MainSceneCamera.SetActive(false);
        //BGM.setParameterByName("BGM", 0.8f);
    }

    public void EndMessage()
    {
        transform.GetComponent<CanvasGroup>().alpha = 1;
        transform.GetComponent<CanvasGroup>().interactable = true;
        MainSceneCamera.SetActive(true);
        SceneManager.UnloadSceneAsync("TextScene");
        MessageFlag = true;
    }

    public void ShowGrouptalk()
    {
        SceneManager.LoadScene("GroupTalk", LoadSceneMode.Additive);
        //PopPanel();
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<CanvasGroup>().interactable = false;
        MainSceneCamera.SetActive(false);
        //BGM.setParameterByName("BGM", 0.8f);
    }

    public void EndTalk()
    {
        transform.GetComponent<CanvasGroup>().alpha = 1;
        transform.GetComponent<CanvasGroup>().interactable = true;
        MainSceneCamera.SetActive(true);
        SceneManager.UnloadSceneAsync("GroupTalk");
        day++;
    }

    public void ShowRMessage()
    {
        SceneManager.LoadScene("Ruby", LoadSceneMode.Additive);
        //PopPanel();
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<CanvasGroup>().interactable = false;
        MainSceneCamera.SetActive(false);
        //BGM.setParameterByName("BGM", 0.8f);
    }

    public void EndRuby()
    {
        transform.GetComponent<CanvasGroup>().alpha = 1;
        transform.GetComponent<CanvasGroup>().interactable = true;
        MainSceneCamera.SetActive(true);
        SceneManager.UnloadSceneAsync("Ruby");
        Secret_App.SetActive(true);
        day++;
    }

    public void ReportSucceed()
    {
        ReportMailFlag = true;
    }

    public void FinalScene()
    {
        Debug.Log("In Final Scene.");
        SceneManager.LoadScene("End");
    }

    private void PushNewPanel(Panel newPanel)
    {
        HidePanel(panelStack.Peek());
        ShowPanel(newPanel);
        panelStack.Push(newPanel);
    }

    public void StartLockScreenTutor()
    {
        LockScreen_Panel.GetComponent<LockScreen>().StartTutor();
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
                if (ReportMailFlag)
                {
                    ReportEmail_Panel.SetActive(false);
                }
                else
                {
                    Email_Content.SetActive(false);
                }
                break;
            case Panel.EmailContentGDC:
                Email_Content_GDC.SetActive(false);
                break;
            case Panel.InstaContent:
                Insta_Content.SetActive(false);
                Insta_bg.SetActive(false);
                break;
            case Panel.InstaMain:
                if (day == 4)
                {
                    Insta_Content_Second.SetActive(false);
                }
                else
                {
                    Insta_Content_Main.SetActive(false);
                }                
                Insta_bg.SetActive(false);
                break;
            case Panel.Calling:
                Calling_Panel.SetActive(false);
                break;
            case Panel.LockScreen:
                LockScreen_Panel.GetComponent<LockScreen>().StopTutor();
                LockScreen_Panel.SetActive(false);
                break;
            case Panel.Note:
                if(day == 0 || day == 1)
                {
                    Note0_Panel.SetActive(false);
                }else if(day == 2 || day == 3 || day == 4 || day == 5)
                {
                    Note1_Panel.SetActive(false);
                }                
                break;
            case Panel.NoteContent:
                Note_Content_Panel.SetActive(false);
                break;
            case Panel.Message:
                if (day == 0 || day == 2 || day == 4)
                {
                    Message0_Panel.SetActive(false);
                }
                else if (day == 1)
                {
                    Message1_Panel.SetActive(false);
                }
                break;
            case Panel.Secret:
                Secret_Panel.SetActive(false);
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
                if (ReportMailFlag)
                {
                    ReportEmail_Panel.SetActive(true);
                }
                else
                {
                    Email_Content.SetActive(true);
                }
                
                break;
            case Panel.EmailContentGDC:
                Email_Content_GDC.SetActive(true);
                break;
            case Panel.InstaContent:
                Insta_Content.SetActive(true);
                Insta_bg.SetActive(true);
                break;
            case Panel.InstaMain:
                if(day == 4)
                {
                    Insta_Content_Second.SetActive(true);
                }
                else
                {
                    Insta_Content_Main.SetActive(true);
                }                
                Insta_bg.SetActive(true);
                break;
            case Panel.LockScreen:
                LockScreen_Panel.SetActive(true);
                break;
            case Panel.Calling:
                Calling_Panel.SetActive(true);
                break;
            case Panel.Note:
                if (day == 0 || day == 1)
                {
                    Note0_Panel.SetActive(true);
                }
                else if (day == 2 || day == 3 || day == 4 || day == 5)
                {
                    Note1_Panel.SetActive(true);
                }
                break;
            case Panel.NoteContent:
                Note_Content_Panel.SetActive(true);
                break;
            case Panel.Message:
                if (day == 0 || day == 2 || day == 4)
                {
                    Message0_Panel.SetActive(true);
                }
                else if (day == 1)
                {
                    Message1_Panel.SetActive(true);
                }
                break;
            case Panel.Main:
                if (day == 2)
                {
                    GameManager.Instance.ShowNoteTutor();
                    BGM.setParameterByName("BGM", 50f);
                }
                if (day == 3)
                {
                    GameManager.Instance.ShowGrouptalkNotification();
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Notification");
                    BGM.setParameterByName("BGM", 65f);
                }
                if(day == 4)
                {
                    GameManager.Instance.ShowInstaVRNotification();
                }
                if (ReportMailFlag)
                {
                    GameManager.Instance.ShowReportMailNotification();
                }
                break;
            case Panel.Secret:
                //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!");
                Secret_Panel.SetActive(true);
                break;
        }
    }
}

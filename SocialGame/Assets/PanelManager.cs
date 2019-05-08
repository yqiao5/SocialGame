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

    enum Panel
    {
        Main,
        InstaContent,
        InstaMain,
        EmailContent,
        EmailContentGDC
    }
    FMOD.Studio.EventInstance BGM;
    public GameObject Insta_bg;
    public GameObject Insta_Content;
    public GameObject Insta_Content_Main;
    public GameObject Email_Content;
    public GameObject Email_Content_GDC;
    public GameObject Calling_Panel;
    private Panel tempPanel = Panel.Main;
    //private Vector2 MouseUpPosition;
    //private Vector2 MouseDownPosition;
    private Vector2 MouseFirstPosition;
    private Vector2 MouseSecondPosition;
    private bool keyDownFlag = false;
    private bool keyUpFlag = false;
    private Vector3 InstaPositionOnDown;
    private bool CallingFlag = false;
    private float CallingTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        BGM = FMODUnity.RuntimeManager.CreateInstance("event:/BGM");
        BGM.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyDownFlag)
        {
            float dis = MouseSecondPosition.y - MouseFirstPosition.y;
            //keyUpFlag = false;
            //Debug.Log("dis: " + dis);
            if (tempPanel == Panel.InstaContent)
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
            if(tempPanel == Panel.InstaMain)
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
            //Debug.Log("In Calling");
            if (CallingTimer > 3)
            {
                CallingFlag = false;
                Calling_Panel.SetActive(true);
            }
        }
    }

    public void ShowInsta()
    {
        Insta_bg.SetActive(true);
        Insta_Content.SetActive(true);
        tempPanel = Panel.InstaContent;
        GameManager.Instance.ClickInsta();
    }

    public void ShowEmail()
    {
        Email_Content.SetActive(true);
        //Insta_Content.SetActive(true);
        tempPanel = Panel.EmailContent;
        GameManager.Instance.ClickEmail();
    }

    public void ShowEmailGDC()
    {
        Email_Content_GDC.SetActive(true);
        //Insta_Content.SetActive(true);
        tempPanel = Panel.EmailContentGDC;
        GameManager.Instance.ClickGDCEmail();
    }

    public void BackButtonClick()
    {
        switch (tempPanel)
        {
            case Panel.Main:
                break;
            case Panel.InstaContent:
                Insta_bg.SetActive(false);
                Insta_Content.SetActive(false);
                tempPanel = Panel.Main;
                GameManager.Instance.ClickBack();
                break;
            case Panel.InstaMain:
                Insta_bg.SetActive(false);
                Insta_Content_Main.SetActive(false);
                tempPanel = Panel.Main;
                GameManager.Instance.ClickBack();
                break;
            case Panel.EmailContentGDC:
                Email_Content_GDC.SetActive(false);
                tempPanel = Panel.EmailContent;
                //GameManager.Instance.ClickBack();
                break;
            case Panel.EmailContent:
                Email_Content.SetActive(false);
                tempPanel = Panel.Main;
                GameManager.Instance.ClickBack();

                break;

        }
    }

    public void InstaMainButtonClick()
    {
        if(tempPanel == Panel.InstaContent)
        {
            Insta_Content_Main.SetActive(true);
            Insta_bg.SetActive(true);
            Insta_Content.SetActive(false);
            tempPanel = Panel.InstaMain;
            GameManager.Instance.ClickInstaMain();
        }
    }

    public void InstaContentButtonClick()
    {
        if (tempPanel == Panel.InstaMain)
        {
            Insta_Content_Main.SetActive(false);
            Insta_bg.SetActive(true);
            Insta_Content.SetActive(true);
            tempPanel = Panel.InstaContent;
        }
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log("OnMouseDown");
    //    MouseDownPosition = Input.mousePosition;
    //    keyDownFlag = true;
    //}

    //private void OnMouseUp()
    //{
    //    if (keyDownFlag)
    //    {
    //        MouseUpPosition = Input.mousePosition;
    //        keyUpFlag = true;
    //        keyDownFlag = false;
    //    }

    //}
    public void EmailTutorEnd()
    {
        //Debug.Log("In tutor end");
        CallingFlag = true;
    }

    public void ReceiveCallButtonClick()
    {
        SceneManager.LoadScene("CallScene");
    }
}

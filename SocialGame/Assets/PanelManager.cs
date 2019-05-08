using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        InstaMain
    }

    public GameObject Insta_bg;
    public GameObject Insta_Content;
    public GameObject Insta_Content_Main;
    private Panel tempPanel = Panel.Main;
    //private Vector2 MouseUpPosition;
    //private Vector2 MouseDownPosition;
    private Vector2 MouseFirstPosition;
    private Vector2 MouseSecondPosition;
    private bool keyDownFlag = false;
    private bool keyUpFlag = false;
    private Vector3 InstaPositionOnDown;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void ShowInsta()
    {
        Insta_bg.SetActive(true);
        Insta_Content.SetActive(true);
        tempPanel = Panel.InstaContent;
        GameManager.Instance.ClickInsta();
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
}

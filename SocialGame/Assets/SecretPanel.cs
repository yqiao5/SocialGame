using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretPanel : MonoBehaviour
{
    public GameObject ReportButton;
    public GameObject ReportSucceedButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowReport()
    {
        ReportButton.SetActive(true);
    }

    public void ShowReportSucceed()
    {
        ReportSucceedButton.SetActive(true);
        ReportButton.SetActive(false);
        PanelManager.Instance.ReportSucceed();
    }

    public void ClickReportSucceed()
    {
        ReportSucceedButton.SetActive(false);
    }
      
}

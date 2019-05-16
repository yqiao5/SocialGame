using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click5 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool end=false;
    public int flag;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                
                if (end == true)
                {
                    if ((hit.collider.gameObject.tag == "P1Phone" && this.name == "p1") || 
                        (hit.collider.gameObject.tag == "P2Phone" && this.name == "p2") ||
                        (hit.collider.gameObject.tag == "P3Phone" && this.name == "p3"))
                    {
                        Debug.Log("In Phone");
                        PanelManager.Instance.EndCall();
                    }
                    else if ((hit.collider.gameObject.tag == "P1Text" && this.name == "p1") || 
                        (hit.collider.gameObject.tag == "P2Text" && this.name == "p2"))
                    {
                        Debug.Log("In Text");
                        PanelManager.Instance.EndMessage();
                    }
                    else if ((hit.collider.gameObject.tag == "P1Group" && this.name == "p1") || 
                        (hit.collider.gameObject.tag == "P2Group" && this.name == "p2"))
                    {
                        Debug.Log("This Tag: " + this.tag);
                        Debug.Log("This Name: " + this.name);
                        PanelManager.Instance.EndTalk();
                    }
                    else if ((hit.collider.gameObject.tag == "P1Truth" && this.name == "p1") ||
                        (hit.collider.gameObject.tag == "P2Truth" && this.name == "p2"))
                    {
                        Debug.Log("In Truth");
                        PanelManager.Instance.EndRuby();
                    }
                    else if (this.tag == "ch"&&this.name=="p1")
                    {
                        flag = 1;
                        GameObject.Find("p1").GetComponent<click5>().end = true;
                        GameObject.Find("p2").GetComponent<click5>().end = true;
                    }
                    else if (this.tag == "ch" && this.name == "p2")
                    {
                        flag = 2;
                        GameObject.Find("p1").GetComponent<click5>().end = true;
                        GameObject.Find("p2").GetComponent<click5>().end = true;
                    }
                }
            }

        }
    }
}

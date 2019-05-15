using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click5 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool end=false;
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
                    if (this.tag == "phone"){
                        Debug.Log("mmm");
                        PanelManager.Instance.EndCall();
                    }
                    else if (this.tag == "text")
                    {
                        //PanelManager.Instance.EndMessage();
                    }
                }
            }

        }
    }
}

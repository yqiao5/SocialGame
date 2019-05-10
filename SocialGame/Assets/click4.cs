using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click4 : MonoBehaviour
{
    // Start is called before the first frame update
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
                if (hit.collider.gameObject.name == "6" || hit.collider.gameObject.name == "61")
                {
                    if (this.name == "6")
                    {
                        GameObject.Find("way1").GetComponent<Display1>().ch1 = 1;
                    }
                    else if (this.name == "61")
                    {
                        GameObject.Find("way2").GetComponent<Display1>().ch1 = 1;
                    }
                    //GameObject.Find("way1").GetComponent<Display1>().start = 1;

                    break;
                }
            }

        }
    }
}

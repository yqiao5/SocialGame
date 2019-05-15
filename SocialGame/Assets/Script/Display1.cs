using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display1 : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> dias;
    public int flag;
    private float k;
    public int start;
    public float curtime;
    private int m=0;
    private Vector3 a;
    public int ch1;
    private float atime;
    void Start()
    {
        for (int i = 0; i <= dias.Count - 1; i++)
        {
            dias[i].SetActive(false);
        }
        //this.transform.position = new Vector3(0, -2.8f, -1.9f);
        //k = -2.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ch1 == 1)
        {
            if (atime == 0)
            {
                atime = Time.time;
            }
            if(Time.time-atime> 1.65)
            {
                k = k + 1.5f;
                a = new Vector3(0, k, this.transform.position.z);
                atime = Time.time;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, a, 5 * Time.deltaTime);
        }
        if (start == 0&&this.name=="way1")
        {
            this.transform.position = new Vector3(0, -3.83f, -1.9f);
            k = -3.83f;
        }
        if(start == 0 && this.name == "way2")
        {
            this.transform.position = new Vector3(0, -6.35f, -1.9f);
            k = -6.35f;
        }
        if (start == 0 && this.name == "cho1")
        {
            this.transform.position = new Vector3(-0.26f, -4.69f, -1.9f);
            k = -4.69f;
        }
        if (start == 0 && this.name == "cho2")
        {
            this.transform.position = new Vector3(0.3167539f, -7.09f, -1.9f);
            k = -7.09f;
        }
        if (start == 1)
        {
            if (curtime == 0)
            {
                curtime = Time.time;
            }
            if (Time.time - curtime > 1.65&&m<= dias.Count)
            {
                if(m<= dias.Count - 1)
                {
                    dias[m].SetActive(true);
                }
                
                m++;
                k = k + 1.5f;
                a = new Vector3(this.transform.position.x, k, this.transform.position.z);
                curtime = Time.time;
            }
            if (m >= 1&&ch1==0)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, a, 5 * Time.deltaTime);
            }
        }
        if(m>= dias.Count-1&&this.name=="cho1"&&this.name=="cho2")
        {
            GameObject.Find("p1").GetComponent<click5>().end = true;
            GameObject.Find("p2").GetComponent<click5>().end = true;
            GameObject.Find("p3").GetComponent<click5>().end = true;
        }
    }
}

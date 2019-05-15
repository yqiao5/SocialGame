using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fma : MonoBehaviour
{
    // Start is called before the first frame update
    public float curtime;
    public GameObject hu;
    public int flag;
    void Start()
    {
        hu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (curtime == 0)
        {
            curtime = Time.time;
        }
        if (Time.time - curtime > 0.05&& flag==0)
        {
            GameObject.Find("Main Camera").GetComponent<chane>().brightness -= 0.08f;
            curtime = 0;
            if(GameObject.Find("Main Camera").GetComponent<chane>().brightness < -2.5f)
            {
                flag = 1;
                hu.SetActive(true);
            }
        }
        if (Time.time - curtime > 0.05 && flag == 1)
        {
            GameObject.Find("Main Camera").GetComponent<chane>().brightness += 0.08f;
            curtime = 0;
            if (GameObject.Find("Main Camera").GetComponent<chane>().brightness >= 1)
            {
                flag = 2;
                GameObject.Find("p1").GetComponent<click5>().end = true;
                GameObject.Find("p2").GetComponent<click5>().end = true;
            }
        }
        if (Time.time - curtime > 0.05 && flag == 2)
        {
            if (GameObject.Find("p1").GetComponent<click5>().flag == 1&& GameObject.Find("p2").GetComponent<click5>().flag !=2)
            {
                GameObject.Find("Main Camera").GetComponent<chane>().brightness += 0.16f;
                GameObject.Find("Main Camera").GetComponent<chane>().saturation = -0.1f;
                GameObject.Find("Main Camera").GetComponent<chane>().contrast -= 0.1f;
            }
            else if(GameObject.Find("p2").GetComponent<click5>().flag == 2)
            {
                GameObject.Find("Main Camera").GetComponent<chane>().brightness -= 0.08f;
            }
            curtime = 0;
        }

    }
}

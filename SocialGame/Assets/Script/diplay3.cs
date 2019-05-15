using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diplay3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] m;
    public int flag;
    public float curtime;
    public float wtime;
    public Vector3 k;
    public bool start=false;
    public bool move = false;
    void Start()
    {
        for(int i = 0; i <= m.Length-1; i++)
        {
            m[i].SetActive(false);
            k = this.transform.position;
        }
        curtime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - curtime >= 1.8f)
        {
            start = true;
        }
        if (this.name == "tex")
        {
            start = true;
        }
        if (start == true)
        {
                m[0].SetActive(true);
            if (flag <= m.Length - 1)
            {
                if (m[flag].GetComponent<nextscentence>().end == true)
                {
                    flag++;
                    m[flag].SetActive(true);
                    if (flag <= m.Length - 1)
                    {
                            m[flag].SetActive(true);
                    }
                    if (flag >= 1&&flag<=1&&this.name=="Talk")
                    {
                        k = new Vector3(this.transform.position.x, this.transform.position.y + 2.7f, this.transform.position.z);
                        move = true;
                    }
                    else if (flag >= 2&&this.name=="Talk")
                    {
                        k = new Vector3(this.transform.position.x, this.transform.position.y + 1.7f, this.transform.position.z);
                        move = true;
                    }
                    else if (flag >= 3 && this.name == "tex")
                    {
                        k = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
                        move = true;
                    }
                }
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, k, 2.5f * Time.deltaTime);
        }
        if(flag>= m.Length - 1)
        {
            GameObject.Find("p1").GetComponent<click5>().end = true;
            GameObject.Find("p2").GetComponent<click5>().end = true;
        }
    }
    IEnumerator waitAnimation3()
    {
        yield return new WaitForSeconds(3f);
        flag++;

    }
}

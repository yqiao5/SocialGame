using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class display2 : MonoBehaviour
{
    public List<GameObject> dias;
    public int flag;
    private float k;
    public int start;
    public float curtime;
    private int m = 0;
    private Vector3 a;
    void Start()
    {
        for (int i = 0; i <= 13; i++)
        {
            dias[i].SetActive(false);
        }
        this.transform.position = new Vector3(0, -2.8f, -1.9f);
        k = -2.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == 0)
        {
            this.transform.position = new Vector3(-0.68f, -7.27f, -1.9f);
            k = -7.27f;
        }
        if (start == 1)
        {
            if (curtime == 0)
            {
                curtime = Time.time;
            }
            if (Time.time - curtime > 1.2 && m <= 3)
            {
                dias[m].SetActive(true);
                m++;
                k = k + 1.5f;
                a = new Vector3(0, k, this.transform.position.z);
                curtime = Time.time;
            }
            if (m >= 1)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, a, 5 * Time.deltaTime);
            }
        }
    }
}

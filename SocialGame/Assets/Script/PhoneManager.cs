using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> dias;
    public float curtime;
    public float tim2;
    public float tim3;
    public Vector3 m;
    public int flag;
    private int dan;
    public int ch1;
    void Start()
    {
        dan = 0;
        for(int i = 0; i <= 13; i++)
        {
            dias[i].SetActive(false);
        }
        dias[0].SetActive(true);
        curtime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ch1 == 1)
        {
            if (tim3 == 0)
            {
                tim3 = Time.time;
            }
            if (Time.time - tim3 > 1.2)
            {
                m = new Vector3(this.transform.position.x, this.transform.position.y + 1.4f, this.transform.position.z);
                tim3 = Time.time;
                dias[dan].SetActive(false);
                dan++;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, m, 5 * Time.deltaTime);
        }
        if (Time.time - curtime > 1.2&&flag<=14)
        {
            dias[flag].SetActive(true);
            flag++;
            m = new Vector3(this.transform.position.x, this.transform.position.y + 1.35f, this.transform.position.z);
            curtime = Time.time;
            if (flag == 4)
            {
                tim2 = Time.time;
            }
        }
        if (Time.time - curtime > 1.2 && flag >= 14&&flag<=16)
        {
            flag++;
            m = new Vector3(this.transform.position.x, this.transform.position.y + 1.35f, this.transform.position.z);
            curtime = Time.time;
        }
        if (flag >= 5&&ch1==0)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, m, 5 * Time.deltaTime);
        }
        if (Time.time - tim2 > 1.2 && flag <= 14&&tim2!=0)
        {
            dias[dan].SetActive(false);
            dan++;
            tim2 = Time.time;
        }
    }
}

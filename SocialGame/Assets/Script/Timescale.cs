using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timescale : MonoBehaviour
{
    // Start is called before the first frame update
    public int start;
    public float curtime;
    public string m;
    public char[] arr;
    void Start()
    {
        start = 1;
    }

    // Update is called once per frame
    void Update()
    {
        m = this.GetComponent<Text>().text;
        arr = m.ToCharArray();
        //Debug.Log(arr[4]);
        if (start == 1)
        {
            if (curtime == 0)
            {
                curtime = Time.time;
            }
            if (Time.time-curtime >= 60)
            {
                if (arr[4] == '9')
                {
                    arr[4] = '0';
                }
                else
                {
                    arr[4] = (char)((int)(arr[4] + 1));
                    //Debug.Log(arr[4]);
                }
                curtime = 0;
            }
        }
        string a = new string(arr);
        
        this.GetComponent<Text>().text = a;
    }
}

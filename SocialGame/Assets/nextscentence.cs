using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextscentence : MonoBehaviour
{
    public char[] a;
    public char[] b;
    public float curtime;
    public int flag;
    public bool end = false;
    public Vector3 k;
    void Start()
    {
        a = this.GetComponent<TextMesh>().text.ToCharArray();
        b= this.GetComponent<TextMesh>().text.ToCharArray();
        for(int i=0;i<= a.Length-1; i++)
        {
            b[i] = ' ';
        }
    }
    void Update()
    {
        if (curtime == 0)
        {
            curtime = Time.time;
        }
        if (Time.time - curtime > 0.05&&flag<=a.Length-1)
        {
            b[flag] = a[flag];
            flag++;
            curtime = 0;
            if(flag>= a.Length - 1)
            {
                end = true;
            }
        }
        string m = new string(b);
        this.GetComponent<TextMesh>().text = m;
    }

}

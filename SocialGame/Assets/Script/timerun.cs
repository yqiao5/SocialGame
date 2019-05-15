using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerun : MonoBehaviour
{
    // Start is called before the first frame update
    public int flag;
    public string m;
    public char[] arr;
    public int a;
    private float curtime;
    public bool end = false;
    void Start()
    {
        arr = this.GetComponent<Text>().text.ToCharArray();
        flag = 1;
        end = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (curtime == 0 && end == false)
        {
            curtime = Time.time;
        }
        if (Time.time - curtime >= 0.02 && end == false)
        {
            if (arr[4] == '9')
            {
                arr[4] = '0';
                if (arr[3] == '5')
                {
                    arr[3] = '0';
                    if (arr[1] == '9')
                    {
                        arr[1] = '0';
                        arr[0] = (char)((int)(arr[0]) + 1);
                    }
                    else
                    {
                        arr[1] = (char)((int)(arr[1]) + 1);
                    }
                }
                else
                {
                    arr[3] = (char)((int)(arr[3]) + 1);
                }
            }
            else
            {
                arr[4] = (char)((int)(arr[4]) + 1);

            }
        }
        m = new string(arr);
        if (arr[0] == '2' && a == 0&&flag==2)
        {
            m = "00:00";
            a = 1;
            this.GetComponent<Text>().text = m;
            arr = this.GetComponent<Text>().text.ToCharArray();
        }
        this.GetComponent<Text>().text = m;
        if (m == "21:00" && flag == 1)
        {
            end = true;
            //flag = 2;
        }
        else if (m == "09:30" && flag == 2)
        {
            end = true;
        }
    }

    public void Unlock()
    {
        if (end)
        {
            PanelManager.Instance.PopPanel();
        }
    }

    public void SetEndFlag()
    {
        end = false;
    }
}


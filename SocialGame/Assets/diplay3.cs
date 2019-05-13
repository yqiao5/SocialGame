using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diplay3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] m;
    public int flag;
    public Vector3 k;
    void Start()
    {
        for(int i = 0; i <= 6; i++)
        {
            m[i].SetActive(false);
            k = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m[0].SetActive(true);
        if (flag <= 6)
        {
            if (m[flag].GetComponent<nextscentence>().end == true)
            {
                flag++;
                if (flag <= 6)
                {
                    m[flag].SetActive(true);
                }
                if (flag >= 3)
                {
                    k = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
                }
            }
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, k, 2 * Time.deltaTime);
    }
}

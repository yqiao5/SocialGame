using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float curtime;
    private Vector3 k;
    void Start()
    {
        k = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Talk").GetComponent<diplay3>().start == true)
        {
                if (GameObject.Find("Talk").GetComponent<diplay3>().move == true)
                {
                    k = new Vector3(this.transform.position.x, this.transform.position.y+2.7f, this.transform.position.z);
                    GameObject.Find("Talk").GetComponent<diplay3>().move = false;
                }
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, k, 2.5f * Time.deltaTime);

    }
}

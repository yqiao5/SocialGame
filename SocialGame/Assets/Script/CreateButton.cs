using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string tex;
    void Start()
    {
        tex = this.gameObject.GetComponentInChildren<Text>().text;
        if (tex.Length > 1)
        {
            GetComponent<RectTransform>().localScale = new Vector3(2, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

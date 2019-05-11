using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDel : MonoBehaviour
{
    public GameObject temp;
    private static bool m_IsHaveOne = false;

    private GameObject m_clone;
    // Start is called before the first frame update
    void Start()
    {
        if (!m_IsHaveOne)
        {
            m_clone = Instantiate(temp) as GameObject;
            DontDestroyOnLoad(m_clone);
            m_IsHaveOne = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

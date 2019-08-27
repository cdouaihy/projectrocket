using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    bool isOn = false;
    [SerializeField] GameObject door;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOn()
    {
        if (!isOn)
        {
            isOn = true;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        }
    }
}

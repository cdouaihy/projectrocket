using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    bool isOn = false;
    bool isTurning = false;
    [SerializeField] GameObject door;
    [SerializeField] float limit;
    [SerializeField] int speed;
    [SerializeField] bool lessthan;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurning) {

            if (lessthan && door.transform.localEulerAngles.z  < limit)
            {
                isTurning = false;
            }
            else if (!lessthan && door.transform.localEulerAngles.z > limit)
            {
                isTurning = false;
            }
            else
            {
                Debug.Log(door.transform.localEulerAngles.z);
                door.transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            }
        }
    }

    public void turnOn()
    {
        if (!isOn)
        {
            isOn = true;
            isTurning = true;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        }
    }
}

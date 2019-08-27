using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    bool isOn = false;
    bool isTurning = false;
    [SerializeField] GameObject door;
    [SerializeField] int speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTurning) {

            if (door.transform.localEulerAngles.z  < 270)
            {

                isTurning = false;
            }
            else{
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

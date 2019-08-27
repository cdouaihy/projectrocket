using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    AudioSource audioSource;
    bool isOn = false;
    bool isTurning = false;
    [SerializeField] GameObject door;
    [SerializeField] float limit;
    [SerializeField] int speed;
    [SerializeField] bool lessthan;

    [SerializeField] AudioClip turned;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(turned);
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        }
    }
}

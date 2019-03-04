using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour {

    [SerializeField] Vector3 vec = new Vector3(0, 5f, 0);
    [SerializeField] float period = 2f;
    float movementFactor;

    Vector3 startPos;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        movementFactor = Mathf.Sin(tau * cycles);

        Vector3 offset = vec * movementFactor;
        transform.position = startPos + offset;
	}
}

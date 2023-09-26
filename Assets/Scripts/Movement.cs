using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0) transform.position += new Vector3(0, 0, -Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        else if (Input.GetAxis("Vertical") != 0) transform.position += new Vector3(Input.GetAxis("Vertical") * speed * Time.deltaTime, 0, 0);
        else { }

    }
}

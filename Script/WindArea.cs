using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour {

    public bool goForward, preserveVelocity;
    public float strength;
    public Vector3 direction;

    private void OnTriggerEnter(Collider other)
    {
       if( other.tag == "Sheep")
        {
            if(!preserveVelocity)
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (!goForward)
                other.GetComponent<Rigidbody>().AddForce(direction * strength);
            else
                other.GetComponent<Rigidbody>().AddForce(Vector3.forward * strength); 
        }
    }

}

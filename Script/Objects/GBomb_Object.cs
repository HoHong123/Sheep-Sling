using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBomb_Object : MonoBehaviour
{


    public GameObject Particle;
    public float radius = 3;
    public float Force = 700;
    public AudioSource Boom_Sound;

    public bool Drop = false;
    private Vector3 Start_Pos = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        Start_Pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Drop == true)
        {
            Drop_Check();
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider NearOdject in colliders)
        {
            Rigidbody rb = NearOdject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Force, transform.position, radius);
            }
        }
        GameObject.Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Explode();
            GameObject tmp = GameObject.Instantiate(Particle, this.transform.position, this.transform.rotation);
            tmp.GetComponent<AudioSource>().clip = Boom_Sound.clip;
            tmp.GetComponent<AudioSource>().Play();
            GameObject.Destroy(tmp, 3);
        }
    }

    public void Drop_Bomd()
    {
        Drop = true;
    }

    public void Drop_Check()
    {
        if (Start_Pos.y - 200 > this.transform.position.y)
        {
            GameObject.Destroy(this.transform.gameObject);
        }
    }
    public void Set_Range(float Range)
    {
        radius = Range;
    }
}
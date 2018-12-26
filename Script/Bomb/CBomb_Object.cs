using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBomb_Object : MonoBehaviour
{
    public float explosionDelay;
    public GameObject Particle;
    public float radius = 3;
    public float Force = 700;
    public AudioSource Boom_Sound;

    public bool Drop = false;
    private Vector3 Start_Pos = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        Start_Pos = transform.position;
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

        GameObject tmp = GameObject.Instantiate(Particle, transform.position, transform.rotation);
        tmp.GetComponent<AudioSource>().clip = Boom_Sound.clip;
        tmp.GetComponent<AudioSource>().Play();
        Destroy(tmp, 3);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider NearOdject in colliders)
        {
            if (NearOdject.name == "CBomb")
            {
                StartCoroutine("wait");
                NearOdject.GetComponent<CBomb_Object>().Explode();
            }

            Rigidbody rb = NearOdject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sheep")
        {
            Explode();
            Destroy(collision.gameObject);
        }
    }

    public void Drop_Bomd()
    {
        Drop = true;
    }

    public void Drop_Check()
    {
        if (Start_Pos.y - 200 > transform.position.y)
        {
            Destroy(transform.gameObject);
        }
    }
    public void Set_Range(float Range)
    {
        radius = Range;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(explosionDelay);
    }
}
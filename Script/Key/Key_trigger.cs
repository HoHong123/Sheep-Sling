using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_trigger : MonoBehaviour {

    [HideInInspector]
    public bool isEnable = false;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private string type;

    private void OnDisable()
    {
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 1f);
        Destroy(gameObject, 1);
    }

    private void Update()
    {

        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == type)
        {
            isEnable = true;
            gameObject.SetActive(false);
        }

    }

}

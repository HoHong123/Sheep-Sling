using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buoyancy : MonoBehaviour
{
    public float UpwardForce = 12.72f;
    // 9.81은 기본 중력, 기본 중력보다 높아야 물에 뜸
    public List<Object> rbList = new List<Object>();
    
    public struct Object{
        public GameObject ID;
        public Rigidbody rb;
        public Transform pos;
        public Vector3 dir;

        public Object(GameObject oid, Vector3 direction)
        {
            ID = oid;
            rb = oid.GetComponent<Rigidbody>();
            pos = oid.GetComponent<Transform>();
            dir = direction;

            rb.drag = 5.0f;
        }
    };
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            Object ob;
            
            if (other.CompareTag("Ground"))
            {
                float angle = other.gameObject.transform.rotation.z - transform.rotation.z;
                Debug.Log(angle);

                if(angle < 0)
                    ob = new Object(other.gameObject, Vector3.right);
                else
                    ob = new Object(other.gameObject, Vector3.left);

                Debug.Log(ob.dir);
            }
            else
            {
                float angle = other.gameObject.transform.rotation.y - transform.rotation.y;

                if (angle < 0)
                    ob = new Object(other.gameObject, Vector3.down);
                else
                    ob = new Object(other.gameObject, Vector3.up);
            }

            rbList.Add(ob);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(rbList.Count != 0)
        {
            GameObject go = other.gameObject;

            int i = 0;
            while (i <= rbList.Count)
            {
                if (go == rbList[i].ID)
                {
                    rbList[i].rb.drag = 0.05f;
                    rbList.RemoveAt(i);

                    break;
                }

                i++;
            }

        }
    }

    void FixedUpdate()
    {
        if (rbList.Count != 0)
        {
            for (int i = 0; i < rbList.Count; i++)
            {
                Vector3 force = transform.up * UpwardForce;
                rbList[i].rb.AddForce(force, ForceMode.Acceleration);
                
                Quaternion targetRot = Quaternion.LookRotation(rbList[i].dir - rbList[i].pos.position);
                rbList[i].pos.rotation = Quaternion.Slerp(rbList[i].pos.rotation, targetRot, Time.deltaTime * 0.99f);

            }
        }
    }
}
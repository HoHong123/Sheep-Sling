using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartKeyManager : MonoBehaviour {

    public GameObject[] startKeys;
    public Transform[] endKeys;

    [SerializeField]
    private float speed, waitingTime;
    private int next;
    
    // Use this for initialization
    void Start() {
        next = 0;
    }

    // Update is called once per frame
    void Update() {

        if (next == 3)
            Destroy(this);

        if (waitingTime <= 0)
        {

            for (; next < 3; next++)
            {
                if (startKeys[next] != null)
                {
                    if (2.0f < Vector3.Distance(startKeys[next].transform.position, endKeys[next].position))
                        MoveToPosition(startKeys[next].transform, endKeys[next].position, speed);
                    else
                    {
                        startKeys[next].GetComponent<Key_trigger>().enabled = false;
                        Destroy(startKeys[next]);
                    }
                }
            }

            next = 0;

        }
        else
        {
            waitingTime -= Time.deltaTime;
        }
    }

    public void MoveToPosition(Transform transform, Vector3 position, float speed)
    {

        Vector3 currentPos = transform.position;

        transform.position = Vector3.MoveTowards(currentPos, position, 10/speed * Time.deltaTime);
        
    }
    
}

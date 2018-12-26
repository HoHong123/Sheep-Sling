using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightController : MonoBehaviour {

    [SerializeField]
    private bool direction;
    public float speed = 1;

    private float maxHeight = 1f;
    private float minHeight = 0.5f;
    private float y;

    public GameObject slingShot;

    private void Awake()
    {
        y = slingShot.transform.localPosition.y;

        if (direction)
            speed *= 1f;
        else
            speed *= -1f;

    }

    // Use this for initialization
    void OnEnable ()
    {

	}

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update () {

        slingShot.transform.localPosition += new Vector3(0, (speed * Time.deltaTime), 0);

        if(slingShot.transform.localPosition.y >= y + maxHeight)
        {
            slingShot.transform.localPosition = new Vector3(-0.175f, y + maxHeight, 2.959999f);
        } else if (slingShot.transform.localPosition.y <= y - minHeight)
        {
            slingShot.transform.localPosition = new Vector3(-0.175f, y - minHeight, 2.959999f);
        }
    }
}

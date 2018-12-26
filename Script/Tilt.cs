using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour {

    [SerializeField]
    private float tilt, tmp;

    public float speed;

	// Use this for initialization
	void Start () {

        tmp = 0;

    }
	
	// Update is called once per frame
	void Update () {
        tmp = transform.localEulerAngles.x;

        if (tmp > 180)
            tmp = 360 - tmp;

        if (tmp > Mathf.Abs(tilt) - 1)
            tilt *= -1;

        float angle = Mathf.LerpAngle(transform.rotation.z, tilt, Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.Euler(tilt, -122, 100), Time.deltaTime * speed);


    }
}

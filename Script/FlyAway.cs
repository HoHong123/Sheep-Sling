using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour {

    public float speed, rot;
    private float front;

    private Vector2 direction;
    private Rigidbody2D rig;

	// Use this for initialization
	void Start () {
        
        rot = 0;
        front = 0.05f;
        
        speed = Random.Range(10, 20);

        rig = this.GetComponent<Rigidbody2D>();

        direction = new Vector2(Random.Range(-300, 300.0f), Random.Range(200.0f, 400.0f));
        rig.AddForce(direction);

        if (direction.x >= 0)
            speed *= -1;

	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.forward, rot);
        transform.position = transform.position - new Vector3(0,0l,front);

        rot += speed * Time.deltaTime;

    }
}

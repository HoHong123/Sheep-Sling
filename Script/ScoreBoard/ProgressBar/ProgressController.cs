using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour {

    [SerializeField]
    private ProgressStat stat;

    public Transform start, end, target;
    private float range;

    private void Awake()
    {
        stat.Initialize();
    }

    // Use this for initialization
    void Start () {
        range = Vector3.Distance(start.position, end.position);
	}
	
	// Update is called once per frame
	void Update () {

        float distance = range - Vector3.Distance(target.position, end.position);

        distance = (distance / range) * 100;

        stat.CurrentVal = distance;

	}
}

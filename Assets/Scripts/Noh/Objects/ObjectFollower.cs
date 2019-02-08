using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour {
    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        gameObject.transform.position = target.transform.position;
    }
}

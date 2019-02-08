using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollower : MonoBehaviour {
    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void LateUpdate()
    {
        Movefunc();
    }
    void Movefunc()
    {
        Vector3 repPos = target.transform.position;
        repPos.y += transform.lossyScale.y * 0.8f;
        Vector3 pos = Camera.main.WorldToScreenPoint(repPos);
        transform.position = pos;
    }
}

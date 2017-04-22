using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public GameObject PrimaryVessel;
    public GameObject SecondaryVessel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PrimaryVessel != null && SecondaryVessel != null)
        {
            var primaryPosition = PrimaryVessel.transform.position;
            SecondaryVessel.transform.position = primaryPosition;
        }
	}
}

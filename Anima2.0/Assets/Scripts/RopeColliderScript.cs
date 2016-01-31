using UnityEngine;
using System.Collections;

public class RopeColliderScript : MonoBehaviour {
    BoxCollider2D Coll;
    float Counter=0.0f;
	// Use this for initialization
	void Start () {

        Coll = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!Coll.enabled)
        {
            Counter += Time.deltaTime;
        }
        if(Counter>=2.0f)
        {
            Coll.enabled = true;
            Counter = 0.0f;
        }
	
	}
}

using UnityEngine;
using System.Collections;

public class toadstool : MonoBehaviour {
    bool StopMoving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D Col)
    {
        if(Col.gameObject.tag=="Platform")
        {
            if(!StopMoving)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 400));

        }
    }
    void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.gameObject.tag=="StopTS")
        {
            StopMoving = true;
        }
    }
}

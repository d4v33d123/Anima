using UnityEngine;
using System.Collections;

public class BoatScript : MonoBehaviour {
    float Counter=0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Counter += Time.deltaTime;
        if (Counter >= 10)
       
            GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
      
	}
}

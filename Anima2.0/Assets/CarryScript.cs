using UnityEngine;
using System.Collections;

public class CarryScript : MonoBehaviour {
    float Counter = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Counter += Time.deltaTime;
        if(Counter<=2)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        else if(Counter<=7)
            GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
        else if (Counter <= 10)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);




	}
}

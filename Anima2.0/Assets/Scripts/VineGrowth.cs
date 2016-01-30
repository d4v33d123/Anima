using UnityEngine;
using System.Collections;

public class VineGrowth : MonoBehaviour {
    BoxCollider2D Collider;
    float CurrentTime = 0.0f;
	// Use this for initialization
	void Start () {
        Collider = GetComponent<BoxCollider2D>();
        Collider.isTrigger = false;

	}
	
	// Update is called once per frame
	void Update () {
        CurrentTime += Time.deltaTime;
        if (CurrentTime <= 2)
        {
            Collider.size = Collider.size + new Vector2(0.1f, 0);
            Collider.offset = Collider.offset + new Vector2(0.05f, 0);
        }
        else
            Collider.isTrigger = true;
	}
}

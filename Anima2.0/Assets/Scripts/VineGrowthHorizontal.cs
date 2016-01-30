using UnityEngine;
using System.Collections;

public class VineGrowthHorizontal : MonoBehaviour {
    BoxCollider2D Collider;
    float CurrentTime = 0.0f;
    public int LatchedCount = 0;
    public bool DontRunAgain=false;

	// Use this for initialization
	void Start () {
        Collider = GetComponent<BoxCollider2D>();
        Collider.isTrigger = true;
       
	}
	
	// Update is called once per frame
	void Update () {
       

        CurrentTime += Time.deltaTime;
        if (CurrentTime <= 2)
        {
            Collider.size = Collider.size + new Vector2(0.1f, 0);
            Collider.offset = Collider.offset + new Vector2(0.05f, 0);
        }
        else if(!DontRunAgain)
        {
            if (LatchedCount >= 2)
            {


                Collider.isTrigger = false;
            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                Collider.isTrigger = false;

                GameObject.Destroy(gameObject, 3f);
            }
            DontRunAgain = true;

        }
	}
    /*
    void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.tag=="Platform")
        {
            LatchedCount++;
            Debug.Log("Latched is" + LatchedCount);
        }
    }
    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "Platform")
        {
            LatchedCount--;
            Debug.Log("Latched is" + LatchedCount);

        }
    }
     * */
}

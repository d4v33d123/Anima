using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "VineH")
        {
            Col.GetComponent<VineGrowthHorizontal>().LatchedCount++;
            Debug.Log("Latched is" + Col.GetComponent<VineGrowthHorizontal>().LatchedCount);
        }
    }
    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "VineH")
        {
            Col.GetComponent<VineGrowthHorizontal>().LatchedCount--;
            Debug.Log("Latched is" + Col.GetComponent<VineGrowthHorizontal>().LatchedCount);

        }
    }
}

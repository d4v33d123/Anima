using UnityEngine;
using System.Collections;

public class RopeScript : MonoBehaviour {
    public int seedNumber;
    public GameObject Seed;
    float CurrentTime = 0.0f;

    public bool Recall;
	// Use this for initialization
	void Start () {
        GetComponent<HingeJoint2D>().connectedAnchor = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Backspace))
            Recall = true;
        CurrentTime += Time.deltaTime;
	     if (CurrentTime >= 30.0f || Recall)
        {

            // Debug.Log("I'm Counting(HOPEFULLY)");

            //GameObject.Find(seedNumber.ToString()).SetActive(false);
            // Debug.Log("I'M ALIVE");
            Recall = false;
            if (GameObject.Find("hero").GetComponent<PlayerControl>().SeedAmmo < 7)
            {
                GameObject.Find("hero").GetComponent<PlayerControl>().SeedAmmo += 1;
            }
            Seed.SetActive(true);
            GameObject.Destroy(gameObject);



        }
	}
    public void SetSeedNumber(int seed)
    {
        seedNumber = seed;
        Seed = GameObject.Find(seed.ToString());
        Seed.SetActive(false);
    }
}

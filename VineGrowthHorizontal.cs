using UnityEngine;
using System.Collections;

public class VineGrowthHorizontal : MonoBehaviour {
    BoxCollider2D Collider;
    float CurrentTime = 0.0f;
    public int LatchedCount = 0;
    public bool DontRunAgain=false;
	public int seedNumber;
	public bool Recall = false;
	public GameObject Seed;

	// Use this for initialization
	void Start () {
        Collider = GetComponent<BoxCollider2D>();
        Collider.isTrigger = true;
       
	}
	
	// Update is called once per frame
	void Update () {
       

        CurrentTime += Time.deltaTime;
        if (CurrentTime <= 2) {
			Collider.size = Collider.size + new Vector2 (0.1f, 0);
			Collider.offset = Collider.offset + new Vector2 (0.05f, 0);
		} else if (!DontRunAgain) {
			if (LatchedCount >= 2) {


				Collider.isTrigger = false;
			} else {
				GetComponent<Rigidbody2D> ().gravityScale = 1;
				GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
				Collider.isTrigger = false;
				if(GameObject.Find("hero").GetComponent<PlayerControl>().SeedAmmo < 6)
				{
					GameObject.Find("hero").GetComponent<PlayerControl>().SeedAmmo += 1;
				}
				Debug.Log(seedNumber);
				Seed.SetActive(true);
				GameObject.Destroy (gameObject, 3f);
			}
			DontRunAgain = true;

		}
		else if (CurrentTime >= 30.0f || Recall)
		{

			//GameObject.Find(seedNumber.ToString()).SetActive(false);
			Debug.Log("I'M ALIVE");
			Recall = false;
			if(GameObject.Find("hero").GetComponent<PlayerControl>().SeedAmmo < 6)
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
		Seed=GameObject.Find(seed.ToString());
		Seed.SetActive(false);
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

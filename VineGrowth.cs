using UnityEngine;
using System.Collections;

public class VineGrowth : MonoBehaviour {
    BoxCollider2D Collider;
    float CurrentTime = 0.0f;
	public int seedNumber;
	public bool Recall = false;
	public GameObject Seed;
	// Use this for initialization
	void Start () {
        Collider = GetComponent<BoxCollider2D>();
        Collider.isTrigger = false;

	}
	
	// Update is called once per frame
	void Update () {
        CurrentTime += Time.deltaTime;
        if (CurrentTime <= 2) {
			Collider.size = Collider.size + new Vector2 (0.1f, 0);
			Collider.offset = Collider.offset + new Vector2 (0.05f, 0);
		}
		else if (CurrentTime >= 30.0f || Recall)
		{

			Debug.Log ("I'm Counting(HOPEFULLY)");
			if(CurrentTime >= 4.0f)
			{
				//GameObject.Find(seedNumber.ToString()).SetActive(false);
				Debug.Log("I'M ALIVE");
				Recall = false;
				if(GameObject.Find("hero").GetComponent<PlayerControl>().SeedAmmo < 7)
				{
					GameObject.Find("hero").GetComponent<PlayerControl>().SeedAmmo += 1;
				}
				Seed.SetActive(true);
				GameObject.Destroy(gameObject);
				
			}

		}
        else
            Collider.isTrigger = true;
	}

	public void SetSeedNumber(int seed)
	{
		seedNumber = seed;
		Seed=GameObject.Find(seed.ToString());
		Seed.SetActive(false);
	}

}

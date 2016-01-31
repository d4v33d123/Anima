﻿using UnityEngine;
using System.Collections;

public class SeedScript : MonoBehaviour {
    PlayerControl.SeedDir SeedDir;
    public bool FacingRight = false;
    public bool StartCalc=false;
	public bool Active = false;
    public GameObject VineH, VineV, Seed;
    GameObject Vine;
    float Counter = 0f;
	public int SeedNumber;

	// Use this for initialization
	void Start () {
        SeedDir = GameObject.Find("hero").GetComponent<PlayerControl>().SD;
        FacingRight = GameObject.Find("hero").GetComponent<PlayerControl>().facingRight;
		SeedNumber = GameObject.Find ("hero").GetComponent<PlayerControl> ().SeedAmmo;
        Debug.Log(SeedDir);
        StartCalc = true;

	}
	
	// Update is called once per frame
	void Update () {
        Counter+=Time.deltaTime;
			if (StartCalc) {
				Debug.Log ("StartCalc" + SeedDir);
				switch (SeedDir) {
				case PlayerControl.SeedDir.Left:
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-300, 300));
                    
                    
					break;
				case PlayerControl.SeedDir.Right:
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (300, 300));

                    

					break;
				case PlayerControl.SeedDir.Up:
					if (FacingRight)
						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (300, 300));
					else
						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-300, 300));

                   

					break;
				case PlayerControl.SeedDir.Down:
                    //GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);

					break;
				case PlayerControl.SeedDir.UpLeft:
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-300, 300));

					break;
				case PlayerControl.SeedDir.UpRight:
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (300, 300));

					break;


				}
				StartCalc = false;
				//Seed=GameObject.Find(SeedNumber.ToString());
			//Seed.SetActive(false);

			}
		
			if (Counter >= 2.0f) {

	       
				switch (SeedDir) {
				case PlayerControl.SeedDir.Left:
					Vine = Instantiate (VineH, transform.position - new Vector3 (0, 1, 0), Quaternion.identity) as GameObject;
					Vine.transform.localScale = new Vector3 (-Vine.transform.localScale.x, 1, 1);
					Vine.GetComponent<VineGrowthHorizontal>().SetSeedNumber(SeedNumber);
					GameObject.Destroy (gameObject);
					break;
				case PlayerControl.SeedDir.Right:
					Vine = Instantiate (VineH, transform.position - new Vector3 (0, 1, 0), Quaternion.identity) as GameObject;
					Vine.GetComponent<VineGrowthHorizontal>().SetSeedNumber(SeedNumber);
					GameObject.Destroy (gameObject);
				Debug.Log ("Destroyed GO");
					break;
				case PlayerControl.SeedDir.Up:
					Vine = Instantiate (VineV, transform.position, Quaternion.identity) as GameObject;
					Vine.transform.Rotate (0, 0, 90, Space.Self);
					Vine.GetComponent<VineGrowth>().SetSeedNumber(SeedNumber);
					GameObject.Destroy (gameObject);
					break;
				case PlayerControl.SeedDir.Down:
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -5);

					break;
				case PlayerControl.SeedDir.UpLeft:
					Vine = Instantiate (VineH, transform.position - new Vector3 (0, 1, 0), Quaternion.identity) as GameObject;
					Vine.transform.Rotate (0, 0, 315, Space.Self);

					Vine.transform.localScale = new Vector3 (-Vine.transform.localScale.x, 1, 1);
					Vine.GetComponent<VineGrowthHorizontal>().SetSeedNumber(SeedNumber);
					GameObject.Destroy (gameObject);
					break;
				case PlayerControl.SeedDir.UpRight:
					Vine = Instantiate (VineH, transform.position - new Vector3 (0, 1, 0), Quaternion.identity) as GameObject;

					Vine.transform.Rotate (0, 0, 45, Space.Self);
					Vine.GetComponent<VineGrowthHorizontal>().SetSeedNumber(SeedNumber);
					GameObject.Destroy (gameObject);
					break;


				}
			}




	}
    public void SeedDirection(PlayerControl.SeedDir SD)
    {
        SeedDir = SD;
        StartCalc = true;
    }
}

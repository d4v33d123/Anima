/*

using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;
public class kame2 : MonoBehaviour {
	
	
	bool a ;
	public BoxCollider2D coll;
	
	public int counter =0 ;
	public bool notplayed = true;
	public GameObject pll=null;
	public PlatformerCharacter2D playerScript=null;
	
	
	
	
	
	// Use this for initialization
	void Start () {
		pll = GameObject.Find ("Player");
		playerScript = pll.GetComponent<PlatformerCharacter2D> ();
		a = playerScript.facingRight;

		coll = GetComponent <BoxCollider2D> ();
		notplayed = true;
		if (!a) {
			transform.localScale -= new Vector3(0.01F, 0, 0);
			transform.localPosition +=new Vector3(1f,0,0);
		}
		/*
		if (PlatformerCharacter2D.a2flag) {
			if(a)
			{
				transform.localScale-=new Vector3(0.02F,0,0);
				transform.localPosition+=new Vector3(5f,0,0);
			}
			else{
				transform.localScale+=new Vector3(0.02F,0,0);
				transform.localPosition-=new Vector3(5f,0,0);
				
			}

		}
		*/
		
		
	/*	
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		counter ++;
		Destroy(gameObject, 3.6f);
		if (counter > 15&&counter<200) {
						coll.size = coll.size + new Vector2 (100, 0);
						coll.center = coll.center + new Vector2 (50, 0);
				}
		if (counter > 15 && notplayed) {
						notplayed = false;
			Debug.Log ("3rd sound!");
						AudioManager.current.play_sfx ("kamehameha3");
				}



		
	}
	
	
}
*/
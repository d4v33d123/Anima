/*
 using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;
public class kame1 : MonoBehaviour {
	
	
	bool a ;
	public Transform kame;
	public int counter;
	public bool spawned = false;
	public GameObject pll=null;
	public PlatformerCharacter2D playerScript=null;

	
	
	
	
	
	
	
	
	// Use this for initialization
	void Start () {
		pll = GameObject.Find ("Player");
		playerScript = pll.GetComponent<PlatformerCharacter2D> ();

		a = playerScript.facingRight;
		AudioManager.current.play_sfx("kamehameha1");

		if (!a) {
			//transform.localScale -= new Vector3(0.005F, 0, 0);
			//transform.localPosition +=new Vector3(5,0,0);

		}
		counter = 0;
		/*

			if(a)
			{
				transform.localScale-=new Vector3(0.02F,0,0);
				transform.localPosition+=new Vector3(5f,0,0);
			}
			else{
				transform.localScale+=new Vector3(0.02F,0,0);
				transform.localPosition-=new Vector3(5f,0,0);
				
			}
			*/

		
		
		
		/*
		
		
	}
	
	// Update is called once per frame
	void Update () {
		counter++;

		Destroy(gameObject, 1.8f);
		if (counter > 50&&!spawned) {
			AudioManager.current.play_sfx("kamehameha2");

			counter=0;
			spawned=true;
			Vector3 atStartPoint;
			atStartPoint = transform.position;
			
			//atStartPoint.x -= 0.5f;
			if (!a) {
				atStartPoint.x += 1f;
			}
			
			//atStartPoint.y += 1f;
			//anim.SetBool ("at1", true);
			Instantiate (kame, atStartPoint, transform.rotation);

				}
		
	}
	
	
}
*/
using UnityEngine;
using System.Collections;

public class SeedScript : MonoBehaviour {
    PlayerControl.SeedDir SeedDir;
    public bool FacingRight = false;
    public bool StartCalc=false;
    public GameObject VineH, VineV, Rope;
    GameObject Vine;
    float Counter = 0f;

    public int SeedNumber;


	// Use this for initialization
	void Start () {
        SeedDir = GameObject.Find("hero").GetComponent<PlayerControl>().SD;
        FacingRight = GameObject.Find("hero").GetComponent<PlayerControl>().facingRight;
        Debug.Log(SeedDir);
        SeedNumber = GameObject.Find("hero").GetComponent<PlayerControl>().SeedAmmo;

        StartCalc = true;

	}
	
	// Update is called once per frame
	void Update () {
        Counter+=Time.deltaTime;
        if(StartCalc)
        {
            Debug.Log("StartCalc"+SeedDir);
            switch(SeedDir)
            {
                case PlayerControl.SeedDir.Left:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 300));
                    
                    
                    break;
                case PlayerControl.SeedDir.Right:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 300));

                    

                    break;
                case PlayerControl.SeedDir.Up:
                    if(FacingRight)
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(300,300));
                    else
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 300));

                   

                    break;
                case PlayerControl.SeedDir.Down:
                    //GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);

                    break;
                case PlayerControl.SeedDir.UpLeft:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 300));

                    break;
                case PlayerControl.SeedDir.UpRight:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 300));

                    break;


            }
            StartCalc = false;
        }
        if(Counter>=2.0f)
        {

           
            switch (SeedDir)
            {
                case PlayerControl.SeedDir.Left:
                    Vine = Instantiate(VineH, transform.position - new Vector3(4, 1, 0), Quaternion.identity) as GameObject;
                    Vine.transform.localScale = new Vector3(-Vine.transform.localScale.x, 1, 1);
                    Vine.GetComponent<VineGrowthHorizontal>().SetSeedNumber(SeedNumber);

                    GameObject.Destroy(gameObject);

                    break;
                case PlayerControl.SeedDir.Right:
                    Vine = Instantiate(VineH, transform.position - new Vector3(-4,1,0), Quaternion.identity) as GameObject;
                    Vine.GetComponent<VineGrowthHorizontal>().SetSeedNumber(SeedNumber);

                    GameObject.Destroy(gameObject);

                    break;
                case PlayerControl.SeedDir.Up:
                    Vine = Instantiate(VineV, transform.position + new Vector3(0,3.5f,0), Quaternion.identity) as GameObject;
                    Vine.transform.Rotate(0, 0, 90, Space.Self);
                    Vine.GetComponent<VineGrowth>().SetSeedNumber(SeedNumber);
	
                    GameObject.Destroy(gameObject);



                    break;
                case PlayerControl.SeedDir.Down:
                    Vine = Instantiate(Rope, transform.position-new Vector3(0,2,0), Quaternion.identity) as GameObject;
                    Vine.GetComponent<RopeScript>().SetSeedNumber(SeedNumber);

                    GameObject.Destroy(gameObject);



                    break;
                case PlayerControl.SeedDir.UpLeft:
                    Vine = Instantiate(VineH, transform.position - new Vector3(3, -2, 0), Quaternion.identity) as GameObject;
                    Vine.transform.Rotate(0, 0, 315, Space.Self);
                    Vine.GetComponent<VineGrowthHorizontal>().SetSeedNumber(SeedNumber);


                    Vine.transform.localScale = new Vector3(-Vine.transform.localScale.x, 1, 1);
                    GameObject.Destroy(gameObject);

                    break;
                case PlayerControl.SeedDir.UpRight:
                    Vine = Instantiate(VineH, transform.position - new Vector3(-3, -2, 0), Quaternion.identity) as GameObject;
                    Vine.GetComponent<VineGrowthHorizontal>().SetSeedNumber(SeedNumber);


                    Vine.transform.Rotate(0, 0, 45, Space.Self);

                    GameObject.Destroy(gameObject);

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

using UnityEngine;
using System.Collections;

public class StickyBlock : MonoBehaviour {
    GameObject ActorObject;
    GameObject ActorHead;
    
    playerLauncher PlayerScript;
    
    Collider2D BlockCollider;

    float GravityTimer = 0f;
    bool GravityToBeDisabled = false;

    Vector3 CollisionPosition;

    
	// Use this for initialization
	void Start () {
        ActorObject = GameObject.Find("Actor");
        ActorHead = GameObject.Find("Main");
        PlayerScript = ActorHead.GetComponent<playerLauncher>();
        BlockCollider = this.GetComponent<BoxCollider2D>() as Collider2D;
        if(BlockCollider==null)
            BlockCollider = this.GetComponent<PolygonCollider2D>() as Collider2D;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(GravityToBeDisabled)
        {
            GravityTimer -= Time.deltaTime;

        }
        if(GravityToBeDisabled && GravityTimer <=0.0f)
        {
            foreach (GameObject obj in PlayerScript.ActorParts)
            {
                //obj.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                obj.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            GravityToBeDisabled = false;
        }
	
	}
    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Limb" && !PlayerScript.ColliderIsDisabled)
        {
            if (!PlayerScript.Attached)
            {
                PlayerScript.Attached = true;
                PlayerScript.AttachedCount++;
               // PlayerScript.Detach = false;
                Debug.Log("Attached is now true"+PlayerScript.AttachedCount);
//                ActorHead.GetComponent<SpringJoint2D>().enabled = false;
                col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                int i = 0;
                foreach (GameObject obj in PlayerScript.ActorParts)
                {
                    //obj.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    //PlayerScript.JointPositions[i] = obj.anchor;
                    //i++;
                    if (obj.name != "Main")
                    {
                        PlayerScript.JointPositions[i] = obj.transform.position;
                        i++;
                    }
            




                }
                
                    CollisionPosition = col.transform.position;
                /*
                foreach(GameObject obj in PlayerScript.ActorParts)
                {
                    obj.transform.position = CollisionPosition;
                }
                 * */

              //  ActorObject.transform.parent = gameObject.transform;
                //col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                col.transform.position = CollisionPosition;

                GravityTimer = 0.2f;
                GravityToBeDisabled = true;
               
            }

            
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Limb" )
        {
            if (!PlayerScript.Detach && PlayerScript.Attached && PlayerScript.AttachedCount == 1 && !PlayerScript.ColliderIsDisabled)
            {
                col.transform.position = CollisionPosition;
                int i = 0;
                foreach (GameObject obj in PlayerScript.ActorParts)
                {
                    //obj.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    //PlayerScript.JointPositions[i] = obj.anchor;
                    //i++;
                    if (obj.name != "Main")
                    {
                         obj.transform.position = PlayerScript.JointPositions[i];
                        i++;
                    }





                }
                foreach (HingeJoint2D obj in PlayerScript.Joints)
                {
                    //obj.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

                    //obj.anchor = PlayerScript.JointPositions[i];
                    i++;

                }
            }
            if (PlayerScript.Attached && PlayerScript.Detach && PlayerScript.AttachedCount == 1)
            {
                //ActorHead.GetComponent<SpringJoint2D>().enabled = true;
                PlayerScript.Attached = false;
                PlayerScript.AttachedCount--;

                PlayerScript.Detach = false;
                //BlockCollider.enabled = false;
                PlayerScript.ColliderIsDisabled = true;
                PlayerScript.ColliderTimer = PlayerScript.ColliderInterval;
                Debug.Log("Attached and Detached are now false, collider is disabled" + PlayerScript.AttachedCount);

                foreach (GameObject obj in PlayerScript.ActorParts)
                {
                    //obj.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    obj.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                   

                }
                foreach (HingeJoint2D obj in PlayerScript.Joints)
                {
                    //obj.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

                    //obj.anchor = PlayerScript.JointPositions[i];
                    obj.enabled = true;

                }
                for (int j = 0; j < 4; j++)
                {
                    PlayerScript.Particles[j].enableEmission = false;
                }
            }

        }
    }
}

/*Changes on 25th October, by Divij
 * Changed player launcher to include some timers and variables.
 * Force is applied after a delay to allow time to leave the collider, might be unnecessary
 * turned off colliders on metal blocks in inspector because i'm using my own added blocks for attaching right now
 * added sticky block script to be attached to blocks that you can attach to
 * changed colours of white menu buttons to black so its more visible.
 * 
 * Shockwave has a placeholder graphic. Force is applied in start function when its spawned with a right click
 * Object can be destroyed when the animation is over. Force still only occurs in the start function so animation length doesn't matter.
 * 
 * 
 * //set shockwave multiplier in inspector to increase or decrease strength of shockwave

 */

using UnityEngine;
using System.Collections;

public class playerLauncher : MonoBehaviour {

	public Vector3 headPosition;
	public Vector3 headToMouse;
	public Vector3 mouseWorldPoint;	
	public float launchPower;
	public LineRenderer line;
    public bool Detach = false;
    public bool Attached = false;
    public bool ApplyForce = false;
    public float ForceTimer = 0.0f;
    public GameObject Shockwave;
    public GameObject[] ActorParts;
    public HingeJoint2D[] Joints = new HingeJoint2D[] { null, null, null, null, null, null, null, null, null, null, null, null };
    public Vector3[] JointPositions;
    public int AttachedCount = 0;
    int i = 0;
    int j = 0;
    public float ColliderTimer = 0f;
    public bool ColliderIsDisabled = false;
    Collider2D BlockCollider;
    public ParticleSystem[] Particles;
    public float ColliderInterval = 0.5f;

    public int ShockwavesUsed = 0;
    public int ShockwavesAllowedNumber=1;


	// Use this for initialization
	void Start () {
        ShockwavesUsed = 0;
		line.enabled = false;
        i=0;
        j = 0;
        foreach (GameObject obj in ActorParts)
        {
            if(obj.name!="Main")
            {
                Joints[i] = obj.GetComponent<HingeJoint2D>();
                i++;
            }
            if(obj.name=="Arm3"||obj.name=="Leg3")
            {
                Particles[j] = obj.GetComponent<ParticleSystem>();
                j++;
            }
            

        }
	}
	
	// Update is called once per frame
	void Update () {
        if (ColliderIsDisabled)
        {
            ColliderTimer -= Time.deltaTime;
            ShockwavesUsed = 0;
        }
        if (ColliderIsDisabled && ColliderTimer <= 0.0f)
        {
            ColliderIsDisabled = false;
            for (int j = 0; j < 4; j++)
            {
                Particles[j].enableEmission = true;
            }
            Debug.Log("Collider is enabled again");
        }

		if(ApplyForce)
        {
            
            ForceTimer -= Time.deltaTime;
            
            
            
        }
        if(ForceTimer<=0f && ApplyForce)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-headToMouse.x * launchPower, -headToMouse.y * launchPower));
            ApplyForce = false;
           
        }
        if(Input.GetMouseButtonDown(1) && ShockwavesUsed<ShockwavesAllowedNumber)
        {
            Instantiate(Shockwave, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            ShockwavesUsed++;
        }
	}
    void OnMouseDown()
    {
        //Detach = true;

    }
	
	void OnMouseDrag () {		
		headPosition = transform.position;
		mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		headToMouse = mouseWorldPoint - headPosition;
		line.enabled = true;
		line.SetPosition(0, new Vector3(headPosition.x, headPosition.y, 1f));
		line.SetPosition(1, new Vector3(headPosition.x + -headToMouse.x, headPosition.y + -headToMouse.y, 1f));		
	}
	
	void OnMouseUp () {
        foreach (GameObject obj in ActorParts)
        {
            //obj.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            obj.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        }

        Debug.Log("Mouse up");
        Detach = true;
        ForceTimer = 0.25f;
        ApplyForce = true;
        line.enabled = false;

	}
}

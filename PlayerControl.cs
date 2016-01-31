using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	[HideInInspector]
	public bool climbing = false;



	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.

    public int PlayerStates= 0;
    //State 0 is normal, state 1 is climbing
    float h,v;
    public GameObject Arrow;
    public bool RotatedOnce = false;

	public int SeedAmmo = 6;
    public int SeedDirection = 0;
    public enum SeedDir { Left, Right, Up, Down, UpRight, UpLeft };
    public SeedDir SD = SeedDir.Right;

    public GameObject SeedPrefab;


	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();

	}
    void Start()
    {
        Arrow = GameObject.Find("ArrowHolder");
        Arrow.SetActive(false);
    }


	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}


	void FixedUpdate ()
	{
        //Arrow pointer

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (facingRight)
                    Arrow.transform.Rotate(0, 0, 45, Space.Self);
                else
                    Arrow.transform.Rotate(0, 0, 315, Space.Self);
                SD = SeedDir.UpLeft;

               


            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (facingRight)
                    Arrow.transform.Rotate(0, 0, 315, Space.Self);
                else
                    Arrow.transform.Rotate(0, 0, 45, Space.Self);
                SD = SeedDir.UpRight;


            }
            else
            {
                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);

                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
                SD = SeedDir.Up;

            }


            Arrow.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (facingRight)
                    Arrow.transform.Rotate(0, 0, 45, Space.Self);
                else
                    Arrow.transform.Rotate(0, 0, 315, Space.Self);
                SD = SeedDir.UpLeft;


            }
            else
            {
                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (facingRight)

                    Arrow.transform.Rotate(0, 0, 90, Space.Self);
                else
                    Arrow.transform.Rotate(0, 0, 270, Space.Self);
                SD = SeedDir.Left;


            }

            Arrow.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (facingRight)
                    Arrow.transform.Rotate(0, 0, 315, Space.Self);
                else
                    Arrow.transform.Rotate(0, 0, 45, Space.Self);
                SD = SeedDir.UpRight;


            }
            else
            {
                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (facingRight)
                    Arrow.transform.Rotate(0, 0, 270, Space.Self);
                else
                    Arrow.transform.Rotate(0, 0, 90, Space.Self);
                SD = SeedDir.Right;

            }
            Arrow.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);

            Arrow.transform.Rotate(0, 0, 180, Space.Self);
            SD = SeedDir.Down;

            Arrow.SetActive(true);

        }

        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
            if(facingRight)
            SD = SeedDir.Right;
            else
                SD = SeedDir.Left;


            Arrow.SetActive(false);


        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(SeedAmmo > 0)
			{
				GameObject Seed = Instantiate(SeedPrefab, transform.position, Quaternion.identity) as GameObject;
				// Seed.GetComponent<SeedScript>().SeedDirection(SD);
				SeedAmmo--;
			}




        }


		// Cache the horizontal input.
        if (PlayerStates == 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.0f;

            h = Input.GetAxis("Horizontal");


            // The Speed animator parameter is set to the absolute value of the horizontal input.
            anim.SetFloat("Speed", Mathf.Abs(h));

            // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
            if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
                // ... add a force to the player.
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

            // If the player's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (h > 0 && !facingRight)
                // ... flip the player.
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (h < 0 && facingRight)
                // ... flip the player.
                Flip();

            // If the player should jump...
            if (jump)
            {
                // Set the Jump animator trigger parameter.
                anim.SetTrigger("Jump");

                // Play a random jump audio clip.
                //int i = Random.Range(0, jumpClips.Length);
                //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

                // Add a vertical force to the player.
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

                // Make sure the player can't jump again until the jump conditions from Update are satisfied.
                jump = false;
            }
        }
        if (PlayerStates == 1)//Climbing a vine
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            v = Input.GetAxis("Vertical");
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, v * 4);
            h = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody2D>().velocity = new Vector2(h*2, GetComponent<Rigidbody2D>().velocity.y);

            /*
            if (v * GetComponent<Rigidbody2D>().velocity.y < maxSpeed)
                // ... add a force to the player.
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * v * moveForce);

            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sign(GetComponent<Rigidbody2D>().velocity.y) * maxSpeed);
             */

        }
	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
    void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.tag=="Vine")
        {
            Debug.Log("Entered Vine");
            PlayerStates = 1;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
    void OnTriggerExit2D(Collider2D Col)
    {
        if(Col.tag=="Vine")
        {
            Debug.Log("Exited Vine");

            PlayerStates = 0;
        }
    }
}

using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {
	private Jumping linkJumping;
	private Climbing linkClimbing;
	private Rigidbody2D body;
	public float maxSpeed = 10f;
	bool facingRight = true;
	public float move;
	public float max_a = 2;
	private float a = 1;
	private Rigidbody2D rb;
	public float knockBackForce = 3f;

	private bool knockedBack = false;
	public float knockBackDuration = 0.5f;
	private float knockBackTimer = 0f;

    public int maxHealth = 3;
    private int currentHealth;

	void Awake() {
		body = GetComponent<Rigidbody2D>();
	}
	void Start() {
		linkJumping = GetComponent<Jumping>();
		linkClimbing = GetComponent<Climbing>();
		rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

	void FixedUpdate () {
		if (!knockedBack)
		{
            move = Input.GetAxis("Horizontal");
			body.linearVelocity = new Vector2(move * maxSpeed * a, body.linearVelocity.y);
        }
		
	}

	void Update(){

		if (!knockedBack)
		{
            // Give player ability to jump

            if (linkJumping != null)
            {

                linkJumping.Jump();

                // Will be script later
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && linkJumping.grounded)
                { a = max_a; }
                else if (linkJumping.grounded)
                { a = 1; }

            }

            // Give player ability to climb

            if (linkClimbing != null)
            {
                linkClimbing.Climb();
            }

            // Moving

            body.linearVelocity = new Vector2(move * maxSpeed * a, body.linearVelocity.y);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }

            // Press ESC to quit [DOESNT WORK NOW]

            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Press R to restart level

            if (Input.GetKey(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (knockedBack)
        {
            knockBackTimer += Time.deltaTime;
            if (knockBackTimer >= knockBackDuration)
            {
                knockedBack = false;
                knockBackTimer = 0f;
            }
        }

    }
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
		{
            var attackedFromAbove = false;
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f && rb.linearVelocity.y < 0)
                {
                    attackedFromAbove = true;
                    break;
                }
            }

            if (attackedFromAbove)
            {
                var enemyScript = collision.gameObject.GetComponent<enemy_movement>();
                if (enemyScript != null)
                    enemyScript.TakeDamage(1);

                var jumpBounceForce = 7f;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpBounceForce);
            }

            else
            {
                TakeDamage(1);
                knockedBack = true;

                var knockBackDirection = (transform.position - collision.transform.position).normalized;
                var velocityMagnitude = rb.linearVelocity.magnitude;
                var forceMultiplier = velocityMagnitude < 1f ? 1.5f : 1f;

                rb.AddForce(knockBackDirection * knockBackForce * forceMultiplier, ForceMode2D.Impulse);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Failed!");
        Destroy(gameObject);

    }
}
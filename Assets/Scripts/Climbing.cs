using UnityEngine;

public class Climbing : MonoBehaviour
{
    private Rigidbody2D body;
    private Jumping linkJumping;
    [HideInInspector] public bool canClimb = false;
    public float climbForce = 700f;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        linkJumping = GetComponent<Jumping>();}
    
    void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Climb") && !linkJumping.grounded) {
            canClimb = true;
        }
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.CompareTag("Climb")) {
            canClimb = false;
        }
	}

    public void Climb() {
        if ((canClimb) && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
                body.AddForce(new Vector2(0f, climbForce));
        }
    }

}

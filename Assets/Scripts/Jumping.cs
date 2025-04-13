using UnityEngine;

public class Jumping : MonoBehaviour
{	
    private Rigidbody2D body;
    [HideInInspector] public bool grounded = false;
	public float jumpForce = 700f;    
    
    void Start() {body = GetComponent<Rigidbody2D>();}

    void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Ground") {grounded = true;}
	}
    void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "Ground") {grounded = false;}
	}
    // Update is called once per frame
    public void Jump()
    {
        if (grounded && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
			body.AddForce(new Vector2(0f, jumpForce));
			grounded = false;
		}
    }
}

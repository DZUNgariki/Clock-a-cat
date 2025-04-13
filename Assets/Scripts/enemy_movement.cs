using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    public GameObject enemy;
    public float speed;
    private float distance;

    public int maxHealth = 3;
    private int currentHealth;

    public LayerMask groundLayer;
    public float groundCheckDistance = 1;

    private Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() =>
        currentHealth = maxHealth;

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            var targetPosition = new Vector2(enemy.transform.position.x, rb.position.y);
            var distance = Vector2.Distance(rb.position, targetPosition);

            if (distance < 7)
            {
                var newPosition = Vector2.MoveTowards(rb.position, 
                    targetPosition, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPosition);
            }
        }
    }

    private bool IsGrounded()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down,
            groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    void Die() => Destroy(gameObject);
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    private Rigidbody2D rb;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager != null && gameManager.IsGameOver())
        {
            return;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(0.8f, 1.2f, 1f);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-0.8f, 1.2f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            if (gameManager != null)
            {
                gameManager.CollectCoin();
            }

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
        }

        if (collision.CompareTag("Goal"))
        {
            if (gameManager != null)
            {
                gameManager.TryWin();
            }
        }
    }
}
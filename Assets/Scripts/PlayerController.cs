using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
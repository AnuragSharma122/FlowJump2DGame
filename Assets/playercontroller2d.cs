using UnityEngine;

public class playercontroller2d : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    bool isGrounded;

    [SerializeField]
    private float runspeed = 10;

    [SerializeField]
    private float jumpspeed = 1.5f;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckL;

    [SerializeField]
    Transform groundCheckR;
    // Start is called before the first frame update

    float dirX, moveSpeed = 10;

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump") && rb2d.velocity.y == 0)
            rb2d.AddForce(Vector2.up * 400f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("coins"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag.Equals("movingplatform"))
            this.transform.parent = other.transform;
    }



    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    private void FixedUpdate()
    {

        if (rb2d.position.y < -13)
        {
            FindObjectOfType<GameManager>().endGame();
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(runspeed, rb2d.velocity.y);
            if (isGrounded)
                animator.Play("character_run");

            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-(runspeed), rb2d.velocity.y);
            if (isGrounded)
                animator.Play("character_run");

            spriteRenderer.flipX = true;
        }
        else
        {
            if (isGrounded)
            {
                animator.Play("Idle");
            }
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed);
            animator.Play("character_jump");
        }
        else if (Input.GetKey("space"))
        {
            animator.Play("character_jump");
        }

        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"))
            || Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("ground")) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        rb2d.velocity = new Vector2(dirX, rb2d.velocity.y);
    }



    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("movingplatform"))
            this.transform.parent = null;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MovementControllerSquare : MonoBehaviour
{
    [SerializeField]
    public float jump_scale = 5.0f;
    [SerializeField]
    public float movement_scale = 5.0f;
    //[SerializeField]
    //public float maximum_speed = 5.0f;

    public float shape_size;

    private Rigidbody2D rb;

    private float move_input;

    private bool is_grounded;

    public LayerMask ground_layer;

    public Sprite square_sprite;

    // changes made to a shape when it transitions
    public void Initialize(float size)
    {
        rb = this.GetComponent<Rigidbody2D>();
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<BoxCollider2D>().size = new Vector2(size, size);
        ground_layer = LayerMask.GetMask("Ground");
        shape_size = size;
        ChangeSprite("Sprites/Square");
    }

    public void ChangeSprite(string path)
    {
        Sprite newSprite = Resources.Load<Sprite>(path);
        if (newSprite != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("Sprite not found at path: " + path);
        }
    }

    public void Dismantle()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
    }


    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        Keyboard kb = Keyboard.current;
        is_grounded = IsGrounded();

        move_input = 0f;
        //if (is_grounded == true)
        //{
            if (kb.aKey.isPressed == true || kb.leftArrowKey.isPressed == true)
            {
                move_input = -1f;
            }
            else if (kb.dKey.isPressed == true || kb.rightArrowKey.isPressed == true)
            {
                move_input = 1f;
            }

            if (kb.wKey.isPressed == true || kb.spaceKey.isPressed == true || kb.upArrowKey.isPressed == true)
            {
                Jump();
            }
        //}
    }

    private void Jump()
    {
        
        if (is_grounded)
        {
            rb.linearVelocityY = jump_scale;
        }
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.localScale.x, transform.localScale.y), transform.rotation.z, ground_layer);
    }

    private void Move()
    {
        if (is_grounded)
        {
            rb.linearVelocityX = move_input * movement_scale;
        }
        
    }
}

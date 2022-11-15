using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rambo : MonoBehaviour
{
    private Rigidbody2D _rbplayer;
    private SpriteRenderer spr;
    private Animator _animator;
    [SerializeField] private float speed = 0f;
    [SerializeField] private Vector2 movement;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private bool Grounded = false;
    [SerializeField] private bool facingRight = false;

    public GameObject bulletPrefab;
    public Transform weapon;

    private void Awake()
    {
        _rbplayer = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontal, 0f);
        _animator.SetBool("Isidle", horizontal == 0.0f);

        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (Input.GetKeyDown(KeyCode.Space) && Grounded == true)
        {
            Jump();
        }

        if (Physics2D.Raycast(transform.position, Vector3.down, 1f))
        {
            Grounded = true;
        }

        else  
        {
            Grounded = false;
        }

        if (horizontal > 0.0f && facingRight == true )
        {
            Flip();
           
        }

        else if (horizontal < 0.0f && facingRight == false) 
        {
            Flip();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        

        //float vertical = Input.GetAxisRaw("Vertical");


    }

    private void FixedUpdate()
    {
        float horizontalVelocity = movement.x * speed;

        _rbplayer.velocity = new Vector2(horizontalVelocity, _rbplayer.velocity.y);
    }

    private void Jump ()
    {
        _rbplayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Flip() 
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Shoot()
    {
        Vector2 direction;
        if (transform.localScale.x == 1.0f)
        {
            direction = Vector2.right;
        }
        else direction = Vector2.left;
        // Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        GameObject bullet = Instantiate(bulletPrefab, weapon.position, Quaternion.identity);
        bullet.GetComponent<Ballas>().SetDirection(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = collision.transform;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }

    private void LateUpdate()
    {
        _animator.SetBool("Ground", Grounded);
    }
}

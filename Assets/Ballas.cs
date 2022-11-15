using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballas : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb_bullet;
    private Vector2 Direction;
    // Start is called before the first frame update

    private void Awake()
    {
        rb_bullet = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb_bullet.velocity = Direction * speed;
        Debug.Log(Direction);
    }

    public void SetDirection (Vector2 direction)
    {
        Direction = direction;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float movespeed = 1f;
    Rigidbody2D rb;
    public Animator animator;
    float height;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("MoveTrigger");
        height = transform.localScale.y;
    }

    void Update()
    {
        rb.velocity = new Vector2(-movespeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Detect")
        {
            FlipSprite();
        }
    }
    void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x) * height, height);
        movespeed = -movespeed;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;
    [SerializeField] float jumpspeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Rigidbody2D rb;
    Animator animator;
    Vector2 moveinput;
    CapsuleCollider2D capsule;
    float height;
    bool isAlive = true;
    SpriteRenderer sprite;
    Color change = Color.red;
    Color Orignal;
    Audio audio;
    void Start()
    {
        audio = FindObjectOfType<Audio>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        height=transform.localScale.y;
        capsule = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        Orignal = sprite.color;
    }
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        run();
        FlipSprite();
        Die();
    }
    //For PC Input
    void OnMove(InputValue value)
    {
        moveinput=value.Get<Vector2>();
    }
    //For Mobile Input
    public void OnRightButtonDown()
    {
        moveinput = new Vector2(1f, 0f); 
    }
    public void OnLeftButtonDown()
    {
        moveinput = new Vector2(-1f, 0f);
    }
    public void OnRightButtonUp()
    {
        moveinput = Vector2.zero; 
    }
    public void OnJumpButtonUp()
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isjumping", true);
            rb.velocity = new Vector2(0f, jumpspeed);
            StartCoroutine(Reset());
        }
    }
    //For New Input System
    void OnJump(InputValue value)
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if(value.isPressed)
            {
                animator.SetBool("isjumping", true);
                rb.velocity = new Vector2(0f, jumpspeed);
                StartCoroutine(Reset());
            }
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isjumping", false);
    }
    public void OnFireButtonUp()
    {
        animator.SetBool("isShooting", true);
        audio.ShootAudio();
        Instantiate(bullet, gun.position, transform.rotation);
        StartCoroutine(Stop());
    }
    void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetBool("isShooting", true);
            audio.ShootAudio();
            Instantiate(bullet, gun.position, transform.rotation);
            StartCoroutine(Stop());
        }
    }
    void Die()
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Enemy","hazards")))
        {
            animator.SetTrigger("isdying");
            isAlive = false;
            EnemyBullet.Health = 0;
            Invoke("LoadScene", 0.3f);
        }
    }

    public void Death()
    {
        animator.SetTrigger("isdying");
        isAlive = false;
        Invoke("LoadScene", 0.3f);
    }
    void LoadScene()
    {
        BulletMovement.score = 0;
        EnemyBullet.Health = 1;
        SceneManager.LoadScene(0);
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isShooting", false);
    }
    void run()
    {
        Vector2 temp = new Vector2(moveinput.x * movespeed, rb.velocity.y);
        rb.velocity = temp;

        bool isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isrunning", isMoving);
    }
    void FlipSprite()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x)*height, height);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet2")
        {
            StartCoroutine(ChangeColor());
        }
    }
    IEnumerator ChangeColor()
    {
        sprite.color = change;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Orignal;
    }
}

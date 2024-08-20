using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;
    Rigidbody2D rb;
    PlayerMovement player;
    float xspeed;
    static int count = 0;
    public static int score = 0;
    Audio audio;
    void Start()
    {
        audio = FindObjectOfType<Audio>();
        rb=GetComponent<Rigidbody2D>();
        player=FindObjectOfType<PlayerMovement>();
        xspeed = player.transform.localScale.x * movespeed;
    }
    void Update()
    {
        rb.velocity = new Vector2(xspeed, 0f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Enemy")
        {
            audio.DamageAudio();
            count++;
            score += 5;
            if (count == 5)
            {
                Destroy(collision.gameObject);
                count = 0;
            }
            Destroy(gameObject);
        }
    }
}

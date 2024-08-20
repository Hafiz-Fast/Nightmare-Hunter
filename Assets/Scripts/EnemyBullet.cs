using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float movespeed = -5f;
    [SerializeField] float maxdistance = 10f;
    Vector2 startpos;
    PlayerMovement player;
    Rigidbody2D rb;
    public static float Health = 1f;
    Audio audio;
    void Start()
    {
        audio = FindObjectOfType<Audio>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        startpos = transform.position;
    }
    void Update()
    {
        rb.velocity=new Vector2 (movespeed,0f);
        if(Vector2.Distance(startpos,transform.position) >=maxdistance)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audio.DamageAudio();
            Health -= 0.1f;
            if (Health<0)
            {
                player.Death();
                Health = 1f;
                BulletMovement.score = 0;
            }
            Destroy(gameObject);
        }
    }
}

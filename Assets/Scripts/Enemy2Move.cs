using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Enemy2Move : MonoBehaviour
{
    [SerializeField] Transform gun;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject player;
    SpriteRenderer sprite;
    Color change = Color.red;
    Color Orignal;

    void Start()
    {
        StartCoroutine(SpawnBullets());
        sprite = GetComponent<SpriteRenderer>();
        Orignal = sprite.color;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
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
    IEnumerator SpawnBullets()
    {
        while (player!=null)
        {
            Instantiate(bullet, gun.position, gun.rotation);
            yield return new WaitForSeconds(1f); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("LoadScene", 0.3f);
        }
    }
    void LoadScene()
    {
        BulletMovement.score = 0;
        EnemyBullet.Health = 1;
        SceneManager.LoadScene(0);
    }
}

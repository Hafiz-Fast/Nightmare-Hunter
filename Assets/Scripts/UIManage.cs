using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManage : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI ScoreText;
    void Start()
    {
        ScoreText.text = "Score " + BulletMovement.score.ToString("000000");
    }
    void Update()
    {
        ScoreText.text = "Score " + BulletMovement.score.ToString("000000");
        slider.value = EnemyBullet.Health;
    }
}

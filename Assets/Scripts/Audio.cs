using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootclip;
    [SerializeField][Range(0f, 1f)] float ShootVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageclip;
    [SerializeField][Range(0f, 1f)] float DamageVolume = 1f;

    public void ShootAudio()
    {
        if (shootclip != null)
        {
            AudioSource.PlayClipAtPoint(shootclip, Camera.main.transform.position, ShootVolume);
        }
    }
    public void DamageAudio()
    {
        if (damageclip != null)
        {
            AudioSource.PlayClipAtPoint(damageclip, Camera.main.transform.position, DamageVolume);
        }
    }
}

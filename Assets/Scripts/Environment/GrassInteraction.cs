using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTrigger : MonoBehaviour
{
    Animator anim;
    private AudioSource _audioSource;

    private bool canTrigger = false;

    IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        yield return new WaitForSeconds(1);
        canTrigger = true;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("NPC")) && canTrigger)
        {
            _audioSource?.Play();
            anim.SetTrigger("CollideGrass");
        }
    }
}

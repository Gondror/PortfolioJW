using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, IHittable
{
    [SerializeField] private Transform targetTransform;
    private GameManager gameManager;
    private AudioSource teleportAudio;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        teleportAudio = GetComponent<AudioSource>();
    }

    public void GetHit()
    {
        PlaySound();
        StartCoroutine(gameManager.TransitionTp(targetTransform));
    }

    private void PlaySound()
    {
        teleportAudio.Play();
    }
}

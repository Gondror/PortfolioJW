using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour, IHittable
{
    private Transform boardTransform;
    private Collider2D boardCrossColl;
    [SerializeField] Collider2D targetBindCollider;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float movingTime;
    [SerializeField] private Ease easeMove;

    private Vector3 firstPos;

    private AudioSource audioSource;

    [SerializeField] ProjectLinkManager projectLinkManager;

    [SerializeField] Collider2D[] ArrayCollider;

    private void Awake()
    {
        boardCrossColl = gameObject.GetComponent<Collider2D>();
        boardTransform = gameObject.transform;
        firstPos = boardTransform.position;

        boardCrossColl.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    public void PopUp()
    {
        audioSource.Play();
        boardTransform.DOMove(targetTransform.position, movingTime).SetEase(Ease.Linear).onComplete = ActiveCross;
    }

    public void GetHit()
    {

        audioSource.Play();
        boardCrossColl.enabled = false;

        if (projectLinkManager == null)
        {
            BackToStartPos();
        }
        else
        {
            StartCoroutine(CleanAndBackToStartPos());
        }
      
    }

    private void BackToStartPos()
    {
        boardTransform.DOMove(firstPos, movingTime).SetEase(Ease.Linear).onComplete = ResetTarget;
    }

    private IEnumerator CleanAndBackToStartPos()
    {
        SetCollider(false);
        yield return StartCoroutine(projectLinkManager.CleanSpace());



        boardTransform.DOMove(firstPos, movingTime).SetEase(Ease.Linear).onComplete = ResetTarget;

        yield return null;
    }

    private void ResetTarget()
    {
        SetCollider(false);
        audioSource.Stop();
        targetBindCollider.enabled = true;
    }

    private void ActiveCross()
    {
        SetCollider(true);
        audioSource.Stop();
        boardCrossColl.enabled = true;
    }

    private void SetCollider(bool activate)
    {
        for (int i = 0; i < ArrayCollider.Length; i++)
        {
            ArrayCollider[i].enabled = activate;
        }
    }
}

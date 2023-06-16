using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectLinkManager : MonoBehaviour
{
    private bool showUnityProject = false;
    private bool showUnrealProject = false;
    public bool isMoving = false;

    [SerializeField] private float movingTime;

    [SerializeField] GameObject[] ArrayProjectUnity;
    [SerializeField] GameObject[] ArrayProjectUnreal;

    [SerializeField] private Transform[] ArrayTargetTransform;
    [SerializeField] private Transform[] ArrayStartTransform;

    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void OpenPanelProject(bool isUnity)
    {
        if (isUnity)
        {
            if (showUnityProject)
            {
                StartCoroutine(HideUnityProject());
            }
            else
            {
                StartCoroutine(DisplayUnityProject());
            }
            
        }
        else
        {
            if (showUnrealProject)
            {
                StartCoroutine(HideUnrealProject());
            }
            else
            {
                StartCoroutine(DisplayUnrealProject());
            }
            
        }
    }

    private IEnumerator HideUnityProject()
    {
        animator.SetTrigger("UnityBack");
        audioSource.Play();

        for (int i = 0; i < ArrayProjectUnity.Length; i++)
        {
            DoingMovement(ArrayProjectUnity[i], ArrayStartTransform[i]);
            yield return new WaitForSeconds(0.25f);
        }

        showUnityProject = false;

        yield return new WaitForSeconds(0.25f);

        if (!isMoving)
        {
            audioSource.Stop();
        }
    }

    private IEnumerator HideUnrealProject()
    {
        animator.SetTrigger("UnrealBack");
        audioSource.Play();

        for (int i = 0; i < ArrayProjectUnreal.Length; i++)
        {
            DoingMovement(ArrayProjectUnreal[i], ArrayStartTransform[i]);
            yield return new WaitForSeconds(0.25f);
        }

        showUnrealProject = false;

        yield return null;

        yield return new WaitForSeconds(0.25f);

        if (!isMoving)
        {
            audioSource.Stop();
        }
        
    }

    private IEnumerator DisplayUnityProject()
    {
        if (isMoving)
        {
            StopCoroutine(DisplayUnityProject());
        }

        isMoving = true;
        animator.SetTrigger("UnityTurn");
        audioSource.Play();

        if (showUnrealProject)
        {
            yield return StartCoroutine(HideUnrealProject());
        }

        showUnityProject = true;

        for (int i = 0; i < ArrayProjectUnity.Length; i++)
        {
            DoingMovement(ArrayProjectUnity[i], ArrayTargetTransform[i]);
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.25f);
        isMoving = false;
        audioSource.Stop();
        yield return null;
    }

    private IEnumerator DisplayUnrealProject()
    {
        if (isMoving)
        {
            StopCoroutine(DisplayUnrealProject());
        }

        isMoving = true;
        animator.SetTrigger("UnrealTurn");
        audioSource.Play();

        if (showUnityProject)
        {
            yield return StartCoroutine(HideUnityProject());
        }

        showUnrealProject = true;

        for (int i = 0; i < ArrayProjectUnreal.Length; i++)
        {
            DoingMovement(ArrayProjectUnreal[i], ArrayTargetTransform[i]);
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.25f);
        isMoving = false;
        audioSource.Stop();
        yield return null;
    }

    public IEnumerator CleanSpace()
    {
        if (showUnityProject)
        {
            yield return StartCoroutine(HideUnityProject());
        }
        else if (showUnrealProject)
        {
            yield return StartCoroutine(HideUnrealProject());
        }

        yield return null;
    }

    public void OpenWebProject(string nameLink)
    {
        Application.OpenURL(nameLink);
    }

    private void DoingMovement(GameObject objectMove, Transform destination)
    {
        objectMove.transform.DOMove(destination.position, movingTime).SetEase(Ease.Linear);
    }
}

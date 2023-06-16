using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHittable
{
    [SerializeField] private BoardManager boardManager;
    private Animator targetAnim;
    private Collider2D targetCollider;
   

    private void Awake()
    {
        targetAnim = gameObject.GetComponent<Animator>();
        targetCollider = gameObject.GetComponent<Collider2D>();
    }

    public void GetHit()
    {
        targetCollider.enabled = false;
        targetAnim.SetTrigger("IsHit");
        boardManager.PopUp();
    }
}

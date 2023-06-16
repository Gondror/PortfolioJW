using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour, IHittable
{
    [SerializeField] UnityEvent eventsTrigger;
    public void GetHit()
    {
        eventsTrigger.Invoke();
    }
}

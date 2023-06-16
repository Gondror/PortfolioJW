using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadescreenManager : MonoBehaviour
{
    [SerializeField] private Image fadeScreenRend;
 
   public void Fading(float fadeValue, float fadeTime)
    {
        fadeScreenRend.DOFade(fadeValue, fadeTime);
    }

}

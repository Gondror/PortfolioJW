using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.InteropServices;

public class ContactManager : MonoBehaviour
{
    [SerializeField] private Text textEmail;
    [SerializeField] private Text textCopy;
    [SerializeField] private float timeFadingText = 2;

    public void CopyMail()
    {
        GUIUtility.systemCopyBuffer = textEmail.text;
        textCopy.DOFade(1, 0);
        textCopy.DOFade(0, timeFadingText);
    }

    public void OpenItchio()
    {
        Application.OpenURL("https://jules-gondror.itch.io/");
        
    }
}

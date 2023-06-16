using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;
    private FadescreenManager fadeScreenManager;
    [SerializeField] private float fadeTime;
    private bool alreadyClick = false;

    private void Awake()
    {
        fadeScreenManager = GetComponent<FadescreenManager>();
        audioSource = GetComponent<AudioSource>();
    }


    public void ButtonStart()
    {
        if (!alreadyClick)
        {
            StartCoroutine(StartGame());
            audioSource.Play();
            alreadyClick = true;
        }
        
    }

    private IEnumerator StartGame()
    {
        fadeScreenManager.Fading(1, fadeTime);

        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(1);
    }
}

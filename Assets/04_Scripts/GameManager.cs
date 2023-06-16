using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private FadescreenManager fadeScreenManager;
    [SerializeField] private float fadeTimeStart;
    [SerializeField] private float fadeTimeTransition;
    [SerializeField] Player_Controller playerContr;
    [SerializeField] private Transform player;

    private void Awake()
    {
        playerContr.enabled = false;
        fadeScreenManager = gameObject.GetComponent<FadescreenManager>();
        StartCoroutine(SceneStart());
    }

    private IEnumerator SceneStart()
    {
        fadeScreenManager.Fading(0, fadeTimeStart);

        yield return new WaitForSeconds(fadeTimeStart);

        playerContr.enabled = true;
    }

    public IEnumerator TransitionTp(Transform target)
    {
        playerContr.enabled = false;

        fadeScreenManager.Fading(1, fadeTimeTransition);

        yield return new WaitForSeconds(fadeTimeTransition);

        player.position = new Vector3(target.position.x, player.position.y, player.position.z);

        fadeScreenManager.Fading(0, fadeTimeTransition);

        playerContr.enabled = true;
    }
}

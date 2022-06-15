using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    [SerializeField]CanvasGroup fadePanel;

    public void BeginButton()
    {
        GetComponent<AudioSource>().Play();
        Invoke(nameof(SceneLoad), 0.5f);
        fadePanel.DOFade(1, 0.5f);
    }

    void SceneLoad()
    {
        SceneManager.LoadScene(1);

    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

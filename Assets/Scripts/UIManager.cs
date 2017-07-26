using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    [Header("Scenes")]
    public CanvasGroup splashScreen;
    public CanvasGroup mainMenu;
    public CanvasGroup gameScreen;
    public CanvasGroup gameOver;

    public GameObject gameObj;

    [Header("Buttons")]
    public Button playButton;
    public Button retryButton;
    public Button mainMenuButton;

    [Header("Fade Properties")]
    public float fadeTime;
    
    float timer;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void SliderControl(Slider slider)
    {
        if (slider.value <= 0.02f)
        {
            slider.fillRect.gameObject.SetActive(false);
        }
        else
        {
            if (!slider.fillRect.gameObject.activeInHierarchy)
            {
                slider.fillRect.gameObject.SetActive(true);
            }
        }
    }

    public void ActivateGameScreen(CanvasGroup currentScene)
    {
        if (playButton.interactable)
            playButton.interactable = false;

       

        if (currentScene.alpha > 0)
            StartCoroutine(FadeOut(currentScene));

        if (!gameObj.activeInHierarchy)
            gameObj.SetActive(true);
        if (gameScreen.alpha < 1)
            StartCoroutine(FadeIn(gameScreen));
    }

    public void ActivateGameOverScreen()
    {
        retryButton.interactable = true;
        mainMenuButton.interactable = true;

        if (gameOver.alpha < 1)
            StartCoroutine(FadeIn(gameOver));
    }

    public void Retry()
    {
        retryButton.interactable = false;
        mainMenuButton.interactable = false;

        if(gameOver.alpha > 0)
        {
            StartCoroutine(FadeOut(gameOver));
        }
    }

    public void ActivateMainMenu(CanvasGroup currentScene)
    {
        print("Play Button Click");

        if(currentScene == gameScreen)
        {
            gameObj.SetActive(false);
            gameOver.alpha = 0;
            retryButton.interactable = false;
            mainMenuButton.interactable = false;
        }

        if (currentScene.alpha > 0)
            StartCoroutine(FadeOut(currentScene));
        if (mainMenu.alpha < 1)
        {
            StartCoroutine(FadeIn(mainMenu));
            playButton.interactable = true;
        }
    }

    public IEnumerator FadeIn(CanvasGroup cnvs)
    {
        timer = 0;

        while (cnvs.alpha <= 0.95f)
        {
            float t = (timer / fadeTime);
            cnvs.alpha = Mathf.Lerp(0, 1, t);

            timer += Time.deltaTime;

            print("Fade IN");

            yield return new WaitForFixedUpdate();
        }

        cnvs.alpha = 1;
    }



    public IEnumerator FadeOut(CanvasGroup cnvs)
    {
        timer = 0;

        while (cnvs.alpha >= 0.05f)
        {
            float t = (timer / fadeTime);
            cnvs.alpha = Mathf.Lerp(1, 0, t);

            timer += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        cnvs.alpha = 0;
    }

}

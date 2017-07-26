using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScene : MonoBehaviour {

    public CanvasGroup cnvs;
    public CanvasGroup[] splashes;
    public float fadeTime;
    public float pauseTime;
    int index = 0;

    float timer;
	// Use this for initialization
	public void Init () {
        StartCoroutine(FadeIn(splashes[index]));
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


            yield return new WaitForSeconds(pauseTime);

            yield return StartCoroutine(FadeOut(splashes[index]));
        

        

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

        if(index + 1 < splashes.Length)
            yield return StartCoroutine(FadeIn(splashes[index++]));
        else
        {
            UIManager.instance.ActivateMainMenu(cnvs);
        }

    }
}

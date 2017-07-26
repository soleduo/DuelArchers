using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour {

    //public Animation anim_roundStart;
    public GameObject markerPrefab;
    public RectTransform markerContainer;

    public CanvasGroup roundFadeCanvas;

    public RoundMarker[] roundMarker1;
    public RoundMarker[] roundMarker2;

    public Animator ko_img;
    public Animator ready;

    [SerializeField] private Animator[] playerIndicatorAnim;

    int winCount1;
    int winCount2;

    public int roundCount = 3;
    int currentRound = 1;

    public float roundStartPause = 3f;
    public float roundEndPause = 3f;

    Character chara1;
    Character chara2;

    void Start()
    {
        chara1 = GameManager.Instance.chara1;
        chara2 = GameManager.Instance.chara2;

        
    }

    public void Init(CanvasGroup cnvs)
    {
        UIManager.instance.ActivateGameScreen(cnvs);

        StartCoroutine(StartRound(true));

    }

    public IEnumerator StartRound(bool isNewGame = false)
    {
        GameManager.Instance.gameState = GameplayState.RoundStart;

        chara1.anim.Play("idle");
        chara2.anim.Play("idle");

        if (isNewGame)
        { 
            ResetWinCount();
            ResetProperties();

            GameManager.turn = 1;

            roundMarker1 = new RoundMarker[roundCount/2 + 1];
            roundMarker2 = new RoundMarker[roundCount/2 + 1];

            for(int i = 0; i < roundMarker1.Length; i++)
            {
                RoundMarker marker = Instantiate(markerPrefab).GetComponent<RoundMarker>();
                roundMarker1[i] = marker;

                marker.Init(i, AnchorPreset.TopLeft, markerContainer);
            }

            for(int j = 0; j < roundMarker2.Length; j++)
            {
                RoundMarker marker = Instantiate(markerPrefab).GetComponent<RoundMarker>();
                roundMarker2[j] = marker;

                marker.Init(j, AnchorPreset.TopRight, markerContainer);
            }
        }

        ready.Play("play");

        yield return new WaitForSeconds(roundStartPause);

        if (GameManager.turn == 1)
            NextTurn(true);
        else
            NextTurn(false);

        GameManager.Instance.gameState = GameplayState.Play;
        /*
        while (anim_roundStart.isPlaying)
        {
            yield return null;
        }
        */
    }

    public IEnumerator EndRound(bool isGameEnd = false)
    {

        ko_img.Play("keok");

        GameManager.Instance.gameState = GameplayState.RoundEnd;

        if(isGameEnd)
        {
            UIManager.instance.ActivateGameOverScreen();

            yield break;
        }

        yield return StartCoroutine(RoundChange());
    }

    public void NextTurn(bool isPlayer1)
    {
        if (isPlayer1)
        {
            playerIndicatorAnim[0].SetBool("isPlaying", true);
            playerIndicatorAnim[1].SetBool("isPlaying", false);
        }
        else
        {
            playerIndicatorAnim[0].SetBool("isPlaying", false);
            playerIndicatorAnim[1].SetBool("isPlaying", true);
        }
    }

    public void ResetProperties()
    {
        chara1.SetHealth(100);
        chara2.SetHealth(100);

        
    }

    public void ResetWinCount()
    {
        currentRound = 1;

        winCount1 = 0;
        foreach(RoundMarker img in roundMarker1)
        {
            img.Deactivate();
        }

        winCount2 = 0;
        foreach(RoundMarker img in roundMarker2)
        {
            img.Deactivate();
        }
    }

    public void AddWinCount(int index)
    {
        if (index == 1)
        {
            roundMarker1[winCount1].Activate();
            winCount1++;

            CheckForGameEnd(winCount1, index);
        }
        else
        {
            roundMarker2[winCount2].Activate();
            winCount2++;

            CheckForGameEnd(winCount2, index);
        }
    }

    public void CheckForGameEnd(int count, int index)
    {
        print("Check win Count " + count);

        if (count >= roundCount)
        {
            StartCoroutine(EndRound(true));
        }
        else
        {
            StartCoroutine(EndRound(false));
        }
    }

    public void Retry()
    {
        UIManager.instance.Retry();

        StartCoroutine(RoundChange(true));
    }

    public IEnumerator RoundChange(bool newGame = false)
    {
        if (currentRound % 2 > 0)
            GameManager.turn = 1;
        else
            GameManager.turn = 2;


        currentRound++;

        yield return new WaitForSeconds(roundEndPause);

        while(roundFadeCanvas.alpha <= 0.95f)
        {
            roundFadeCanvas.alpha = Mathf.Lerp(roundFadeCanvas.alpha, 1, Time.deltaTime);

            yield return new WaitForFixedUpdate();
        }

        roundFadeCanvas.alpha = 1f;

        yield return new WaitForSeconds(1.5f);

        ResetProperties();

        while (roundFadeCanvas.alpha >= 0.05f)
        {
            roundFadeCanvas.alpha = Mathf.Lerp(roundFadeCanvas.alpha, 0, Time.deltaTime);

            
        }

        roundFadeCanvas.alpha = 0;

        

        yield return StartCoroutine(StartRound(newGame));
    }
}

public enum GameplayState
{
    RoundStart,
    Play,
    RoundEnd
}
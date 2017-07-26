using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour {

    public static GameOverScene instance;

    public Image img;

    public Sprite[] winnerImg;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetWinner(int index)
    {
        img.sprite = winnerImg[index];
    }
}

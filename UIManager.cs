using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite title;
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public int score;
    public GameObject titeScreen;

    public void updateLives(int currentlives)
    {

        livesImageDisplay.sprite = lives[currentlives];
    }

    public void updateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void showTitle()
    {
        titeScreen.SetActive(true);
    }

    public void hideTitle()
    {
        titeScreen.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
    }

}

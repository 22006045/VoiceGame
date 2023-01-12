using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeadMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = $"SCORE = {ScoreControl.finalScore}";
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Game");
    }    

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

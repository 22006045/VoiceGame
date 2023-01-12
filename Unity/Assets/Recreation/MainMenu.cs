using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject OptionsCanvas;
    [SerializeField] private GameObject MainCanvas;
    public static string difficulty = "easy";
    [SerializeField] private AudioSource selectAudio;

    public void StartGame()
    {
        selectAudio.Play();
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        selectAudio.Play();
        OptionsCanvas.SetActive(true);
        MainCanvas.SetActive(false);
    }

    public void BackToMain()
    {
        selectAudio.Play();
        OptionsCanvas.SetActive(false);
        MainCanvas.SetActive(true);        
    }

    public void QuitGame()
    {
        selectAudio.Play();
        Application.Quit();
    }

    public void ChooseDifficulty(string newDifficulty)
    {
        selectAudio.Play();
        difficulty = newDifficulty;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Animator tutorialAnimator;
    [SerializeField] private TrainControl trainControl;
    [SerializeField] private SpawnEnemies spawnEnemies;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private AudioSource selectOption;

    void Start()
    {
        tutorialAnimator = GetComponent<Animator>();
        trainControl.enabled = false;
        spawnEnemies.enabled = false;
        gameCanvas.SetActive(false);
    }

    public void CloseTutorial()
    {
        tutorialAnimator.SetTrigger("StartGame");
        selectOption.Play();
        StartCoroutine("StartGame");
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.25f);
        trainControl.enabled = true;
        spawnEnemies.enabled = true;
        gameCanvas.SetActive(true);  
        tutorialCanvas.SetActive(false);      
    }
}

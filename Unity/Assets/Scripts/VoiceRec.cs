using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Windows.Speech;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class VoiceRec : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private TrainControl trainControl;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private WordsLives wordsLives;
    [SerializeField] private TextMeshProUGUI text;
    private string newWord ;
    private bool loop = true;
    private void Start()
    {
        
        trainControl = FindObjectOfType<TrainControl>();
        GameObject player = GameObject.Find("Player");
        actions.Add("Sphere", Sphere);
        actions.Add("Cube", Cube);
        actions.Add("Cylinder", Cylinder);
        actions.Add("Red", Red);
        actions.Add("Yellow", Yellow);
        actions.Add("Blue", Blue);
        wordsLives = FindObjectOfType<WordsLives>();
        StartCoroutine(ChangeWord(10));

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }
    private void Update()
    {
        
    }
    private IEnumerator ChangeWord(float repeat)
    {
        while(loop)
        {
            int random = Random.Range(0, wordsLives.spaceWords.Length);
            newWord = wordsLives.spaceWords[random];
            text.text = newWord;
            if(newWord != null)
            {
                actions.Add(newWord,NewWordDetected);
                Debug.Log("Current known words : " + actions);
            } 
            yield return new WaitForSeconds(repeat);
        }
        
        
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void NewWordDetected()
    {
        Debug.Log("I know what word this is");
    }

    private void Sphere()
    {
        Debug.Log("TransformToSphere");
        trainControl.forms = TrainControl.Forms.sphere;
    }
    private void Cube()
    {
        trainControl.forms = TrainControl.Forms.cube;
    }
    private void Cylinder()
    {
        trainControl.forms = TrainControl.Forms.cylinder;
    }
    private void Red()
    {
        trainControl.trainPos = TrainControl.TrainPos.midle;
    }
    private void Yellow()
    {
        trainControl.trainPos = TrainControl.TrainPos.right;
    }
    private void Blue()
    {
        trainControl.trainPos = TrainControl.TrainPos.left;
    }
}

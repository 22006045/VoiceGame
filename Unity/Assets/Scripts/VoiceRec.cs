using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
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
    private string newWord = "coco";
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
        StartCoroutine(ChangeWord(2));

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
            wordsLives.GetNewWord(newWord);
            text.text = newWord;
            Debug.Log(newWord);
            actions.Add(newWord,NewWordDetected);
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

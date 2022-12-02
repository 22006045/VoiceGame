using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceRec : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private TrainControl trainControl;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Start()
    {
        trainControl = GetComponent<TrainControl>();
        GameObject player = GameObject.Find("Player");
        actions.Add("Sphere", Sphere);
        actions.Add("Square", Cube);
        actions.Add("Cilinder", Cylinder);
        actions.Add("Red", Red);
        actions.Add("Yellow", Yellow);
        actions.Add("Blue", Blue);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Sphere()
    {
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
        
    }
    private void Yellow()
    {
        
    }
    private void Blue()
    {
        
    }
}

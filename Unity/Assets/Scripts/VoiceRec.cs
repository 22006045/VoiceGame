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

    public int shieldCount = 0;
    [SerializeField] private GameObject shieldOn;
    [SerializeField] public GameObject shieldOff;
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
        actions.Add("DPD", DPD);
        wordsLives = FindObjectOfType<WordsLives>();
        StartCoroutine(ChangeWord(10));

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

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
                UpdateKeywordRecognizer();
                
            } 
            yield return new WaitForSeconds(repeat);
        }
        
        
    }

    private void UpdateKeywordRecognizer()
        {
            if(keywordRecognizer != null )
            {
                keywordRecognizer.OnPhraseRecognized -= RecognizedSpeech;
                keywordRecognizer.Stop();
                keywordRecognizer.Dispose();
                keywordRecognizer = null;
                keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
                keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
                keywordRecognizer.Start();
            }
        }
    public IEnumerator ActivateUI(GameObject UI, float activeDuration)
    {
        UI.SetActive(true);
        yield return new WaitForSeconds(activeDuration);
        UI.SetActive(false);

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        UpdateKeywordRecognizer();
        actions[speech.text].Invoke();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet" && shieldCount == 1) StartCoroutine(ActivateUI(shieldOff, 2f));
        
    }
    private void NewWordDetected()
    {
        if(shieldCount == 0)
        {
            shieldCount = 1;
            StartCoroutine(ActivateUI(shieldOn, 2f));
        } 
        
        Debug.Log("I know what word this is");
    }

    private void DPD()
    {
        trainControl.DPDSecret();
    }

    private void Sphere()
    {
        Debug.Log("TransformToSphere");
        trainControl.GoToSphere();
    }
    private void Cube()
    {
        trainControl.GoTocube();
    }
    private void Cylinder()
    {
        trainControl.GoToCylinder();
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

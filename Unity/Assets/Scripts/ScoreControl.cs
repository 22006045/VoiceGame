using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreControl : MonoBehaviour
{
    public int Score {get; set;}
    [SerializeField] private TextMeshProUGUI text;
    public static int finalScore;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        finalScore = Score;
    }

    private void UpdateUI()
    {
        string s = "0000";

        if(Score < 10) s = "000" + Score;
        else if(Score < 100) s = "00" + Score;
        else if(Score < 1000) s = "0" + Score;
        else s = Score.ToString();

        text.text = s;
    }
}

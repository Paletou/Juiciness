using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static int score;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        text.text = score.ToString();
    }

    public void addScore()
    {
        Debug.Log("2");
        score++;
        Debug.Log(score);
    }
}

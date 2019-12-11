using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Text scoreText;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        printScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void printScore()
    {
        scoreText.text = score.ToString();
    }
    public void addScore()
    {
        score++;
        printScore();
    }
}

using UnityEngine;
using System;
using TMPro;

public class Points : MonoBehaviour
{
    private static int score = 0;
    public TMP_Text scoreText;
    private static bool timeIsOver = false;

    public static void increaseScore(){
        if(!timeIsOver){
            score = score + 1;
            Console.WriteLine(score);
        }
    }

    public static void decreaseScore(){
        if(!timeIsOver){
            score = score - 1;
            Console.WriteLine(score);
        }
    }

    public static int getScore(){
        return score;
    }

    public static void setTimeIsOver(bool over){
        timeIsOver = over;
    }

    void Update(){
        scoreText.text = "Score: " + score;
    }
}

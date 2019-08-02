using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGainControl : MonoBehaviour {

    public float playerScore = 0f;
    public float scoreIncrement_normal = 100f;

    private void Start()
    {
        playerScore = 0;
    }

    public void increaseScore()
    {
        playerScore += scoreIncrement_normal;
    }

    public void addXscore(float scoreToAdd)
    {
        playerScore += scoreToAdd;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public GameObject directorObject;
    public float playerScore;
    public float timer = 0;
    public float CountInterval = 0.001f;
    public float incrementAmount = 10;
    public float i = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PointGainControl pointGainScript = directorObject.gameObject.GetComponent<PointGainControl>();
        playerScore = pointGainScript.playerScore;

        timer += Time.deltaTime;

        if (i < playerScore * .9 && i > 1000)
        {
            incrementAmount = 100;
        }
        else
        {
            incrementAmount = 10;
        }

        if (i < playerScore && timer >= CountInterval)
        {
            i += incrementAmount;
            scoreText.text = i.ToString();
            timer = 0;

            //play tick sound
            //GameObject.Find("Director").GetComponent<PointGainSoundControl>().playPointSound();
        }
    }
}

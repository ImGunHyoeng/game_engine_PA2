using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clear_time : MonoBehaviour
{
    private int highScore;
    private int result;
    public Text resultTime;
    public Text bestTime;
    void Start()
    {
        result = set_result.st_result;
        // PlayerPrefs는 데이터 저장 클래스
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore =
            PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScore = 0;
        }
    }
    void Update()
    {
        
        if (highScore < result)
        {
            PlayerPrefs.SetInt("HighScore", result);
            highScore = result;
        }
        
        resultTime.text = "CurTime: " + result;
        bestTime.text = "BestTime: " + highScore;
            
        
    }
}


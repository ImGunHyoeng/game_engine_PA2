using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class time_ctr : MonoBehaviour
{
    float time;
    public Text time_t;
    // Start is called before the first frame update
    void Start()
    {
        //=GameObject.Find("Time").GetComponent<Text>();
        time = 200f;
        StartCoroutine(time_set());
    }

    // Update is called once per frame
    void Update()
    {
        
        time-=Time.deltaTime;
    }
    public int get_time(){return (int)time;}
    public void minus_time(int minus)
    {
        time-= minus;
    }
    IEnumerator time_set()
    {
        yield return new WaitForSeconds(0.5f);
        if (time <= 0)
        {
            SceneManager.LoadScene("Gameover");
           //yield return new WaitForSeconds(0.3f);
        }
        time_t.text = "Time : " + (int)time;
        
        StartCoroutine(time_set());
    }
}

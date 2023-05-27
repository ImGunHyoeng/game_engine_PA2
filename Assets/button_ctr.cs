using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_ctr : MonoBehaviour
{
    public GameObject control;
    public GameObject clear;
    // Start is called before the first frame update
    public void return_scene()
    {
        SceneManager.LoadScene("Title");
    }
    public void end_game()
    {
        Application.Quit();
    }
    public void control_view()
    {
        
        control.SetActive(true);
    }
    public void clear_view()
    {

        clear.SetActive(true);
    }
    public void start_game()
    {
        SceneManager.LoadScene("Main");
    }
}

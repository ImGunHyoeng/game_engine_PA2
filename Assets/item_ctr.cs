using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_ctr : MonoBehaviour
{
    select_ctr ctr;
    Light area_light;
    Slider light_slider;
    public GameObject bomb;
    private void Start()
    {
        light_slider=GameObject.Find("light_slider").GetComponent<Slider>();
        area_light=GameObject.Find("light").GetComponent<Light>();
        ctr = GameObject.Find("Select_ctr").GetComponent<select_ctr>();
        StartCoroutine(light_down());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) 
        {
            if (ctr.get_count_item() > 0)
            {
                ctr.minus_item();
                useitem();
            }
        }
    }
    IEnumerator light_down()
    {
        yield return new WaitForSeconds(0.4f);
        if (area_light.range < 3)
        {  }
        else
        {
            area_light.range -=Time.deltaTime*3;
            light_slider.value = (area_light.range-3)/5;//.area_light.range;
        }
        StartCoroutine(light_down());
    }


    void useitem()
    {
        if (ctr.get_item_name() == "Burn")
        {
            area_light.range = 8;
        }
        else
        {

        }
    }
}

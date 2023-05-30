using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_ctr : MonoBehaviour
{
    AudioSource audio;
    public AudioClip burn_ac;
    public AudioClip bomb_ac;
    public AudioClip bomb_effect;
    //public AudioClip burn_effect;
    public Pick_up up;
    select_ctr ctr;
    Light area_light;
    Slider light_slider;
    public GameObject bomb;
    Transform player;
    private void Start()
    {
        audio = this.gameObject.AddComponent<AudioSource>();
        player=GameObject.Find("Player").GetComponent<Transform>();
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
        yield return new WaitForSeconds(0.7f);
        if (area_light.range < 3)
        {  
            light_slider.value = 0;
        }
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
            audio.clip = burn_ac;
            area_light.range = 8;
        }
        else
        {
            audio.clip = bomb_ac;
            StartCoroutine(spawnbomb());
        }
        audio.Play();
    }
    IEnumerator spawnbomb()
    {
        float x=Random.Range(-1f,1f);
        float z=Random.Range(-1f,1f);
        GameObject bom_ins = Instantiate(bomb, new Vector3(player.position.x+x,player.position.y+1f,player.position.z+z),Quaternion.Euler(Vector3.zero));
        yield return new WaitForSeconds(4.99f);
        //Debug.Log(bom_ins.GetComponentInChildren<Collider>().enabled);
        //bom_ins.GetComponentInChildren<Collider>().enabled = true;
        //Debug.Log(bom_ins.transform.GetChild(0).gameObject);
        bom_ins.transform.GetChild(0).gameObject.SetActive(true);
        up.set_grab_bool();
        audio.clip = bomb_effect;
        audio.Play();
        yield return new WaitForSeconds(0.08f);
        Destroy(bom_ins);
    }
}

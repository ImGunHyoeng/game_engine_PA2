using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class select_ctr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject select;
    Image children_image;
    Text children_text;
    Sprite tmp;
    string item;
    int bombcount;
    int burncount;
    void Start()
    {
        children_image = select.GetComponentInChildren<Image>();//이미지로만 받아온다면 원급을 받아오지만 해당하는것을 함수로써 받아오면 복사되어 넘어오는것
        children_text = select.GetComponentInChildren<Text>();
        item = "Burn";
        bombcount = 0;
        burncount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        change_item();
    }
    public void updatetext()
    {
        if (item == "Burn")
        {
            children_text.text = burncount.ToString();
        }
        else
        {
            children_text.text = bombcount.ToString();
        }
    }
    void change_item()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (item == "Burn")
            {
                item = "Bomb";
                children_text.text = bombcount.ToString();
            }
            else
            {
                item = "Burn";
                children_text.text = burncount.ToString();
            }
            //= Resources.Load<Sprite>(item);
            tmp = Resources.Load<Sprite>(item);
            children_image.sprite = tmp;
        }
    }
    public void minus_item()
    {
        if(item=="Burn"){ minus_burncount(); }
        else { minus_bombcount(); }
        updatetext();
    }
    public int get_count_item()
    {
        if (item == "Burn") { return get_burncount(); }
        else { return get_bombcount(); } 
    }
    public string get_item_name() { return item; }
    
    public void pick_bomb(){bombcount = 3;}
    public void pick_burn() { burncount = 5;}
    int get_bombcount() { return bombcount; }
    int get_burncount() {  return burncount; }
    void minus_bombcount() { bombcount--; }
    void minus_burncount() { burncount--; }
}

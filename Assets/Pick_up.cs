using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_up : MonoBehaviour
{
    public Animator animator;
    select_ctr select_Ctr;
    private void Awake()
    {
        select_Ctr=GameObject.Find("Select_ctr").GetComponent<select_ctr>();
        // if (!animator) { gameObject.GetComponent<Animator>(); }
    }
    void isPick(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("yes");
            animator.SetTrigger("Pickup");
            if(LayerMask.LayerToName(other.gameObject.layer)=="Burn")//레이어의 이름을 알아오는것
            {
                select_Ctr.pick_burn();
                select_Ctr.updatetext();
            }
            else 
            {
                select_Ctr.pick_bomb();
                select_Ctr.updatetext();
            }
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            isPick(other);
        }
    }
}

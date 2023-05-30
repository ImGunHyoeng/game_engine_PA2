using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_up : MonoBehaviour
{
    AudioSource audio;
    public AudioClip pick_ac;
    public Animator animator;
    select_ctr select_Ctr;
    GameObject others_gm;
    bool isitem=false;
    bool isgrab = false;
    Transform player;
    Collision bomb_ins;
    private void Awake()
    {
        audio = this.gameObject.AddComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        audio.clip = pick_ac;
        select_Ctr = GameObject.Find("Select_ctr").GetComponent<select_ctr>();
        // if (!animator) { gameObject.GetComponent<Animator>(); }
    }
    private void Update()
    {
        
        Debug.Log(isitem);
        if(isitem)
        {
            StartCoroutine(isPick(others_gm));
        }
/*        if(isgrab)
        {
            if (bomb_ins != null)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    isgrab = true;
                    //bomb_ins.rigidbody.isKinematic = false;
                    bomb_ins.transform.SetParent(null);
                    bomb_ins.rigidbody.AddForce(30 * player.rotation.y, 0, 0);
                }
            }
        }*/
    }
    IEnumerator isPick(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("yes");
            animator.SetTrigger("Pickup");
            audio.Play();
            if(other.layer==6)
            //if(LayerMask.LayerToName(other.gameObject.layer)=="Burn")//레이어의 이름을 알아오는것
            {
                select_Ctr.pick_burn();
                select_Ctr.updatetext();
            }
            else if(other.layer==7)
            {
                select_Ctr.pick_bomb();
                select_Ctr.updatetext();
            }
            Destroy(other);
            isitem = false;
        }
        yield return new WaitForSeconds(0.2f);
    }
    public void set_grab_bool() { isgrab = false; }
    IEnumerator grabbomb(Collision bomb)
    {
        if (bomb == null) ; //{ isgrab = false; } 
        else
        {
            isgrab = true;
            bomb_ins = bomb;
            //Debug.Log(box.position);
            bomb.gameObject.transform.SetParent(player);
            bomb.gameObject.transform.position = player.position + new Vector3(0, 1.5f, 0);
            //bomb.gameObject.transform.position= box.position;

            //bomb.gameObject.transform.position = 
            bomb.rigidbody.isKinematic = true;

        }
        yield return null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bomb"&&isgrab==false)
        {
            StartCoroutine(grabbomb(collision));
        }
    }

  
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            isitem = true;
            others_gm = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            isitem = true;
            others_gm = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            isitem =false;
            //Destroy(others_gm);
        }
    }
}

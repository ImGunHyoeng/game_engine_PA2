using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_ctr_view : MonoBehaviour
{
    public GameObject save_t;
    private void Start()
    {
        //save_t = GameObject.Find("Save");
    }
    // Start is called before the first frame update
    public void save_text()
    {
        StartCoroutine(save());
    }
    IEnumerator save()
    {
        save_t.SetActive(true);
        yield return new WaitForSeconds(3f);
        save_t.SetActive(false);
    }
}

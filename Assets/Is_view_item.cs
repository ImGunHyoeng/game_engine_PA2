using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Is_view_item : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject isview_gm;
    GameObject view;
    private void Start()
    {
        isview_gm = Resources.Load<GameObject>("Prefebs/Icon");
    }
    private void OnTriggerEnter(Collider other)
    {
        view = Instantiate(isview_gm,this.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(view);
    }
}

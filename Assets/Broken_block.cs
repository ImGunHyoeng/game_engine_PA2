using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken_block : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Explosion")
        {
            Destroy(this.gameObject);
        }
    }
}

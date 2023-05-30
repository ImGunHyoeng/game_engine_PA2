using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible_bridge : MonoBehaviour
{
    public GameObject bridge;
    AudioSource audio;
    public AudioClip sc;
    // Start is called before the first frame update
    private void Start()
    {
        audio = this.gameObject.AddComponent<AudioSource>();
        audio.clip = sc;
        audio.volume = 0.3f;
    }
    private void OnTriggerEnter(Collider other)
    {
        audio.Play();
        bridge.SetActive(true);
        Destroy(gameObject,5f);
    }
}

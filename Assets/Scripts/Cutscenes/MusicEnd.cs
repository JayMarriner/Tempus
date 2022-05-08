using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEnd : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource sourceOld;
    [SerializeField] AudioClip newClip;
    [SerializeField] GameObject robot;
    bool stopper;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(robot == null && !stopper)
        {
            stopper = true;
            source.Play(0);
            sourceOld.enabled = false;
        }
    }
}

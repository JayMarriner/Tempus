using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutBamboo : MonoBehaviour
{
    [SerializeField] GameObject cut;
    [SerializeField] GameObject notCut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cut()
    {
        notCut.SetActive(false);
        cut.SetActive(true);
    }
}

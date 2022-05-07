using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoots : MonoBehaviour
{
    ThirdPersonPlayer tpp;
    // Start is called before the first frame update
    void Start()
    {
        tpp = GetComponent<ThirdPersonPlayer>();
    }

    public void ShoesOn()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<GameManager>().jumpBoots = true;
        tpp.PutOnShoes();
    }
}

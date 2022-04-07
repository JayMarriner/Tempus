using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCText : MonoBehaviour
{
    [SerializeField] string npcName;
    [SerializeField] string[] sentences;
    //[SerializeField] int[] sentenceTime;
    [SerializeField] bool[] isAnimChange;
    [SerializeField] string[] animName;
    [SerializeField] AudioClip[] audio;
    [SerializeField] Animator anim;
    [SerializeField] TalkerHandler talkerHandler;

    AudioSource source;

    public void StartRead()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(ReadLines());
    }

    IEnumerator ReadLines()
    {
        print(sentences.Length);
        for(int x = 0; x < sentences.Length; x++)
        {
            GetComponent<TMP_Text>().text = npcName + ": " + sentences[x];
            if (isAnimChange[x])
            {
                anim.SetBool(animName[x], true);
            }
            source.clip = audio[x];
            source.Play();
            yield return new WaitForSeconds(audio[x].length);
            if (isAnimChange[x])
            {
                anim.SetBool(animName[x], false);
            }
        }
        yield return new WaitForSeconds(1f);

        talkerHandler.EndConversation();
    }
}

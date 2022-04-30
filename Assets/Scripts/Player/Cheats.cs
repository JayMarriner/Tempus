using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    ThirdPersonPlayer playerScript;
    [SerializeField] GameObject bomberEnemy;
    [SerializeField] GameObject playerHead;
    [SerializeField] Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }

    public void ChangeLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void GiveJetpack()
    {
        playerScript.jetScript.HasJetPack = true;
        playerScript.jetScript.isEquipped = true;
    }

    public void BigBang()
    {
        StartCoroutine(SpawnBangers());
    }

    public void FullHealth()
    {
        playerScript.currHealth = playerScript.maxHealth;
    }

    public void Invincible()
    {
        playerScript.invincible = true;
    }

    public void BigHead()
    {
        playerHead.transform.localScale = new Vector3(5, 5, 5);
        anim.SetLayerWeight(1, 1);
        anim.SetBool("BigHead", true);
        print("Yes");
    }

    IEnumerator SpawnBangers()
    {
        int amt = 0;
        while(amt < 1000)
        {
            Instantiate(bomberEnemy, playerScript.gameObject.transform);
            yield return new WaitForSeconds(0.1f);
        }
    }


}

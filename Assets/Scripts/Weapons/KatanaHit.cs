using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KatanaHit : MonoBehaviour
{
    [SerializeField] Image uiKanji;
    [SerializeField] Image uiFill;
    [SerializeField] TMP_Text uiText;
    KatanaHandler kH;
    float powerAmt;
    // Start is called before the first frame update
    void Start()
    {
        kH = GetComponentInParent<KatanaHandler>();
        uiKanji.rectTransform.sizeDelta = new Vector2(100, 100);
        uiFill.fillAmount = 0;
        uiText.text = "POWER";
    }

    // Update is called once per frame
    void Update()
    {
        if (uiFill.fillAmount >= 1f && Input.GetKeyDown(KeyCode.F) && GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>().IsGrounded() && !kH.powerUpActive)
        {
            kH.SpecialAttack();
            StartCoroutine(SpecialDrain());
        }
    }

    IEnumerator SpecialDrain()
    {
        uiText.text = "2x DMG";
        float counter = 0;
        while (counter < 4.2)
        {
            uiFill.fillAmount -= 0.0025f;
            yield return new WaitForSeconds(0.01f);
            counter += 0.01f;
        }
        powerAmt = 0;
        uiFill.fillAmount = 0;
        uiText.text = "POWER";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(kH.swinging || kH.slashing)
        {
            if (other.tag == "Enemy")
            {
                if(kH.powerUpActive)
                    other.GetComponent<RobotInfo>().LowerHealth(100);
                else
                    other.GetComponent<RobotInfo>().LowerHealth(50);
                uiKanji.rectTransform.sizeDelta = Vector2.Lerp(new Vector2(100, 100), new Vector2(300, 150), Time.deltaTime * 2);
                if(!kH.powerUpActive)
                    uiFill.fillAmount += 0.2f;
                if (uiFill.fillAmount >= 1f)
                    uiText.text = "PRESS 'F'";
            }

            if (other.tag == "Spawner")
            {
                other.GetComponent<RobotInfo>().LowerHealth(100);
                uiKanji.rectTransform.sizeDelta = Vector2.Lerp(new Vector2(100, 100), new Vector2(150, 150), Time.deltaTime * 10);
                if (!kH.powerUpActive)
                    uiFill.fillAmount += 0.2f;
                if (uiFill.fillAmount >= 1f)
                    uiText.text = "PRESS 'F'";
            }

            if (other.tag == "Boss")
            {
                if (kH.powerUpActive)
                    other.GetComponent<RobotInfo>().LowerHealth(20);
                else
                    other.GetComponent<RobotInfo>().LowerHealth(10);
                uiKanji.rectTransform.sizeDelta = Vector2.Lerp(new Vector2(100, 100), new Vector2(150, 150), Time.deltaTime * 10);
                if (!kH.powerUpActive)
                    uiFill.fillAmount += 0.2f;
                if (uiFill.fillAmount >= 1f)
                    uiText.text = "PRESS 'F'";
            }

            if(other.tag == "Bamboo")
            {
                other.GetComponent<CutBamboo>().Cut();
                if (!kH.powerUpActive)
                    uiFill.fillAmount += 0.01f;
                if (uiFill.fillAmount >= 1f)
                    uiText.text = "PRESS 'F'";
            }
        }
    }
}

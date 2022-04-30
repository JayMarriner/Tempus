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
        if (uiFill.fillAmount >= 1f && Input.GetKeyDown(KeyCode.F) && GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>().IsGrounded())
        {
            kH.SpecialAttack();
            uiFill.fillAmount = 0;
            uiText.text = "POWER";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(kH.swinging || kH.slashing)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<RobotInfo>().LowerHealth(50);
                uiKanji.rectTransform.sizeDelta = Vector2.Lerp(new Vector2(100, 100), new Vector2(300, 150), Time.deltaTime * 2);
                uiFill.fillAmount += 0.2f;
                if (uiFill.fillAmount >= 1f)
                    uiText.text = "PRESS 'F'";
            }

            if (other.tag == "Spawner")
            {
                other.GetComponent<SpawnerHealth>().LowerHealth(100);
                uiKanji.rectTransform.sizeDelta = Vector2.Lerp(new Vector2(100, 100), new Vector2(150, 150), Time.deltaTime * 10);
                uiFill.fillAmount += 0.25f;
                if (uiFill.fillAmount >= 0.1f)
                    uiText.text = "PRESS 'F'";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    [SerializeField] GameObject canvas;
    public ThirdPersonPlayer player;
    bool fillChange;
    public float currFillAmt;

    private void Start()
    {
        canvas.SetActive(false);
        currFillAmt = 10;
    }

    private void Update()
    {
        if (currFillAmt > player.currHealth && !fillChange)
                StartCoroutine(UpdateHealth());
    }

    IEnumerator UpdateHealth()
    {
        canvas.SetActive(true);
        fillChange = true;
        while(healthBarImage.fillAmount > player.currHealth/10f)
        {
            healthBarImage.fillAmount -= 0.005f;
            yield return new WaitForSeconds(0.001f);
        }
        fillChange = false;
        currFillAmt = player.currHealth;
        yield return new WaitForSeconds(1f);
        canvas.SetActive(false);
    }
   
}

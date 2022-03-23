using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;

    public ThirdPersonPlayer player;

    private void Update()
    {
        /*
        if (isMoving && player.controller.velocity.magnitude < 0.1f)
        {
            if (tween != null)
                tween.Kill();

            isMoving = false;
            tween = parentCanvasGroup.DOFade(1f, 0.75f);
        }
        else if (!isMoving && player.controller.velocity.magnitude > 0.15f)
        { 
            if (tween!=null)
                tween.Kill();

            isMoving = true;
            tween = parentCanvasGroup.DOFade(1f, 0.75f);
        }
        */
        
    }

    public void UpdatePlayerHealth()
    {
        float duration = 0.75f * (player.currHealth / player.maxHealth);
        healthBarImage.DOFillAmount(player.currHealth / player.maxHealth, duration);

        Color newColor = Color.green;
        if (player.currHealth < player.maxHealth * 0.25f)
        {
            newColor = Color.red;
        }
        else if (player.currHealth < player.maxHealth * 0.66f)
        {
            newColor = new Color(1f, .64f, 0f, 1f);
        }

        healthBarImage.DOColor(newColor, duration);
    }
   
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    /* Health Bar*/
    public TMPro.TextMeshProUGUI healthText;
    public Image healthBarFill;

    // Cache width of health bar visual
    //private float cachedWidth;


    /* Cooldowns */
    public Image dashImg;
    public Image parryImg;

    public Player player;

    void Start()
    {
        //cachedWidth = healthRect.rect.width;
    }

    public void UpdateHealthBar()
    {
        /*
        Vector2 anchorMin = healthRect.anchorMin;
        Vector2 anchorMax = healthRect.anchorMax;

        // Calculate the new anchors
        float newAnchorMaxX = anchorMin.x + Player.baseHealth / Player.maxHealth * cachedWidth / healthRect.parent.GetComponent<RectTransform>().rect.width;
        anchorMax.x = newAnchorMaxX;

        healthRect.anchorMax = anchorMax;
        */

        healthText.SetText("Health: " + Player.curHealth + "/" + Player.maxHealth);
        healthBarFill.fillAmount = (float)(Player.curHealth / Player.maxHealth);
    }

    public void UpdateCooldowns()
    {
        if(player.isDashOnCooldown)
        {
            dashImg.CrossFadeAlpha(0, 0.1f, false);
        }
        else
        {
            dashImg.CrossFadeAlpha(1, 0.1f, false);
        }

        if(player.isParryOnCooldown)
        {
            parryImg.CrossFadeAlpha(0, 0.1f, false);
        }
        else
        {
        parryImg.CrossFadeAlpha(1, 0.1f, false);
        }
    }

}

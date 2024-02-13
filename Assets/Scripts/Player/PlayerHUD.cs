using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHUD : MonoBehaviour
{

    /* Health Bar*/
    public TMPro.TextMeshProUGUI healthText;
    public RectTransform healthRect;

    // Cache width of health bar visual
    private float cachedWidth;


    /* Cooldowns */
    public UnityEngine.UI.Image dashImg;
    public UnityEngine.UI.Image parryImg;

    void Start()
    {
        cachedWidth = healthRect.rect.width;
    }

    void Update()
    {
        if(healthText != null)
        {
            healthText.SetText("Health: " + Player.baseHealth + "/" + Player.maxHealth);
        }
        if(healthRect != null)
        {
            UpdateHealthBar();
        }

        if(dashImg && parryImg){
            UpdateCooldowns();
        }
    }

    void UpdateHealthBar()
    {
        Vector2 anchorMin = healthRect.anchorMin;
        Vector2 anchorMax = healthRect.anchorMax;

        // Calculate the new anchors
        float newAnchorMaxX = anchorMin.x + Player.baseHealth / Player.maxHealth * cachedWidth / healthRect.parent.GetComponent<RectTransform>().rect.width;
        anchorMax.x = newAnchorMaxX;

        healthRect.anchorMax = anchorMax;
    }

    void UpdateCooldowns()
    {
        if(Player.isDashOnCooldown)
        {
            dashImg.CrossFadeAlpha(0, 0.1f, false);
        }
        else
        {
            dashImg.CrossFadeAlpha(1, 0.1f, false);
        }

        if(Player.isParryOnCooldown)
        {
            parryImg.CrossFadeAlpha(0, 0.1f, false);
        }
        else
        {
        parryImg.CrossFadeAlpha(1, 0.1f, false);
        }
    }

}

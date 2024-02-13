using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHUD : MonoBehaviour
{
    public TMPro.TextMeshProUGUI healthText;
    public RectTransform healthRect;

    // Cache width of health bar visual
    private float cachedWidth;
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

}

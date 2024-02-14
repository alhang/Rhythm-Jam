using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : Drop
{
    public float addBaseSpeed = 2;
    public float addDashSpeed = 3;
    public float addBaseDamage = 1;
    public float addMaxHealth = 10;
    public float addBaseRegen = 1;
    [SerializeField] PlayerStatsSO playerStats;

    public enum UpgradeType
    {
        BaseSpeed,
        DashSpeed,
        BaseDamage,
        MaxHealth,
        BaseRegen
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Pickup(Player player)
    {
        Debug.Log("Upgrade");

        // Ranodom upgrade
        UpgradeType upgrade = (UpgradeType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(UpgradeType)).Length);
        Debug.Log("Upgrade: " + upgrade.ToString());

        switch(upgrade) 
        {
        case UpgradeType.BaseSpeed:
            playerStats.baseSpeed += addBaseSpeed;
            break;
        case UpgradeType.DashSpeed:
            playerStats.dashSpeed += addDashSpeed;
            break;
        case UpgradeType.BaseDamage:
            playerStats.baseDamage += addBaseDamage;
            break;
        case UpgradeType.MaxHealth:
            Player.maxHealth += addMaxHealth;
            break;
        case UpgradeType.BaseRegen:
            playerStats.baseRegen += addBaseRegen;
            break;
        default:
            // code block
            break;
        }
        
        Destroy(gameObject);
    }
}

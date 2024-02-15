using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsPage : MonoBehaviour
{

    [SerializeField] PlayerStatsSO playerStats;
    public TMPro.TextMeshProUGUI statsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayStats()
    {
        string displayString = $"Base Speed: {playerStats.baseSpeed}\n" +
                               $"Dash Speed: {playerStats.dashSpeed}\n" +
                               $"Base Damage: {playerStats.baseDamage}\n" +
                               $"Base Regen: {playerStats.baseRegen}\n" +
                               $"Base Regen Rate: {playerStats.baseRegenRate}";

        statsText.SetText(displayString);
    }
}

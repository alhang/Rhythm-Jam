using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStatsSO : ScriptableObject
{
    [Range(0.1f, 20f)]
    public float baseSpeed;
    [Range(0.1f, 20f)]
    public float dashSpeed;
    [Range(0.1f, 20f)]
    public float baseDamage;
    [Range(0.1f, 20f)]
    public float baseRegen;
    [Range(0.1f, 20f)]
    public float baseRegenRate;

    void Awake()
    {
        ResetStats();
    }
    public void ResetStats() 
    {
        baseSpeed = 2;
        dashSpeed = 4;
        baseDamage = 1;
        baseRegen = 1;
        baseRegenRate = 1;
    }
}

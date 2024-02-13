using UnityEngine;
using System.Collections;

public class FirstBoss : MonoBehaviour
{
    public float baseSpeed = 2;
    public float baseDamage = 1;
    public float baseHealth = 1;
    public float baseRegen = 1;
    public GameObject enemy;
    private BeatListener beatListener;

    private int beatsUntilReset = 16;
    private int numBeats = 0;

    private int phase = 1;

    public Pistol pistol;
    public Shotgun shotgun;

    public void BeatCounter()
    {
        numBeats++;

        if (numBeats >= beatsUntilReset)
            numBeats = 0;

        TryAttack();
    }

    public void TryAttack()
    {
        if (phase == 1)
            PhaseOneAttack();
        else
            PhaseTwoAttack();

    }

    void PhaseOneAttack()
    {

    }

    void BasicAttack(float projectileSpeed)
    {

    }

    void ShotgunAttack()
    {

    }

    void SpecialRangedAttack()
    {

    }



    void PhaseTwoAttack()
    {

    }




    void PhaseThreeAttack()
    {

    }

    void BasicMeleeAttack()
    {

    }

    void SpecialMeleeAttack()
    {

    }
}


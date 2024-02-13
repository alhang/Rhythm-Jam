using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FirstBoss : MonoBehaviour
{
    public float baseSpeed = 2;
    public float baseDamage = 1;
    public float baseHealth = 100;
    public float baseRegen = 1;

    public int beatsUntilReset = 16;
    public int numBeats = 0;

    public int phase = 1;

    public Pistol leftPistol;
    public Pistol rightPistol;
    public Shotgun leftShotgun;
    public Shotgun middleShotgun;
    public Shotgun rightShotgun;

    public int timeStampIndex = 0;
    public List<TimeStamps> timeStamps;
    private TimeStamps curTimeStamp => timeStamps[timeStampIndex];

    private void Update()
    {
        if (timeStampIndex < timeStamps.Count && SongManager.time >= curTimeStamp.timeStamp) {
            timeStampIndex++;
            phase = curTimeStamp.phase;
        }
    }

    public void OnBeat()
    {
        numBeats++;

        if (numBeats >= beatsUntilReset)
            numBeats = 0;

        TryAttack();
    }

    public void TryAttack()
    {
        switch (phase)
        {
            case 1:
                PhaseOneAttack();
                break;
            case 2:
                PhaseTwoAttack();
                break;
            case 3:
                PhaseThreeAttack();
                break;
            case 4:
                PhaseFourAttack();
                break;
            case 5:
                PhaseFiveAttack();
                break;
        }

    }

    void PhaseOneAttack()
    {
        if (numBeats % 2 == 0)
            BasicAttack(leftPistol, 5);
        else
            BasicAttack(rightPistol, 5);
    }

    void PhaseTwoAttack()
    {
        if (numBeats % 8 < 4)
        {
            BasicAttack(leftPistol, 5);
            BasicAttack(rightPistol, 5);
        }
        if ((numBeats - 4) % 8 == 0)
        {
            ShotgunAttack(leftShotgun, 30, 7, 5);
            ShotgunAttack(rightShotgun, 30, 7, 5);
        }
    }

    void PhaseThreeAttack()
    {
        if ((numBeats - 2) % 4 == 0)
            ShotgunAttack(leftShotgun, 25, 3, 5);
        else if (numBeats % 4 == 0)
            ShotgunAttack(rightShotgun, 25, 3, 5);

        if (numBeats % 8 == 0)
            ShotgunAttack(middleShotgun, 18, 20, 5);
    }

    void PhaseFourAttack()
    {
        int beats = numBeats - 4;
        if (beats % 4 == 0)
        {
            ShotgunAttack(leftShotgun, 45, 8, 5);
            ShotgunAttack(rightShotgun, 45, 8, 5);
        }
        if (beats % 8 == 0)
            ShotgunAttack(middleShotgun, 18, 20, 5);
    }

    void PhaseFiveAttack()
    {
        int[] beats = { 3, 7, 10 };

        foreach (int beat in beats)
        {
            if(numBeats == beat)
                ShotgunAttack(middleShotgun, 18, 20, 5);
        }
    }


    void BasicAttack(Pistol pistol, int projectileSpeed)
    {
        pistol.projectileSpeed = projectileSpeed;
        pistol.Attack();
    }

    void ShotgunAttack(Shotgun shotgun, int spread, int numBullets, int projectileSpeed)
    {
        shotgun.spread = spread;
        shotgun.numBullets = numBullets;
        shotgun.projectileSpeed = projectileSpeed;
        shotgun.Attack();
    }

    void BasicMeleeAttack()
    {

    }

    void SpecialMeleeAttack()
    {

    }

    void ResetBeats()
    {
        numBeats = 0;
    }
}

[System.Serializable]
public class TimeStamps
{
    public float timeStamp;
    public int phase;
}


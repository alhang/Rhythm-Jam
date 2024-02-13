using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FirstBoss : Enemy
{
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
            case 6:
                PhaseSixAttack();
                break;
            default:
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
            ShotgunAttack(leftShotgun, 30, 6, 5);
            ShotgunAttack(rightShotgun, 30, 6, 5);
        }
    }

    void PhaseThreeAttack()
    {
        if (numBeats % 4 == 0)
        {
            ShotgunAttack(leftShotgun, 20, 4, 5);
            ShotgunAttack(rightShotgun, 20, 4, 5);
        }

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

    public int phaseFiveOffset = 0;
    void PhaseFiveAttack()
    {
        int beat = (numBeats + phaseFiveOffset) % 16;
        int[] beats = { 0, 1, 0, 2, 0, 3, 0, 0, 0, 1, 0, 2, 1, 0, 3, 0 };

        if (beats[beat] == 1)
            ShotgunAttack(leftShotgun, 60, 6, 5);
        if (beats[beat] == 2)
            ShotgunAttack(rightShotgun, 60, 6, 5);
        if (beats[beat] == 3)
            ShotgunAttack(middleShotgun, 36, 10, 5);
    }

    void PhaseSixAttack()
    {
        if (numBeats % 4 == 0)
            BasicAttack(leftPistol, 5);
        else if (numBeats % 4 == 2)
            BasicAttack(rightPistol, 5);

        int beat = (numBeats + phaseFiveOffset) % 16;
        int[] beats = { 0, 1, 0, 2, 0, 3, 0, 0, 0, 1, 0, 2, 1, 0, 3, 0 };

        if (beats[beat] == 1)
            ShotgunAttack(leftShotgun, 36, 10, 5);
        if (beats[beat] == 2)
            ShotgunAttack(rightShotgun, 36, 10, 5);
        if (beats[beat] == 3)
            ShotgunAttack(middleShotgun, 18, 20, 5);
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


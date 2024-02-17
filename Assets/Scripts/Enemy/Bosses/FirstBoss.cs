using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

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

    [SerializeField] private TextMeshProUGUI timer;
    public float maxTimer = 253.022f;
    private float timeLeft = 0;

    private bool hasBattleEnded = false;

    [SerializeField] Totem totemPrefab;
    public List<Transform> totemSpawnPositions;
    public static List<Transform> avaliableSpawnPositions;

    private void Start()
    {
        avaliableSpawnPositions = new List<Transform>(totemSpawnPositions);
    }

    private void OnEnable()
    {
        GameManager.OnPlayerEnter += OnPlayerEnter;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerEnter -= OnPlayerEnter;
    }

    private void OnPlayerEnter()
    {
        StartCoroutine(FadeToRed());
        StartCoroutine(Battle());
    }

    private IEnumerator FadeToRed()
    {
        float elapsedTime = 0;
        float maxTime = 2;
        while (elapsedTime < maxTime)
        {
            elapsedTime += Time.deltaTime;
            float percentDone = elapsedTime / maxTime;
            GameManager.Instance.baseTileMap.color = Color.Lerp(Color.white, Color.red, percentDone);
            yield return null;
        }
        GameManager.Instance.baseTileMap.color = Color.red;
    }

    private IEnumerator TotemSpawner()
    {
        yield return new WaitForSeconds(5);
        SpawnTotem();
        yield return new WaitForSeconds(5);
        while (!hasBattleEnded)
        {
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => avaliableSpawnPositions.Count > 0);
            float val = Mathf.Lerp(0.05f, 0.1f, SongManager.time / maxTimer);
            float rng = Random.Range(0, 1f);
            if (rng < val)
                SpawnTotem();
        }
    }

    public void SpawnTotem()
    {
        int randomIndex = Random.Range(0, avaliableSpawnPositions.Count);
        Transform randomPosTransf = avaliableSpawnPositions[randomIndex]; 
        Instantiate(totemPrefab, randomPosTransf.position, Quaternion.identity, randomPosTransf);
        avaliableSpawnPositions.RemoveAt(randomIndex);
    }

    public override void FlipSprite()
    {
        if (Player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private IEnumerator Battle()
    {
        SongManager.Instance.Play();
        StartCoroutine(TotemSpawner());
        while (!hasBattleEnded)
        {
            if (timeStampIndex < timeStamps.Count && SongManager.time >= curTimeStamp.timeStamp)
            {
                timeStampIndex++;
                phase = curTimeStamp.phase;
            }

            timeLeft = maxTimer - SongManager.time;
            timeLeft = timeLeft < 0.1f ? 0 : timeLeft;
            timer.text = timeLeft.ToString("0.00");

            if (timeLeft == 0 && !hasBattleEnded)
            {
                EndBattle();
                hasBattleEnded = true;
            }
            yield return null;
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

    public int projectileSpeed = 10;

    void PhaseOneAttack()
    {
        if (numBeats % 2 == 0)
            BasicAttack(leftPistol, projectileSpeed);
        else
            BasicAttack(rightPistol, projectileSpeed);
    }

    void PhaseTwoAttack()
    {
        if (numBeats % 8 < 4)
        {
            BasicAttack(leftPistol, projectileSpeed);
            BasicAttack(rightPistol, projectileSpeed);
        }
        if ((numBeats - 4) % 8 == 0)
        {
            ShotgunAttack(leftShotgun, 30, 6, projectileSpeed);
            ShotgunAttack(rightShotgun, 30, 6, projectileSpeed);
        }
    }

    void PhaseThreeAttack()
    {
        if (numBeats % 4 == 0)
        {
            ShotgunAttack(leftShotgun, 20, 4, projectileSpeed);
            ShotgunAttack(rightShotgun, 20, 4, projectileSpeed);
        }

        if (numBeats % 8 == 0)
            ShotgunAttack(middleShotgun, 20, 18, projectileSpeed);
    }

    void PhaseFourAttack()
    {
        int beats = numBeats - 4;
        if (beats % 4 == 0)
        {
            ShotgunAttack(leftShotgun, 45, 8, projectileSpeed);
            ShotgunAttack(rightShotgun, 45, 8, projectileSpeed);
        }
        if (beats % 8 == 0)
            ShotgunAttack(middleShotgun, 20, 18, projectileSpeed);
    }

    public int phaseFiveOffset = 0;
    void PhaseFiveAttack()
    {
        int beat = (numBeats + phaseFiveOffset) % 16;
        int[] beats = { 0, 1, 0, 2, 0, 3, 0, 0, 0, 1, 0, 2, 1, 0, 3, 0 };

        if (beats[beat] == 1)
            ShotgunAttack(leftShotgun, 60, 6, projectileSpeed);
        if (beats[beat] == 2)
            ShotgunAttack(rightShotgun, 60, 6, projectileSpeed);
        if (beats[beat] == 3)
            ShotgunAttack(middleShotgun, 36, 10, projectileSpeed);
    }

    void PhaseSixAttack()
    {
        if (numBeats % 4 == 0)
            BasicAttack(leftPistol, projectileSpeed);
        else if (numBeats % 4 == 2)
            BasicAttack(rightPistol, projectileSpeed);

        int beat = (numBeats + phaseFiveOffset) % 16;
        int[] beats = { 0, 1, 0, 2, 0, 3, 0, 0, 0, 1, 0, 2, 1, 0, 3, 0 };

        if (beats[beat] == 1)
            ShotgunAttack(leftShotgun, 36, 10, projectileSpeed);
        if (beats[beat] == 2)
            ShotgunAttack(rightShotgun, 36, 10, projectileSpeed);
        if (beats[beat] == 3)
            ShotgunAttack(middleShotgun, 20, 18, projectileSpeed);
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

    void EndBattle()
    {
        GameManager.Instance.OnRoomClear();
    }
}

[System.Serializable]
public class TimeStamps
{
    public float timeStamp;
    public int phase;
}


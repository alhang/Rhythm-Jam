using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System;
using System.Numerics;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public GameObject playerCamera;
    public Transform startingPos;
    public Animator roomTransition;

    public GameObject closedGameArea;

    public GameObject arrows;

    public int difficulty = 1;

    public string bossSceneName;
    public int difficultyToReachBoss = 20;

    AsyncSceneLoader asyncSceneLoader;

    public int enemiesLeft;

    public static event Action OnPlayerEnter;

    private void Start()
    {
        SpawnPlayerIn();
    }

    public void OnEnable()
    {
        Enemy.OnEnemyKill += EnemyKilled;
    }

    public void OnDisable()
    {
        Enemy.OnEnemyKill -= EnemyKilled;
    }

    public void EnemyKilled(Enemy enemy)
    {
        if (EnemySpawner.Instance)
        {
            enemiesLeft--;
            if (enemiesLeft == 0)
                OnRoomClear();
        }
    }

    private bool hasPlayerEntered = false;
    public void OnPlayerEnterGameArea()
	{
        if (hasPlayerEntered)
            return;

        hasPlayerEntered = true;
        closedGameArea.SetActive(true);
        Enemy.AggroAllEnemies(true);
        OnPlayerEnter?.Invoke();
    }

    public void OnRoomClear()
    {
        arrows.SetActive(true);
        closedGameArea.SetActive(false);

        if (difficulty >= difficultyToReachBoss)
        {
            Debug.Log("Loading boss");
            asyncSceneLoader = new AsyncSceneLoader(bossSceneName);
            StartCoroutine(asyncSceneLoader.Load());
        }
    }

    public bool hasPlayerExited;

    public void OnPlayerExitGameArea(int route)
    {
        if (hasPlayerExited)
            return;

        if (difficulty >= difficultyToReachBoss)
            asyncSceneLoader.AllowSceneActivation();

        if (route != 1)
            difficulty += 3;
        else
            difficulty += 6;

        hasPlayerExited = true;

        StartCoroutine(RoomTransition());
    }

    public IEnumerator RoomTransition()
    {
        playerCamera.SetActive(false);
        roomTransition.SetTrigger("Exit");
        yield return new WaitForSeconds(0.3f);

        SpawnPlayerIn();
    }

    public Tilemap baseTileMap;

    public void SpawnPlayerIn()
    {
        player.gameObject.transform.position = startingPos.position;

        // Spawn upgrade
        if(UpgradeSpawner.Instance)
            UpgradeSpawner.Instance.SpawnUpgrade(startingPos.position + new UnityEngine.Vector3(0, 10, 0));

        Enemy.AggroAllEnemies(false);
        if (EnemySpawner.Instance)
            enemiesLeft = EnemySpawner.Instance.PrepopulateRoom(difficulty);

        playerCamera.SetActive(true);
        arrows.SetActive(false);
        roomTransition.SetTrigger("Enter");
        hasPlayerExited = false;
        hasPlayerEntered = false;
    }
}


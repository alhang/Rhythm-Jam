using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public GameObject playerCamera;
    public Transform startingPos;
    public Animator roomTransition;

    public int enemiesToKillForRoomClear = 10;
    public int enemiesKilledInRoom = 0;

    public GameObject closedGameArea;

    public GameObject arrows;

    public int difficulty = 1;

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
        enemiesKilledInRoom++;
        if (enemiesKilledInRoom >= enemiesToKillForRoomClear)
            OnRoomClear();
    }

    public void OnPlayerEnterGameArea()
	{
        Debug.Log("Player entered");
        closedGameArea.SetActive(true);
    }

    public void OnRoomClear()
    {
        Debug.Log("Room cleared");
        arrows.SetActive(true);
        closedGameArea.SetActive(false);
    }

    public void OnPlayerExitGameArea(int route)
    {
        if (route != 1)
            difficulty++;
        else
            difficulty += 2;
        Debug.Log("Player exited");

        EnemySpawner.Instance.SetDifficulty(difficulty);
        enemiesKilledInRoom = 0;
        Enemy.KillAll();

        StartCoroutine(RoomTransition());
    }

    public IEnumerator RoomTransition()
    {
        playerCamera.SetActive(false);
        roomTransition.SetTrigger("Exit");
        yield return new WaitForSeconds(0.25f);
        player.gameObject.transform.position = startingPos.position;
        playerCamera.SetActive(true);
        arrows.SetActive(false);
        roomTransition.SetTrigger("Enter");
    }
}


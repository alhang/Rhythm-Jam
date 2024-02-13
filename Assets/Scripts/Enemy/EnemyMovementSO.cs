using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy Movement", fileName = "New Enemy Movement")]
public class EnemyMovementSO : ScriptableObject
{
    [Tooltip("Negative to avoid player")]
    [Range(-1,1f)] public float followBias = 0;
    [Range(0, 1f)] public float randomBias = 0;

    public void Move(Enemy enemy, Vector3 randomDirection)
    {
        Vector2 enemyPosition = enemy.transform.position;

        Vector3 followDirection = followBias * Vector3.Normalize(Player.position - enemyPosition);
        randomDirection = randomBias * Vector3.Normalize(randomDirection);

        enemy.transform.position += enemy.baseSpeed * Time.deltaTime * (followDirection + randomDirection);
    }
}


using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        patrol,
        pursuit,
        dead,
    }

    public EnemyState currentEnemyState;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy", fileName = "SO_EnemyData_EnemyName")]
public class EnemyData : ScriptableObject
{
    [field:SerializeField] public float MoveSpeed { get; private set; }
    [field:SerializeField] public int RewardScore { get; private set; }
    [field:SerializeField] public int MaxHealth { get; private set; }
}

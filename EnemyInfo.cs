using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public int Id;
    public GameObject TypeEnemy; // Тип врага
    public int NumberThisNPC; // Кол-во "шеренг" данного типа
    public int DelaySpawnNPC; // Промежуток времени между спавном врагов

    public EnemyInfo(int Id, int NumberThisNPC,int DelaySpawnNPC)
    {
        this.Id = Id;
        this.NumberThisNPC = NumberThisNPC;
        this.DelaySpawnNPC = DelaySpawnNPC;
    }
}

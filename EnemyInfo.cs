using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public int Id;
    public GameObject TypeEnemy; // ��� �����
    public int NumberThisNPC; // ���-�� "������" ������� ����
    public int DelaySpawnNPC; // ���������� ������� ����� ������� ������

    public EnemyInfo(int Id, int NumberThisNPC,int DelaySpawnNPC)
    {
        this.Id = Id;
        this.NumberThisNPC = NumberThisNPC;
        this.DelaySpawnNPC = DelaySpawnNPC;
    }
}

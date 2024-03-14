using UnityEngine;

public class LoadLevelData : MonoBehaviour
{
    [SerializeField] private LoadData fromLoadData;
    [SerializeField] private TMPro.TMP_Text money;
    [SerializeField] private DataBase DB;

    private void Start()
    {
        var fromFile = SaveLoadSystem.LoadLevel();

        fromLoadData.wave = fromFile.waves;
        fromLoadData.towers = fromFile.towers;

        //�������� �� ������, ������� ��������� � "��"
        for (int j = 0; j < DB.enemys.Count; j++)
        {
            // ����������  �� ������
            for (int i = 0; i < fromLoadData.wave.Length; i++)
            {
                //���������� �� ������ � ������ �����
                for (int k = 0; k < fromLoadData.wave[i].EnemyInfo.Length; k++)
                {
                    // ���� ���� �� �� � �� ����������� ����� � �����������
                    //��������� - ������������� � typeEnemy ������ GameObject
                    if (fromLoadData.wave[i].EnemyInfo[k].Id == DB.enemys[j].Id)
                        fromLoadData.wave[i].EnemyInfo[k].TypeEnemy = 
                                                           DB.enemys[j].TypeEnemy;
                }
            }
        }

        for (int j = 0; j < DB.towers.Count; j++)
        {
            for (int i = 0; i < fromLoadData.towers.Length; i++)
            {
                if (DB.towers[j].ID == fromLoadData.towers[i].ID)
                    fromLoadData.towers[i].tower = DB.towers[j].tower;
            }
        }
        money.text = fromFile.money;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

// ������� ������ ������("���")
public class SpawnNpc : MonoBehaviour
{
    // ������� ��� ������
    [SerializeField] private Transform[] spawnPositions;
    //����������, ����������� �� ����� ������
    [SerializeField] private LoadData loadData;

    private int i = 0;


    // ��� ������� �� ������
    public void Spawn()
    {
        if (i < loadData.wave.Length)
        {
            StartCoroutine(GetEnemy(i));
            i++;
        }
    }

    private IEnumerator GetEnemy(int indexWave)
    {
        // �������� ���� ������
        for (int i = 0; i < loadData.wave[indexWave].EnemyInfo.Length; i++)
        {
            // �������� ���-�� "������" � �������
            for (int j = 0; j < loadData.wave[indexWave].EnemyInfo[i].NumberThisNPC; j++)
            {
                // ���� ���. ���-�� ������
                yield return new WaitForSeconds(loadData.wave[indexWave].
                                                EnemyInfo[i].DelaySpawnNPC);

                for (int k = 0; k < spawnPositions.Length; k++)
                    SpawnNPC(indexWave, i, spawnPositions[k]); // �������
            }
        }
    }

    // ����� �����
    private void SpawnNPC(int indexWave, int numberEnemy, Transform spawnPos)
    {
        Instantiate(loadData.wave[indexWave].EnemyInfo[numberEnemy].TypeEnemy,
                    spawnPos.position,Quaternion.identity);
    }

}
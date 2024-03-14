using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

// Система спавна врагов("нпс")
public class SpawnNpc : MonoBehaviour
{
    // позиции для спавна
    [SerializeField] private Transform[] spawnPositions;
    //информация, загруженная из файла уровня
    [SerializeField] private LoadData loadData;

    private int i = 0;


    // При нажатии на кнопку
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
        // получаем типы врагов
        for (int i = 0; i < loadData.wave[indexWave].EnemyInfo.Length; i++)
        {
            // получаем кол-во "шеренг" с врагами
            for (int j = 0; j < loadData.wave[indexWave].EnemyInfo[i].NumberThisNPC; j++)
            {
                // ждем опр. кол-во секунд
                yield return new WaitForSeconds(loadData.wave[indexWave].
                                                EnemyInfo[i].DelaySpawnNPC);

                for (int k = 0; k < spawnPositions.Length; k++)
                    SpawnNPC(indexWave, i, spawnPositions[k]); // Спавним
            }
        }
    }

    // Спавн врага
    private void SpawnNPC(int indexWave, int numberEnemy, Transform spawnPos)
    {
        Instantiate(loadData.wave[indexWave].EnemyInfo[numberEnemy].TypeEnemy,
                    spawnPos.position,Quaternion.identity);
    }

}
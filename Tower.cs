using System.Collections.Generic;
using UnityEngine;

// Башня
public class Tower : MonoBehaviour
{
    private enum TowerTargetPriority
    {
        Standart,
        Random
    }

    [SerializeField] private TowerTargetPriority targetPriority;

    // задержка между выстралами
    [SerializeField] private float attackRate;

    // префаб снаряда
    [SerializeField] private GameObject projectilePrefab;
    // позиция для спавна снаряда
    [SerializeField] private Transform projectileSpawnPos;
    // скорость снаряда
    [SerializeField] private float projectileSpeed;
    // урон
    [SerializeField] private int projectileDamage;

    public int price;

    // лист с врагами, которые задетекченны башней
    private List<Npc> curEnemysInRange = new();

    // выбранная цель
    private Npc curEnemy;

    // сколько времени прошло с момента атаки
    private float lastAttackTime;

    private void Update()
    {
        curEnemy = GetEnemy();
        // сколько времени прошло с момента прошлой атаки
        if (Time.time - lastAttackTime > attackRate)
        {
            lastAttackTime = Time.time;

            if (curEnemy != null) Attack();
        }
    }

    // выбираем цель
    private Npc GetEnemy()
    {
        curEnemysInRange.RemoveAll(x => x == null);

        if (curEnemysInRange.Count == 0) return null;

        if (curEnemysInRange.Count == 1) return curEnemysInRange[0];

        //иначе выбираем нужную цель
        switch (targetPriority)
        {
            case TowerTargetPriority.Standart:
            {
                    return curEnemysInRange[0];
            }

            // возврат рандомного врага
            case TowerTargetPriority.Random:
            {
                    return curEnemysInRange[Random.
                              Range(0, curEnemysInRange.Count)];
            }
        }
        // иначе null
        return null;
    }
    private void Attack()
    {
        // создаем снаряд
        var proj = Instantiate(projectilePrefab, projectileSpawnPos.position,
                               Quaternion.identity);

        // передаем информацию снаряду
        proj.GetComponent<Projectile>().StartProjectile(curEnemy, projectileDamage, 
            projectileSpeed,transform, curEnemy.transform.position);
    } // атакуем
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            curEnemysInRange.Add(other.GetComponent<Npc>());
    } // если нпс вошел в триггер башни
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            curEnemysInRange.Remove(other.GetComponent<Npc>());
    } // если нпс вышел из тригера башни
}
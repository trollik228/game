using System.Collections.Generic;
using UnityEngine;

// �����
public class Tower : MonoBehaviour
{
    private enum TowerTargetPriority
    {
        Standart,
        Random
    }

    [SerializeField] private TowerTargetPriority targetPriority;

    // �������� ����� ����������
    [SerializeField] private float attackRate;

    // ������ �������
    [SerializeField] private GameObject projectilePrefab;
    // ������� ��� ������ �������
    [SerializeField] private Transform projectileSpawnPos;
    // �������� �������
    [SerializeField] private float projectileSpeed;
    // ����
    [SerializeField] private int projectileDamage;

    public int price;

    // ���� � �������, ������� ������������ ������
    private List<Npc> curEnemysInRange = new();

    // ��������� ����
    private Npc curEnemy;

    // ������� ������� ������ � ������� �����
    private float lastAttackTime;

    private void Update()
    {
        curEnemy = GetEnemy();
        // ������� ������� ������ � ������� ������� �����
        if (Time.time - lastAttackTime > attackRate)
        {
            lastAttackTime = Time.time;

            if (curEnemy != null) Attack();
        }
    }

    // �������� ����
    private Npc GetEnemy()
    {
        curEnemysInRange.RemoveAll(x => x == null);

        if (curEnemysInRange.Count == 0) return null;

        if (curEnemysInRange.Count == 1) return curEnemysInRange[0];

        //����� �������� ������ ����
        switch (targetPriority)
        {
            case TowerTargetPriority.Standart:
            {
                    return curEnemysInRange[0];
            }

            // ������� ���������� �����
            case TowerTargetPriority.Random:
            {
                    return curEnemysInRange[Random.
                              Range(0, curEnemysInRange.Count)];
            }
        }
        // ����� null
        return null;
    }
    private void Attack()
    {
        // ������� ������
        var proj = Instantiate(projectilePrefab, projectileSpawnPos.position,
                               Quaternion.identity);

        // �������� ���������� �������
        proj.GetComponent<Projectile>().StartProjectile(curEnemy, projectileDamage, 
            projectileSpeed,transform, curEnemy.transform.position);
    } // �������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            curEnemysInRange.Add(other.GetComponent<Npc>());
    } // ���� ��� ����� � ������� �����
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            curEnemysInRange.Remove(other.GetComponent<Npc>());
    } // ���� ��� ����� �� ������� �����
}
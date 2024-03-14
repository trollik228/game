using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    //��������
    [SerializeField] private float speed;
    //������ ����� ����������� ������� �����
    [SerializeField] private int moneyDeath;
    //��������
    [SerializeField] private int health;
    //����
    public int damage;

    public float X = 0;
    public float Z = -1;

    private void Update()
    {
        if (health <= 0)
        {
            MoneySystem.TakeMoney(moneyDeath);
            Destroy(gameObject);
        }
        transform.position += new Vector3(X, 0, Z) * speed * Time.deltaTime;
    }

    public void TakeDamage(int damage) 
    { 
        health -= damage;
    }

}

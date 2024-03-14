using UnityEngine;

// Снаряд
public class Projectile : MonoBehaviour
{
    private Npc target; // переданная цель
    private int damage; // переданный урон
    private float moveSpeed; // переданная скорость
    private Transform towerPosition; // позиция башни
    private Vector3 targetPosition; // цель


    // передаем данные из башни в снаряд
    public void StartProjectile(Npc target, int damage, float moveSpeed, 
                           Transform towerPosition, Vector3 targetPosition)
    {
        this.target = target;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.towerPosition = towerPosition;
        this.targetPosition = targetPosition;
    }
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                 targetPosition, moveSpeed * Time.deltaTime);

            transform.LookAt(targetPosition);

            if (Vector3.Distance(transform.position, towerPosition.position) > 15f)
                Destroy(gameObject);
            
            if (Vector3.Distance(transform.position, targetPosition) < 1f)
            {
                target.TakeDamage(damage); // наносим урон
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject); // если цель вдруг была уничтожена,
                                 // а снаряд нет, то уничтожаем его
    }
}
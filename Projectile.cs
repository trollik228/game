using UnityEngine;

// ������
public class Projectile : MonoBehaviour
{
    private Npc target; // ���������� ����
    private int damage; // ���������� ����
    private float moveSpeed; // ���������� ��������
    private Transform towerPosition; // ������� �����
    private Vector3 targetPosition; // ����


    // �������� ������ �� ����� � ������
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
                target.TakeDamage(damage); // ������� ����
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject); // ���� ���� ����� ���� ����������,
                                 // � ������ ���, �� ���������� ���
    }
}
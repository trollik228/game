using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float X;
    [SerializeField] private float Z;

    // меняем направление врага
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Npc>().X = X;
            other.GetComponent<Npc>().Z = Z;
        }
    }
}

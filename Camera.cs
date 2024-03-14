using UnityEngine;

// ������
public class Camera : MonoBehaviour
{
    [SerializeField] private float speedCamera = 20f;
    [SerializeField] private float speedScrollCamera = 60;

    private void Update()
    {
        // �������� WASD
        float directionX = Input.GetAxis("Horizontal");
        float directionZ = Input.GetAxis("Vertical");

        Vector3 nn = speedCamera * Time.deltaTime * 
            transform.TransformDirection(directionX, 0, directionZ);

        transform.position += new Vector3(nn.x, 0, nn.z);

        // ������ ���������
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0 && transform.position.y > 9.82f) 
            transform.position += transform.forward * Time.deltaTime 
                                  * speedScrollCamera;//�����������
        
        if (mw < 0) 
            transform.position -= transform.forward * Time.deltaTime 
                                  * speedScrollCamera;//���������
    } // ���������� �������
}

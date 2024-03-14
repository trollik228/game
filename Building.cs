using UnityEngine;

// ������� �������������
public class Building : MonoBehaviour
{
    // ������� ��� ����� �������
    [SerializeField] private GameObject planeForBuild;
    // �� ��� ���� ��������� �� ����� ������
    [SerializeField] private LoadData loadData;
    // ������ ��� ������� �����
    [SerializeField] private GameObject firstPrefab;

    // ��������� �����(���������� �������� �� �����. ������)
    private GameObject currentTower;
    // ��������������
    private  GameObject startPrefab; 

    // ����� �� ���������� ����� �� ������ �����������
    public static bool allowConstruction = true;


    // ��� 1 ������
    public void PushTower()
    {
        if (currentTower == null)
        {
            currentTower = loadData.towers[0].tower;
            startPrefab = Instantiate(firstPrefab);
            PlaneForBuilding();
        }
    }
    private void Update()
    {
        if (currentTower != null && startPrefab != null)
            MovementTower(1);
    }

    // ����������� startPrefab
    private void MovementTower(int typeTower)
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, ~(1 << 9)))
        {
            // startPrefab ��������� �� ��������
            startPrefab.transform.position = new Vector3(hit.point.x, 
                                                         hit.point.y, 
                                                         hit.point.z);

            SpawnTower(hit);
        }
    }

    // ������������� ������ startPrefab currentTower
    private void SpawnTower(RaycastHit ray)
    {
        // ���� ������ ���
        if (Input.GetMouseButtonDown(0))
        {
            // ���� ������ ��������� �� ������� ��� �������������
            if (ray.collider.gameObject.CompareTag("Plane For Building"))
            {
                // ���� ���������� ���������
                if (allowConstruction)
                {
                    // ���� ���-�� ����� �� ������ ������ ������ ��� ����� ���� �����
                    if (MoneySystem.ReturnMoney() >= currentTower.
                                        GetComponent<Tower>().price)
                    {
                        // ������� �����
                        Instantiate(currentTower, startPrefab.transform.position, 
                            Quaternion.identity);

                        // ������� ������
                        Destroy(startPrefab);
                        startPrefab = null;
                        currentTower = null;

                        // �������� ���� ����� �� �����
                        MoneySystem.TakeMoney(-loadData.towers[0].tower
                            .GetComponent<Tower>().price);

                        // ��������� ������� ��� �������������
                        OffPlaneForBuilding();

                        // ����� ������
                    }
                    else Debug.Log("������������ �������");
                }
                else Debug.Log("������� ������ � ������ �����");
            }
            else Debug.Log("������ ��������� ���");
        }
        // ���� ������ ��� ��� �������
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(startPrefab);
            startPrefab = null;
            currentTower = null;
            OffPlaneForBuilding();
        }
    }

    // ���������� ������� ��� �������������
    private void PlaneForBuilding() { planeForBuild.SetActive(true); }

    // ��������� ������� ��� �������������
    private void OffPlaneForBuilding() { planeForBuild.SetActive(false); } 
}
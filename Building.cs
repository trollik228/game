using UnityEngine;

// Система строительства
public class Building : MonoBehaviour
{
    // область где можно строить
    [SerializeField] private GameObject planeForBuild;
    // то что было прочитано из файла уровня
    [SerializeField] private LoadData loadData;
    // префаб для зеленой башни
    [SerializeField] private GameObject firstPrefab;

    // выбранная башня(выбирается нажатием на соотв. кнопку)
    private GameObject currentTower;
    // параллелипипед
    private  GameObject startPrefab; 

    // можно ли разместить башню на данных координатах
    public static bool allowConstruction = true;


    // для 1 кнопки
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

    // передвигаем startPrefab
    private void MovementTower(int typeTower)
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, ~(1 << 9)))
        {
            // startPrefab двигается за курсором
            startPrefab.transform.position = new Vector3(hit.point.x, 
                                                         hit.point.y, 
                                                         hit.point.z);

            SpawnTower(hit);
        }
    }

    // устанавливаем вместо startPrefab currentTower
    private void SpawnTower(RaycastHit ray)
    {
        // если нажата ПКМ
        if (Input.GetMouseButtonDown(0))
        {
            // если обьект находится на области для строительства
            if (ray.collider.gameObject.CompareTag("Plane For Building"))
            {
                // если размещение разрешено
                if (allowConstruction)
                {
                    // если кол-во денег на данный момент больше или равно цене башни
                    if (MoneySystem.ReturnMoney() >= currentTower.
                                        GetComponent<Tower>().price)
                    {
                        // создаем башню
                        Instantiate(currentTower, startPrefab.transform.position, 
                            Quaternion.identity);

                        // очищаем данные
                        Destroy(startPrefab);
                        startPrefab = null;
                        currentTower = null;

                        // отнимаем цену башни из денег
                        MoneySystem.TakeMoney(-loadData.towers[0].tower
                            .GetComponent<Tower>().price);

                        // отключаем области для строительства
                        OffPlaneForBuilding();

                        // иначе ошибка
                    }
                    else Debug.Log("Недостаточно средств");
                }
                else Debug.Log("Слишком близко к другой башне");
            }
            else Debug.Log("Нельзя поставить тут");
        }
        // если нажали ПКМ все очищаем
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(startPrefab);
            startPrefab = null;
            currentTower = null;
            OffPlaneForBuilding();
        }
    }

    // активируем области для строительства
    private void PlaneForBuilding() { planeForBuild.SetActive(true); }

    // отключаем области для строительства
    private void OffPlaneForBuilding() { planeForBuild.SetActive(false); } 
}
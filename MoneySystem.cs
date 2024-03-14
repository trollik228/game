using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public static TMPro.TMP_Text money;
    [SerializeField] private TMPro.TMP_Text Money;

    private void Start() 
    { 
        money = Money;
    }

    /// <summary>
    /// �������� ���������� ������������ ����� �����
    /// </summary>
    /// <param name="cash"></param>
    public static void TakeMoney(int cash)
    {
        money.text = (int.Parse(money.text) + cash).ToString();
    }

    /// <summary>
    /// ���������� ���������� �����
    /// </summary>
    /// <returns></returns>
    public static int ReturnMoney() 
    { 
        return int.Parse(money.text);
    }
}

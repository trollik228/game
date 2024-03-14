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
    /// Изменяет количество отображаемой суммы денег
    /// </summary>
    /// <param name="cash"></param>
    public static void TakeMoney(int cash)
    {
        money.text = (int.Parse(money.text) + cash).ToString();
    }

    /// <summary>
    /// Возвращает количество денег
    /// </summary>
    /// <returns></returns>
    public static int ReturnMoney() 
    { 
        return int.Parse(money.text);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyBalance;

    private void Update()
    {
        moneyBalance.text = money.ToString();
    }
}

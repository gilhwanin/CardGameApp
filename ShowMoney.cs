using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoney : MonoBehaviour
{
    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        t.text = CardList.instance.money + "";
    }
    public void RenewMoney()
    {
        t.text = CardList.instance.money + "";
    }
}

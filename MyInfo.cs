using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInfo : MonoBehaviour
{
    private void Start()
    {
        CardList.instance.RenewDeck();
        CardList.instance.RenewInventory();
    }
    public void EquipItem(Item item, int n)
    {
        StatBoard.statBoardOn = false;
        CardList.instance.myDeck[n].item = item;
        CardList.instance.myDeck[n].EquipItem(item);
        item.occ = true;
        item.connectedCard = n;
        for (int i = 0; i < 7; i++)
        {
            if (StatBoard.buttons[i] != null) Destroy(StatBoard.buttons[i].gameObject);
        }
        CardList.instance.RenewDeck();
        CardList.instance.RenewInventory();
        Utils.Save();
    }
}

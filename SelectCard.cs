using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    public int n;

    public void Pick()
    {
        GameObject.Find("Display").GetComponent<Matchup>().SelectCard(n);
    }

    public void Pick_StartingMemeber()
    {
        GameObject.Find("EventSystem").GetComponent<StartingMember>().SelectCard(n);
        Destroy(gameObject);
    }

    public void Pick_CustomCard()
    {
        GameObject.Find("EventSystem").GetComponent<MyInfo>().EquipItem(CardList.instance.myItem[StatBoard.itemNum], n);
    }

    public void Pick_DumpCard()
    {
        if (CardList.instance.myDeck[n].item != null)
        {
            CardList.instance.myDeck[n].item.occ = false;
        }
        CardList.instance.myDeck.RemoveAt(n);
        var card = GameObject.Find("EventSystem").GetComponent<RandomCard>().card;
        CardList.instance.myDeck.Insert(n, card);
        CardList.instance.RenewDeck();
        Utils.Save();
        for (int i = 0; i < 7; i++)
        {
            if (StatBoard.buttons[i] != null) Destroy(StatBoard.buttons[i].gameObject);
        }
        CardList.instance.RenewDeck();
    }
    public void Pick_DumpItem()
    {
        if (CardList.instance.myItem[n].occ == true)
        {
            CardList.instance.myDeck[CardList.instance.myItem[n].connectedCard].item = null;
        }
        CardList.instance.myItem.RemoveAt(n);
        var item = GameObject.Find("EventSystem").GetComponent<ItemDisplay>().item;
        CardList.instance.myItem.Insert(n, item);
        CardList.instance.RenewInventoryBelow();
        Utils.Save();
        for (int i = 0; i < 14; i++)
        {
            if (StatBoard.buttons[i] != null) Destroy(StatBoard.buttons[i].gameObject);
        }
        CardList.instance.RenewInventoryBelow();
    }
}

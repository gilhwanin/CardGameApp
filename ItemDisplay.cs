using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public GameObject prefab, prefab2, obj;
    public Item item;

    private void Start()
    {
        RandomGet();
    }
    public void RandomGet()
    {
        item = new Item();
        obj = Instantiate(prefab);
        Vector3 pos = new Vector3(0, 0, 0);
        obj.transform.position = pos;
        obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(item.type);
        obj.GetComponent<ItemScript>().item = item;
        Utils.Save();
    }

    public void GetItem()
    {
        if (CardList.instance.myItem.Count < 14)
        {
            ObjUp();
            CardList.instance.myItem.Add(item);
            CardList.instance.RenewInventoryBelow();
            Utils.Save();
        }
        else
        {
            StatBoard.statBoardOn = true;
            var warning = Instantiate(prefab2, GameObject.Find("UI").GetComponent<Transform>());
            warning.transform.Find("Text").GetComponent<Text>().text = "인벤토리가 가득 찼습니다. 버리실 아이템을 선택해주세요. \n아이템획득을 원하지 않으면 '뒤로'를 눌러주세요.";
            warning.transform.Find("Button").GetComponent<WarningButton>().errorName = "Full_Item";
        }
    }
    public void ObjUp()
    {
        Vector3 pos = new Vector3(0, 350, 0);
        obj.transform.position = pos;
    }
}

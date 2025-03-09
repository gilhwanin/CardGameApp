using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject prefab;
    public static GameObject temp;
    public Item item;

    void OnMouseDown()
    {
        if (StatBoard.statBoardOn == false)
        {
            temp = Instantiate(prefab, GameObject.Find("UI").GetComponent<Transform>());
            temp.GetComponent<StatBoard>().ItemBoardSet(item);
            StatBoard.statBoardOn = true;
        }
    }
}

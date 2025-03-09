using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCloseButton : MonoBehaviour
{
    public void StatClose()
    {
        if (CardObject.temp != null) Destroy(CardObject.temp);
        if (ItemScript.temp != null) Destroy(ItemScript.temp);
        StatBoard.statBoardOn = false;
    }
}

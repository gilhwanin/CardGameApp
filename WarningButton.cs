using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningButton : MonoBehaviour
{
    public string errorName;
    public Button button;
    public Button button2;
    private void Start()
    {
        StatBoard.statBoardOn = true;
    }
    void RemoveWarning()
    {
        Destroy(gameObject.transform.parent.gameObject);

    }
    public void ErrorButton()
    {
        switch (errorName)
        {
            case "Full_Card":
                CardList.instance.RenewDeck();
                GameObject.Find("EventSystem").GetComponent<RandomCard>().ObjUp();
                for (int i = 0; i < CardList.instance.myDeck.Count; i++)
                {
                    StatBoard.buttons[i] = Instantiate(button, GameObject.Find("UI").GetComponent<Transform>());
                    StatBoard.buttons[i].GetComponent<SelectCard>().n = i;
                    Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 330f * i, -Camera.main.orthographicSize * 0.28f, 0);
                    StatBoard.buttons[i].transform.position = pos;
                }
                break;

            case "Full_Item":
                CardList.instance.RenewInventoryBelow();
                GameObject.Find("EventSystem").GetComponent<ItemDisplay>().ObjUp();
                for (int i = 0; i < CardList.instance.myItem.Count; i++)
                {
                    StatBoard.buttons[i] = Instantiate(button2, GameObject.Find("UI").GetComponent<Transform>());
                    StatBoard.buttons[i].GetComponent<SelectCard>().n = i;
                    Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 330 * (i % 7), -Camera.main.orthographicSize * 0.28f - 400 * (int)(i / 7), 0);
                    StatBoard.buttons[i].transform.position = pos;
                }
                break;
            case "Minimum_card":
                break;
            case "No_Unit_For_Item":
                StatBoard.statBoardOn = false;
                for (int i = 0; i < 7; i++)
                {
                    if (StatBoard.buttons[i] != null) Destroy(StatBoard.buttons[i].gameObject);
                }
                break;
        }
        StatBoard.statBoardOn = false;
        RemoveWarning();
    }
}

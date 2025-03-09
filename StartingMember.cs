using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingMember : MonoBehaviour
{
    public GameObject prefab;
    public GameObject button1;
    public Transform parent;
    public GameObject[] selectionList;
    bool[] cardocc = new bool[6];


    void Start()
    {
        TeamManager.incentiveOn = true;
        selectionList = new GameObject[6];


        DisplayCard();
        DisplayButton();
    }
    public void DisplayCard()
    {
        for (int i = 0; i < 6; i++)
        {
            selectionList[i] = Instantiate(prefab);
            var card = CardList.instance.cardDatabase[i];
            Ora ora = new Ora();
            var card_unit = new Card_Unit(card, ora);
            selectionList[i].GetComponent<CardObject>().AssignCardUnit(card_unit);
            Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 380 * i, +Camera.main.orthographicSize * 0.4f, 0);
            selectionList[i].transform.position = pos;
        }
    }
    public void DisplayButton()
    {
        for (int i = 0; i < 6; i++)
        {
            var button = Instantiate(button1, parent);
            button.GetComponent<SelectCard>().n = i;
            Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 380 * i, 0, 0);
            button.transform.position = pos;
        }
    }
    public void SelectCard(int selectedCard)
    {
        selectionList[selectedCard].transform.Find("CardCanvas").transform.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 50 / 255f);
        CardList.instance.myDeck.Add(selectionList[selectedCard].GetComponent<CardObject>().card);
        CardList.instance.RenewDeck();
    }
}

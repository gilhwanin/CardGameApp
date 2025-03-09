using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matchup : MonoBehaviour
{
    public GameObject prefab;
    public GameObject button1;
    int teamNum;
    bool[] cardocc = new bool[7];
    public bool[] entryocc = new bool[3];
    int[] slot = new int[7];
    GameObject[] entry = new GameObject[3];
    GameObject[] hand = new GameObject[7];
    public Transform parent;
    public Text text1, text2;

    void Start()
    {
        for (int i=0; i<7; i++)
        {
            cardocc[i] = false;
        }
        for (int i = 0; i < 3; i++)
        {
            entryocc[i] = false;
        }
        TextDisplay();
        DisplayHand();
        DisplayOpponentCard();
        DisplayButton();
    }
    public void TextDisplay()
    {
        teamNum = Utils.schedule[Utils.day][8];
        string str = "";
        str = str + "팀명: " + TeamManager.teamListOrigin[9].name + "\n";
        str = str + "순위: " + (TeamManager.teamListOrigin[9].rank+1) + "\n";
        str = str + "점수: " + TeamManager.teamListOrigin[9].point;
        text1.text = str;
        str = "";
        str = str + "팀명: " + TeamManager.teamListOrigin[teamNum].name + "\n";
        str = str + "순위: " + (TeamManager.teamListOrigin[teamNum].rank+1) + "\n";
        str = str + "점수: " + TeamManager.teamListOrigin[teamNum].point;
        text2.text = str;
    }
    public void DisplayOpponentCard()
    {
        teamNum = Utils.schedule[Utils.day][8];
        for (int i = 0; i <= 2; i++)
        {
            var spawnedCard = Instantiate(prefab);
            Vector3 pos = new Vector3(200 + 350 * i, 480, 0);
            spawnedCard.transform.position = pos;
            spawnedCard.GetComponent<CardObject>().AssignCardUnit(CardList.instance.playerDeck[teamNum, i]);
        }
    }
    public void DisplayHand()
    {
        for (int i = 0; i < CardList.instance.myDeck.Count; i++)
        {  
            hand[i] = Instantiate(prefab);
            var card = CardList.instance.myDeck[i];
            hand[i].GetComponent<CardObject>().AssignCardUnit(card);
            Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 330 * i, -Camera.main.orthographicSize * 0.65f, 0);
            hand[i].transform.position = pos;
        }
    }
    public void DisplayButton()
    {
        for (int i = 0; i < CardList.instance.myDeck.Count; i++)
        {
            var button = Instantiate(button1, parent);
            button.GetComponent<SelectCard>().n = i;
            Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 330 * i, -Camera.main.orthographicSize * 0.28f, 0);
            button.transform.position = pos;
        }
    }

    public void SelectCard(int selectedCard)
    {
        if(cardocc[selectedCard]==false)
        {
            for (int i=0; i<3; i++)
            {
                if (entryocc[i] == false)
                {
                    entry[i] = Instantiate(prefab);
                    var card = CardList.instance.myDeck[selectedCard];
                    entry[i].GetComponent<CardObject>().AssignCardUnit(card);
                    Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 350 * i, +Camera.main.orthographicSize * 0.1f, 0);
                    entry[i].transform.position = pos;
                    slot[selectedCard] = i;
                    cardocc[selectedCard] = true;
                    entryocc[i] = true;
                    CardList.instance.entry[i] = selectedCard;
                    hand[selectedCard].transform.Find("CardCanvas").transform.Find("Image").GetComponent<Image>().color= new Color(255, 255, 255, 50/255f);
                    break;
                }
            }
        } 
        else
        {
            Destroy(entry[slot[selectedCard]]);
            cardocc[selectedCard] = false;
            entryocc[slot[selectedCard]] = false;
            hand[selectedCard].transform.Find("CardCanvas").transform.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCard : MonoBehaviour
{
    public Card_Unit card;
    GameObject spawnedCard;
    public GameObject prefab, prefab2;
    GameObject[] hand = new GameObject[7];
    void Start()
    {
        RandomGet();
    }
    public void RandomGet()
    {
        int rarity = Utils.GetRarity(Utils.packNum);
        int r = Random.Range(0, CardList.instance.listOflist[rarity].Count);
        int cardNum = CardList.instance.listOflist[rarity][r];
        Ora ora = new Ora();
        card = new Card_Unit(CardList.instance.cardDatabase[cardNum], ora); 
        spawnedCard = Instantiate(prefab);
        Vector3 pos = new Vector3(0, 0, 0);
        spawnedCard.transform.position = pos;
        spawnedCard.transform.localScale = new Vector3
            (transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, 0);
        spawnedCard.GetComponent<CardObject>().AssignCardUnit(card);
        Utils.Save();
    }

    public void GetCard()
    {
        if(CardList.instance.myDeck.Count < 7)
        {
            ObjUp();
            CardList.instance.myDeck.Add(card);
            CardList.instance.RenewDeck();
            Utils.Save();
        }
        else
        {
            StatBoard.statBoardOn = true;
            var warning = Instantiate(prefab2, GameObject.Find("UI").GetComponent<Transform>());
            warning.transform.Find("Text").GetComponent<Text>().text = "인벤토리가 가득 찼습니다. 버리실 카드를 선택해주세요. \n카드획득을 원하지 않으면 '뒤로'를 눌러주세요.";
            warning.transform.Find("Button").GetComponent<WarningButton>().errorName = "Full_Card";
        }
    }
    public void ObjUp()
    {
        Vector3 pos = new Vector3(0, 250, 0);
        spawnedCard.transform.position = pos;
    }
}

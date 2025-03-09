using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardList : MonoBehaviour
{
    public static CardList instance = null;                 //singleton : 플레이어들의 카드, 아이템, 돈 등 소유 정보를 담을 것임.

    public List<Card> cardDatabase = new List<Card>();
    public List<int> bronzeCard = new List<int>();
    public List<int> silverCard = new List<int>();
    public List<int> goldCard = new List<int>();
    public List<int> captainCard = new List<int>();
    public List<int> legendCard = new List<int>();
    public List<List<int>> listOflist = new List<List<int>>();
    public List<Card_Unit> myDeck = new List<Card_Unit>();
    public List<Item> myItem = new List<Item>();
    public Card_Unit[,] playerDeck;
    public int[] entry = new int[3];
    public string myTeamName;
    public bool[] cardocc = new bool[7];
    public GameObject prefab;
    public GameObject prefab2;
    GameObject[] deck = new GameObject[7];
    GameObject[] inventory = new GameObject[14];
    public int money;
    public Card_Unit[] tocken;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this) Destroy(this.gameObject);
        }

        if (SceneManager.GetActiveScene().name == "TeamGenerate")
        {
            money = 0;
            for (int i = 0; i < 7; i++)
            {
                cardocc[i] = false;
            }
        }
        playerDeck = new Card_Unit[9, 3];

        listOflist.Add(bronzeCard);
        listOflist.Add(silverCard);
        listOflist.Add(goldCard);
        listOflist.Add(captainCard);
        listOflist.Add(legendCard);

        for (int i=0; i<cardDatabase.Count; i++)
        {
            listOflist[cardDatabase[i].rarity].Add(i);
        }
    }
    
    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public void RandomTeamEntry()
    {
        int count=0, level=0;
        for (int i = 0; i < 9; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                count = 0;
                int r = Random.Range(0, 100);
                for (int j=0; j<5; j++)
                {
                    count = count + Utils.difficulty[Utils.league][j];
                    if (r < count)
                    {
                        level = j;
                        break;
                    }
                }
                int cn = Random.Range(0, listOflist[level].Count);
                int cardNum = listOflist[level][cn];
                Ora ora = new Ora();
                var temp_card = new Card_Unit(cardDatabase[cardNum], ora);
                playerDeck[i, k] = temp_card;
                if (playerDeck[i, k].species != "괴물")
                {
                    bool itemhave = Utils.IsItemEquipped();
                    if (itemhave == true)
                    {
                        var tempItem = new Item(true);
                        if (playerDeck[i, k].species == "인간")
                        {
                            tempItem.HumanUp();
                        }
                        tempItem.type = playerDeck[i, k].species;
                        playerDeck[i, k].item = tempItem;
                        playerDeck[i, k].EquipItem(tempItem);
                        playerDeck[i, k].item.occ = true;
                    }
                }
            }
        }
    }
    public void RenewDeck()
    {
        for (int i = 0; i < 7; i++)
        {
            if (deck[i] != null) Destroy(deck[i]);
        }
        for (int i = 0; i < CardList.instance.myDeck.Count; i++)
        {
            deck[i] = Instantiate(prefab);
            var card = CardList.instance.myDeck[i];
            deck[i].GetComponent<CardObject>().AssignCardUnit(card);
            deck[i].GetComponent<CardObject>().slot = i;
            Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 330 * i, -Camera.main.orthographicSize * 0.65f, 0);
            deck[i].transform.position = pos;
        }
    }
    public void RenewInventory()
    {
        for (int i = 0; i < 14; i++)
        {
            if (inventory[i] != null) Destroy(inventory[i]);
        }
        for (int i = 0; i < CardList.instance.myItem.Count; i++)
        {
            inventory[i] = Instantiate(prefab2);
            var item = CardList.instance.myItem[i];
            inventory[i].GetComponent<ItemScript>().item = item;
            inventory[i].GetComponent<ItemScript>().item.slot = i;
            inventory[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(item.type);
            Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 330 * (i % 7), +Camera.main.orthographicSize * 0.5f - 330 * (int)(i / 7), 0);
            inventory[i].transform.position = pos;
            inventory[i].transform.localScale = new Vector3
            (transform.localScale.x * 70, transform.localScale.y * 70, 0);
            if(CardList.instance.myItem[i].occ == true)
            {
                inventory[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            }
        }
    }
    public void RenewInventoryBelow()
    {
        for (int i = 0; i < 14; i++)
        {
            if (inventory[i] != null) Destroy(inventory[i]);
        }
        for (int i = 0; i < CardList.instance.myItem.Count; i++)
        {
            inventory[i] = Instantiate(prefab2);
            var item = CardList.instance.myItem[i];
            inventory[i].GetComponent<ItemScript>().item = item;
            inventory[i].GetComponent<ItemScript>().item.slot = i;
            inventory[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(item.type);
            Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 330 * (i % 7), -Camera.main.orthographicSize * 0.05f - 400 * (int)(i / 7), 0);
            inventory[i].transform.position = pos;
            inventory[i].transform.localScale = new Vector3
            (transform.localScale.x * 60, transform.localScale.y * 60, 0);
            if (CardList.instance.myItem[i].occ == true)
            {
                inventory[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            }
        }
    }
}
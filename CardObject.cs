using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObject : MonoBehaviour
{
    public GameObject prefab1;
    public Transform transform1;
    public static GameObject temp;
    public int slot;
    Transform t;

    public Card_Unit card;
    public Card_Battle card_battle;

    public Slider slider;
    public Image img, frame, rank, state;
    public TextMeshProUGUI stateText;


    void Start()
    {
        t = GameObject.Find("UI").GetComponent<Transform>();
    }

    public void AssignCardUnit(Card_Unit card)
    {
        this.card = card;
        this.img.sprite = Resources.Load<Sprite>("CardImage/"+card.image);
        this.slider.maxValue = card.hp;
        this.slider.value = card.hp;
        this.frame.color = Utils.GetFrameColor(card.attribute);
        if(card.rarity != 0)
        {
            this.rank.gameObject.SetActive(true);
            this.rank.sprite = Resources.Load<Sprite>(card.rarity+"");
        }
    }
    public void AssignCardBattle(Card_Battle card)
    {
        this.card_battle = card;
    }

    void OnMouseDown()
    {
        if (StatBoard.statBoardOn == false)
        {
            temp = Instantiate(prefab1, t);
            temp.GetComponent<StatBoard>().StatBoardSet(card);
            StatBoard.cardNum = slot;
            StatBoard.statBoardOn = true;
        }
    }
    public void Damaged(int damage)
    {
        card_battle.chp -= damage;
        this.slider.value = card_battle.chp;
        if (card_battle.chp <= 0)
        {
            gameObject.transform.Find("CardCanvas").transform.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 50 / 255f);
        }
    }
    public IEnumerator Damaging(int damage)
    {
        int n = damage;
        if (card_battle.chp < n)
        {
            n = card_battle.chp;
        }
            while (n > 0)
        {
            card_battle.chp -= 1;
            this.slider.value = card_battle.chp;
            n--;
            yield return new WaitForSeconds(0.05f);
        }
        if (card_battle.chp<=0)
        {
            gameObject.transform.Find("CardCanvas").transform.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 50 / 255f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatBoard : MonoBehaviour
{
    public Image img;
    public Text name1, stat, ora, property, item, plus, use, skilltree;
    public static bool statBoardOn = false;

    public Image img2;
    public Text text1, value;

    public static int itemNum, cardNum;
    public static Button[] buttons = new Button[14];

    public void StatBoardSet(Card_Unit card)
    {
        img.sprite = Resources.Load<Sprite>("CardImage/" + card.image);
        name1.text = card.name;
        ora.text = card.ora.value_hp + "\n" + card.ora.value_ap + "\n" + card.ora.value_dp + "\n" + card.ora.value_sp + "\n" + card.ora.value_cp + "\n" + card.ora.value_ep;
        property.text = "종족: " + card.species + "\n속성: " + card.attribute + "\n등급: " + Utils.rarityName[card.rarity];

        string str = "";
        string[] sign = { "", "", "%" };

        str = str + card.hp + "\n";
        str = str + card.ap + "\n";
        str = str + card.dp + "\n";
        str = str + card.sp + "\n";
        str = str + card.cp + "\n";
        str = str + card.ep + "\n";
        stat.text = str;

        str = "";
        str = str + "(" + card.card.hp + "+" + (card.hp - card.card.hp) + ") \n";
        str = str + "(" + card.card.ap + "+" + (card.ap - card.card.ap) + ") \n";
        str = str + "(" + card.card.dp + "+" + (card.dp - card.card.dp) + ") \n";
        str = str + "(" + card.card.sp + "+" + (card.sp - card.card.sp) + ") \n";
        str = str + "(" + card.card.cp + "+" + (card.cp - card.card.cp) + ") \n";
        str = str + "(" + card.card.ep + "+" + (card.ep - card.card.ep) + ") \n";
        plus.text = str;

        str = "";
        if (card.ora.sign_hp == 0) str = str + "\n";
        else str = str + "+" +card.ora.value_hp + sign[card.ora.sign_hp] + "\n";
        if (card.ora.sign_ap == 0) str = str + "\n";
        else str = str + "+" + card.ora.value_ap + sign[card.ora.sign_ap] + "\n";
        if (card.ora.sign_dp == 0) str = str + "\n";
        else str = str + "+" + card.ora.value_dp + sign[card.ora.sign_dp] + "\n";
        if (card.ora.sign_sp == 0) str = str + "\n";
        else str = str + "+" + card.ora.value_sp + sign[card.ora.sign_sp] + "\n";
        if (card.ora.sign_cp == 0) str = str + "\n";
        else str = str + "+" + card.ora.value_cp + sign[card.ora.sign_cp] + "\n";
        if (card.ora.sign_ep == 0) str = str +"";
        else str = str + "+" + card.ora.value_ep + sign[card.ora.sign_ep] + "\n";
        ora.text = str;
        
        str = "";
        if (card.item != null) 
        {
            if (card.item.sign_hp == 0) str = str + "\n";
            else str = str + "+" + card.item.value_hp + sign[card.item.sign_hp] + "\n";
            if (card.item.sign_ap == 0) str = str + "\n";
            else str = str + "+" + card.item.value_ap + sign[card.item.sign_ap] + "\n";
            if (card.item.sign_dp == 0) str = str + "\n";
            else str = str + "+" + card.item.value_dp + sign[card.item.sign_dp] + "\n";
            if (card.item.sign_sp == 0) str = str + "\n";
            else str = str + "+" + card.item.value_sp + sign[card.item.sign_sp] + "\n";
            if (card.item.sign_cp == 0) str = str + "\n";
            else str = str + "+" + card.item.value_cp + sign[card.item.sign_cp] + "\n";
            if (card.item.sign_ep == 0) str = str + "";
            else str = str + "+" + card.item.value_ep + sign[card.item.sign_ep] + "\n";
        }
        item.text = str;

        str = "<특수능력>\n";
        for (int i=0; i<card.skill.Count; i++)
        {
            if (card.skill[i].type < 2) str = str + card.skill[i].name + " "+ card.skill[i].value +sign[(card.skill[i].type+1)]+"\n";
            else str = str + card.skill[i].name + "\n";
        }
        skilltree.text = str;

        if (SceneManager.GetActiveScene().name == "Custom")
        {
            if(card.item != null) gameObject.transform.Find("Button1").gameObject.SetActive(true);
            gameObject.transform.Find("Button2").gameObject.SetActive(true);
        }
        if (card.item != null)
        {
            gameObject.transform.Find("ItemIcon").gameObject.SetActive(true);
            gameObject.transform.Find("ItemIcon").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(card.item.type);
        }
    }

    public void ItemBoardSet(Item item)
    {
        itemNum = item.slot;
        img2.sprite = Resources.Load<Sprite>(item.type);
        text1.text = item.type +"종족 전용";
        string str = "";
        string[] sign = { "", "", "%" };
        if (item.sign_hp == 0) str = str + "\n";
        else str = str + "+" + item.value_hp + sign[item.sign_hp] + "\n";
        if (item.sign_ap == 0) str = str + "\n";
        else str = str + "+" + item.value_ap + sign[item.sign_ap] + "\n";
        if (item.sign_dp == 0) str = str + "\n";
        else str = str + "+" + item.value_dp + sign[item.sign_dp] + "\n";
        if (item.sign_sp == 0) str = str + "\n";
        else str = str + "+" + item.value_sp + sign[item.sign_sp] + "\n";
        if (item.sign_cp == 0) str = str + "\n";
        else str = str + "+" + item.value_cp + sign[item.sign_cp] + "\n";
        if (item.sign_ep == 0) str = str + "\n";
        else str = str + "+" + item.value_ep + sign[item.sign_ep] + "\n";
        value.text = str;

        if (SceneManager.GetActiveScene().name == "Custom" && item.occ==false)
        {
            gameObject.transform.Find("Button1").gameObject.SetActive(true);
            gameObject.transform.Find("Button2").gameObject.SetActive(true);
        }
        else if(SceneManager.GetActiveScene().name == "Custom" && item.occ == true)
        {
            gameObject.transform.Find("Use").gameObject.SetActive(true);
            use.text = (item.connectedCard + 1) + "번째 카드에 장착중";
        }
        else
        {
        }
    }
}

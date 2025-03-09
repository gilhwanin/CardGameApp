using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Card
{
    // state : 0-default 1-field
    public string name, species, attribute, image;
    public int hp, ap, dp, sp, cp, ep, rarity;
    public List<Skill> skill = new List<Skill>();

    public Card() { }

    public Card(Card card)
    {
        this.name = card.name;
        this.species = card.species;
        this.attribute = card.attribute;
        this.image = card.image;
        this.hp = card.hp;
        this.ap = card.ap;
        this.dp = card.dp;
        this.sp = card.sp;
        this.cp = card.cp;
        this.ep = card.ep;
        this.rarity = card.rarity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Battle : Card_Unit
{
    public int slot;
    public int chp;
    public bool stun;
    public Card_Battle() {}
    public Card_Battle(Card_Unit card)
    {
        this.skill = card.skill;
        this.slot = 0;
        this.name = card.name;
        this.species = card.species;
        this.attribute = card.attribute;
        this.image = card.image;
        this.hp = card.hp;
        this.chp = card.hp;
        this.ap = card.ap;
        this.dp = card.dp;
        this.sp = card.sp;
        this.cp = card.cp;
        this.ep = card.ep;
        this.rarity = card.rarity;
        this.ora = card.ora;
        this.item = card.item;
        this.stun = false;

    }
    public void Damaged(int damage)
    {
        this.chp -= damage;
    }
}

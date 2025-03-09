[System.Serializable]
public class Card_Unit : Card
{
    public Ora ora;
    public Item item = null;
    public Card card;

    public Card_Unit() { item = null; }

    public Card_Unit(Card card, Ora ora)
    {
        this.card = card;
        this.name = card.name;
        this.species = card.species;
        this.attribute = card.attribute;
        this.image = card.image;
        this.ora = ora;
        this.item = null;
        this.hp = Utils.GetValueWithSIgn(card.hp, ora.value_hp, ora.sign_hp);
        this.ap = Utils.GetValueWithSIgn(card.ap, ora.value_ap, ora.sign_ap);
        this.dp = Utils.GetValueWithSIgn(card.dp, ora.value_dp, ora.sign_dp);
        this.sp = Utils.GetValueWithSIgn(card.sp, ora.value_sp, ora.sign_sp);
        this.cp = Utils.GetValueWithSIgn(card.cp, ora.value_cp, ora.sign_cp);
        this.ep = Utils.GetValueWithSIgn(card.ep, ora.value_ep, ora.sign_ep);
        this.rarity = card.rarity;
        this.skill = card.skill;
    }
    public void EquipItem(Item item)
    {
        this.hp = Utils.GetValueWithSIgn(card.hp, ora.value_hp, ora.sign_hp);
        this.ap = Utils.GetValueWithSIgn(card.ap, ora.value_ap, ora.sign_ap);
        this.dp = Utils.GetValueWithSIgn(card.dp, ora.value_dp, ora.sign_dp);
        this.sp = Utils.GetValueWithSIgn(card.sp, ora.value_sp, ora.sign_sp);
        this.cp = Utils.GetValueWithSIgn(card.cp, ora.value_cp, ora.sign_cp);
        this.ep = Utils.GetValueWithSIgn(card.ep, ora.value_ep, ora.sign_ep);

        this.hp = Utils.GetValueWithSIgn(this.hp, item.value_hp, item.sign_hp);
        this.ap = Utils.GetValueWithSIgn(this.ap, item.value_ap, item.sign_ap);
        this.dp = Utils.GetValueWithSIgn(this.dp, item.value_dp, item.sign_dp);
        this.sp = Utils.GetValueWithSIgn(this.sp, item.value_sp, item.sign_sp);
        this.cp = Utils.GetValueWithSIgn(this.cp, item.value_cp, item.sign_cp);
        this.ep = Utils.GetValueWithSIgn(this.ep, item.value_ep, item.sign_ep);
    }
}

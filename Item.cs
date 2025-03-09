using System;

[System.Serializable]
public class Item
{
    public int value_hp, value_ap, value_dp, value_sp, value_cp, value_ep;
    public int sign_hp, sign_ap, sign_dp, sign_sp, sign_cp, sign_ep;
    public int connectedCard, slot;
    public bool occ;
    public string type;

    public Item()
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int r = rnd.Next(0, 100);
        if (r < 30) { type = "마법"; }
        else if (r < 60) { type = "야수"; }
        else if (r < 90) { type = "인간"; }
        else { type = "전체"; }

        var temp = Utils.GetOraTable();
        if(type == "인간")
        {
            for(int i=0; i<6; i++)
            {
                temp[i] = (int)(temp[i] * 1.3f);
            }
        }

        this.value_hp = temp[0];
        this.value_ap = temp[1];
        this.value_dp = temp[2];
        this.value_sp = temp[3];
        this.value_cp = temp[4];
        this.value_ep = temp[5];
        this.sign_hp = temp[6];
        this.sign_ap = temp[7];
        this.sign_dp = temp[8];
        this.sign_sp = temp[9];
        this.sign_cp = temp[10];
        this.sign_ep = temp[11];
        this.slot = 0;
        this.occ = false;
    }

    public Item(bool b)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int r = rnd.Next(0, 100);
        if (r < 30) { type = "마법"; }
        else if (r < 60) { type = "야수"; }
        else if (r < 90) { type = "인간"; }
        else { type = "전체"; }

        var temp = Utils.GetOraTable();
        this.value_hp = temp[0];
        this.value_ap = temp[1];
        this.value_dp = temp[2];
        this.value_sp = temp[3];
        this.value_cp = temp[4];
        this.value_ep = temp[5];
        this.sign_hp = temp[6];
        this.sign_ap = temp[7];
        this.sign_dp = temp[8];
        this.sign_sp = temp[9];
        this.sign_cp = temp[10];
        this.sign_ep = temp[11];
        this.slot = 0;
        this.occ = false;
    }

    public void HumanUp()
    {
        value_hp = (int)(value_hp * 1.3f);
        value_ap = (int)(value_ap * 1.3f);
        value_dp = (int)(value_dp * 1.3f);
        value_sp = (int)(value_sp * 1.3f);
        value_cp = (int)(value_cp * 1.3f);
        value_ep = (int)(value_ep * 1.3f);
    }
}

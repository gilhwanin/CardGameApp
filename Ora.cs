using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ora
{
    public int value_hp, value_ap, value_dp, value_sp, value_cp, value_ep;
    public int sign_hp, sign_ap, sign_dp, sign_sp, sign_cp, sign_ep;

    public Ora(){
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
    }
}

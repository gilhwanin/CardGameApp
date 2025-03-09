using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchResult : MonoBehaviour
{
    public Text text1, day, reward;

    void Start()
    {
        string str = "";
        reward.text = Utils.leagueName[Utils.league] + "\n" + (Utils.day+1) + "일차 경기결과" + "\n승리보상 : " + ((Utils.league + 1) * 10) + "원";
        str = str + TeamManager.teamListOrigin[Utils.schedule[Utils.day][9]].name + " VS " + TeamManager.teamListOrigin[Utils.schedule[Utils.day][8]].name;
        str = str +"   "+ Battle.recentResult + "\n";
        QuickBattle q = new QuickBattle();
        for (int i=0; i<4; i++)
        {
            int result = q.Fight(Utils.schedule[Utils.day][2 * i], Utils.schedule[Utils.day][2 * i + 1]);
            str = str + TeamManager.teamListOrigin[Utils.schedule[Utils.day][2 * i]].name +" VS " + TeamManager.teamListOrigin[Utils.schedule[Utils.day][2 * i + 1]].name;
            str = str + "   "+QuickBattle.recentResult +"\n";
        }
        text1.text = str;
        Utils.day++;
        Utils.Save();
    }
}

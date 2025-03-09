using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryCards : MonoBehaviour
{
    public GameObject prefab;
    int teamNum;
    public Text text1, text2;
    
    void Start()

    {
        DisplayCard();
    }
    public void DisplayCard()
    {
        teamNum = TeamManager.selectedTeam;
        for (int i=0; i<=2; i++)
        {
            var spawnedCard = Instantiate(prefab);
            Vector3 pos = new Vector3(-500+500*i, 0, 0);
            spawnedCard.transform.position = pos;
            spawnedCard.GetComponent<CardObject>().AssignCardUnit(CardList.instance.playerDeck[teamNum,i]);
        }
        text1.text = TeamManager.teamListOrigin[teamNum].name;
        string str = "";
        str = str + "현재순위 : " + TeamManager.teamListOrigin[teamNum].rank + "\n";
        str = str + "점수 : " + TeamManager.teamListOrigin[teamNum].point + "\n";
        str = str + "승 : " + TeamManager.teamListOrigin[teamNum].win + "\n";
        str = str + "무 : " + TeamManager.teamListOrigin[teamNum].draw + "\n";
        str = str + "패 : " + TeamManager.teamListOrigin[teamNum].defeat + "\n";
        text2.text = str;
    }

}

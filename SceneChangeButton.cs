using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public int n;
    public void GoTo_MatchBoard()
    {
        if (StatBoard.statBoardOn == false)
        {
            SceneManager.LoadScene("MatchBoard");
        }
    }
    public void GoTo_MatchBoardAfterEnding()
    {
        Utils.day = 0;
        CardList.instance.money += 3000;
        Utils.upgradeLeague = true;
        Utils.end = false;
        SceneManager.LoadScene("MatchBoard");
    }
    public void GoTo_MatchBoardWithEnding()
    {
        if (StatBoard.statBoardOn == false)
        {
            if (Utils.end == true) SceneManager.LoadScene("Ending");
            else SceneManager.LoadScene("MatchBoard");
        }
    }
    public void GoTo_MainScreen()
    {
        SceneManager.LoadScene("MainScreen");
    }
    public void GoTo_MatchBoard_Condition()
    {
        if (CardList.instance.myDeck.Count == 6)
        {
            StatBoard.statBoardOn = false;
            SceneManager.LoadScene("MatchBoard");
        }
    }
    public void GoTo_TeamEntry()
    {
        if (StatBoard.statBoardOn == false)
        {
            TeamManager.selectedTeam = n;
            SceneManager.LoadScene("TeamEntry");
        }
    }
    public void GoTo_LockerRoom()
    {
        if (StatBoard.statBoardOn == false)
        {
            SceneManager.LoadScene("LockerRoom");
        }
    }
    public void GoTo_Battle()
    {
        if (StatBoard.statBoardOn == false)
        {
            if (GameObject.Find("Display").GetComponent<Matchup>().entryocc[0] == true &&
                GameObject.Find("Display").GetComponent<Matchup>().entryocc[1] == true &&
                GameObject.Find("Display").GetComponent<Matchup>().entryocc[2] == true)
            {
                SceneManager.LoadScene("Battle");
            }
        }
    }
    public void GoTo_StartingMember()
    {
        SceneManager.LoadScene("StartingMember");
    }
    public void GoTo_TeamGenerate()
    {
        SceneManager.LoadScene("TeamGenerate");
    }
    public void GoTo_RandomCard()
    {
        if (n == 0)
        {
            if (CardList.instance.money >= 50)
            {
                Utils.packNum = n;
                CardList.instance.money -= 50;
                SceneManager.LoadScene("RandomCard");
            }
        }
        if (n == 1)
        {
            if (CardList.instance.money >= 200)
            {
                Utils.packNum = n;
                CardList.instance.money -= 200;
                SceneManager.LoadScene("RandomCard");
            }
        }
        if (n == 2)
        {
            if (CardList.instance.money >= 1000)
            {
                Utils.packNum = n;
                CardList.instance.money -= 1000;
                SceneManager.LoadScene("RandomCard");
            }
        }
    }
    public void GoTo_CardMarket()
    {
        if (StatBoard.statBoardOn == false)
        {
            SceneManager.LoadScene("CardMarket");
        }
    }
    public void GoTo_ItemMarket()
    {
        if (StatBoard.statBoardOn == false)
        {
            SceneManager.LoadScene("ItemMarket");
        }
    }
    public void GoTo_RandomItem()
    {
        if (CardList.instance.money >= 100)
        {
            CardList.instance.money -= 100;
            SceneManager.LoadScene("RandomItem");
        }
    }
    public void GoTo_Custom()
    {
        if (StatBoard.statBoardOn == false)
        {
            SceneManager.LoadScene("Custom");
        }
    }
    public void GoTo_Schedule()
    {
        if (StatBoard.statBoardOn == false)
        {
            SceneManager.LoadScene("Schedule");
        }
    }
    public void GoTo_BattleResult()
    {
        StatBoard.statBoardOn = false;
        SceneManager.LoadScene("BattleResult");

    }
    public void GoTo_Load()
    {
        SceneManager.LoadScene("Load");
    }
    public void GoTo_Description()
    {
        if (StatBoard.statBoardOn == false)
        {
            SceneManager.LoadScene("Description");
        }
    }

    public void GoTo_MatchBoardWithLoad()
    {
        SaveData loadData = SaveSystem.Load("save_001");

        CardList.instance.myDeck = loadData.myDeck;
        for(int i =0; i< CardList.instance.myDeck.Count; i++)
        {
            if (CardList.instance.myDeck[i].item.occ == false)
            {
                CardList.instance.myDeck[i].item = null;
            }
        }
        CardList.instance.myItem = loadData.myItem;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                CardList.instance.playerDeck[i, j] = loadData.playerDeck[3*i + j];
                if(CardList.instance.playerDeck[i, j].item.occ == false)
                {
                    CardList.instance.playerDeck[i, j].item = null;
                }
            }
        }
        CardList.instance.cardocc = loadData.cardocc;
        CardList.instance.money = loadData.money;

        TeamManager.teamListOrigin = loadData.teamListOrigin;
        for (int i = 0; i < 10; i++)
        {
            TeamManager.teamList.Add(TeamManager.teamListOrigin[i]);
        }

            Utils.day = loadData.day;
        Utils.league = loadData.league;
        Utils.upgradeLeague = false;

        SceneManager.LoadScene("MatchBoard");
    }

    public void GoTo_NewLeague()
    {
        if (StatBoard.statBoardOn == false)
        {
            TeamManager.teamList.Sort(delegate (Team x, Team y) { return y.point.CompareTo(x.point); });
            for (int i = 0; i <= 9; i++)
            {
                TeamManager.teamList[i].rank = i;
            }
            if (Utils.day == 18)
            {
                if (TeamManager.teamListOrigin[9].rank < 2)
                {
                    Utils.league++;
                    Utils.upgradeLeague = true;
                    SceneManager.LoadScene("LeagueUp");
                }
                else
                {
                    SceneManager.LoadScene("LeagueEnd");
                }
            }
            else
            {
                SceneManager.LoadScene("MatchBoard");
            }
        }
    }
}

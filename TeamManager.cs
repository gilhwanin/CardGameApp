using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamManager : MonoBehaviour
{
    public Text name1, win, defeat, draw, point, incen;
    public static List<Team> teamList= new List<Team>();
    public static List<Team> teamListOrigin = new List<Team>();
    public static int selectedTeam = 0;
    public static bool incentiveOn = false;
    public Transform t;
    public Button b;
    void Start()
    {
        if (incentiveOn == true && SceneManager.GetActiveScene().name == "MatchBoard")
        {
            StatBoard.statBoardOn = true;
            GameObject.Find("UI").transform.Find("incentive").gameObject.SetActive(true);
            incen.text = Utils.leagueName[Utils.league] + " 시작 지원금\n" + Utils.incentive[Utils.league] + "원이 지급되었습니다.";
            incentiveOn = false;
        }
        if (Utils.league == 5)
        {
            Utils.league = 4;
            Utils.end = true;
        }
        if (SceneManager.GetActiveScene().name == "MatchBoard")
        {
            if (Utils.upgradeLeague == true)
            {
                RandomTeamGenerate();
                CardList.instance.RandomTeamEntry();
                TeamSorting();
                DisplayButton();
                RenewBoard();
                Utils.upgradeLeague = false;
            }
            else
            {
                TeamSorting();
                DisplayButton();
                RenewBoard();
            }

        }
        else
        {
            incentiveOn = true;
            if (Utils.upgradeLeague == true)
            {
                TeamSorting();
                RenewBoard();
                Initialize();
            }
            else
            {
                TeamSorting();
                RenewBoard();
                Weak_Initialize();
            }
        }
        Debug.Log(CardList.instance.bronzeCard.Count);
        Debug.Log(CardList.instance.silverCard.Count);
        Debug.Log(CardList.instance.goldCard.Count);
        Debug.Log(CardList.instance.captainCard.Count);
        Debug.Log(CardList.instance.legendCard.Count);

        Utils.Save();
        SaveSystem.Save(Utils.savedata, "save_001");
    }
    public void Initialize()
    {
        for (int k = 0; k < 10; k++)
        {
            TeamManager.teamList[k].Initialize();
        }
        TeamManager.teamList.Clear();
        TeamManager.teamListOrigin.Clear();
        CardList.instance.RandomTeamEntry();
        RandomTeamGenerate();
        Utils.day = 0;
        Utils.upgradeLeague = false;
    }
    public void Weak_Initialize()
    {
        for (int k = 0; k < 10; k++)
        {
            TeamManager.teamList[k].Initialize();
        }
        Utils.day = 0;
    }
    public void RenewBoard()
    {
        string str = "팀명\n";
        for (int i=0; i<10; i++)
        {
            str = str + teamList[i].name + "\n";
        }
        name1.text = str;

        str = "승\n";
        for (int i = 0; i < 10; i++)
        {
            str = str + teamList[i].win + "\n";
        }
        win.text = str;

        str = "무\n";
        for (int i = 0; i < 10; i++)
        {
            str = str + teamList[i].draw + "\n";
        }
        draw.text = str;

        str = "패\n";
        for (int i = 0; i < 10; i++)
        {
            str = str + teamList[i].defeat + "\n";
        }
        defeat.text = str;

        str = "점수\n";
        for (int i = 0; i < 10; i++)
        {
            str = str + teamList[i].point + "\n";
        }
        point.text = str;
    }

    public void RandomTeamGenerate()
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < 9; i++)
        {
            int size = Random.Range(2, 4);
            var Charsarr = new char[size];
            for (int k = 0; k < Charsarr.Length; k++)
            {
                Charsarr[k] = characters[Random.Range(0, characters.Length)];
            }
            string resultString = new string(Charsarr);
            Team a = new Team(resultString, i);
            teamList.Add(a);
            teamListOrigin.Add(a);
        }
        Team tt = new Team(CardList.instance.myTeamName, 9);
        teamList.Add(tt);
        teamListOrigin.Add(tt);
    }
    public void TeamSorting()
    {
        teamList.Sort(delegate (Team x, Team y) { return y.point.CompareTo(x.point); });
        for (int i = 0; i <= 9; i++)
        {
            TeamManager.teamList[i].rank = i;
        }
    }
    public void DisplayButton()
    {
        for (int i = 0; i < 10; i++)
        {
            if (teamList[i].id != 9)
            {
                var button = Instantiate(b, t);
                button.GetComponent<SceneChangeButton>().n = TeamManager.teamList[i].id;
                Vector3 pos = new Vector3(0, -i*(45*Camera.main.aspect) - Camera.main.aspect*35, 0);
                button.transform.Translate(pos);
            }
        }
    }
}

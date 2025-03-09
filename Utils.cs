using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Utils
{
    public static float natural_e = 2.718f;
    public static List<int[]> oraWeight = new List<int[]>();
    public static List<int[]> schedule = new List<int[]>();
    public static List<string> leagueName = new List<string>();
    public static List<int[]> difficulty = new List<int[]>();
    public static List<string> rarityName = new List<string>();
    public static int[] incentive = { 50, 200, 500, 750, 800 };
    public static int day, league = 0;
    public static bool upgradeLeague = true;
    public static bool end = false;
    public static int packNum, param1, param2;
    public static int[] resultTable = new int[10];
    public static SaveData savedata;
    public static float speed =1;

    static Utils ()
    {
        savedata = new SaveData();

        oraWeight.Add(new int[6] { 5, 40, 10, 5, 85, 10 });
        oraWeight.Add(new int[6] { 2, 12, 5, 2, 85, 7 });
        oraWeight.Add(new int[6] { 1, 9, 10, 1, 80, 4 });
        oraWeight.Add(new int[6] { 2, 11, 5, 2, 82, 6 });
        oraWeight.Add(new int[6] { 10, 35, 20, 10, 90, 25 });
        oraWeight.Add(new int[6] { 10, 35, 20, 10, 90, 25 });

        schedule.Add(new int[10] { 0, 1, 2, 8, 3, 7, 4, 6, 5, 9 });
        schedule.Add(new int[10] { 1, 2, 0 ,3, 4, 8 ,5, 7, 6, 9 });
        schedule.Add(new int[10] { 2, 3, 1, 4, 0, 5, 6, 8, 7, 9 });
        schedule.Add(new int[10] { 3, 4, 2, 5, 1, 6, 0, 7, 8, 9 });
        schedule.Add(new int[10] { 4, 5, 3, 6, 2, 7, 1, 8, 0, 9 });
        schedule.Add(new int[10] { 5, 6 ,4, 7, 3, 8, 0, 2, 1, 9 });
        schedule.Add(new int[10] { 6, 7, 5, 8, 0, 4, 1, 3, 2, 9 });
        schedule.Add(new int[10] { 7, 8, 0, 6, 1, 5, 2, 4, 3, 9 });
        schedule.Add(new int[10] { 0, 8, 1, 7, 2, 6, 3, 5, 4, 9 });
        schedule.Add(new int[10] { 0, 1, 2, 8, 3, 7, 4, 6, 5, 9 });
        schedule.Add(new int[10] { 1, 2, 0, 3, 4, 8, 5, 7, 6, 9 });
        schedule.Add(new int[10] { 2, 3, 1, 4, 0, 5, 6, 8, 7, 9 });
        schedule.Add(new int[10] { 3, 4, 2, 5, 1, 6, 0, 7, 8, 9 });
        schedule.Add(new int[10] { 4, 5, 3, 6, 2, 7, 1, 8, 0, 9 });
        schedule.Add(new int[10] { 5, 6, 4, 7, 3, 8, 0, 2, 1, 9 });
        schedule.Add(new int[10] { 6, 7, 5, 8, 0, 4, 1, 3, 2, 9 });
        schedule.Add(new int[10] { 7, 8, 0, 6, 1, 5, 2, 4, 3, 9 });
        schedule.Add(new int[10] { 0, 8, 1, 7, 2, 6, 3, 5, 4, 9 });

        difficulty.Add(new int[5] { 60, 40, 0, 0, 0});
        difficulty.Add(new int[5] { 25, 40, 35, 0, 0 });
        difficulty.Add(new int[5] { 10, 30, 30, 30, 0 });
        difficulty.Add(new int[5] { 0, 10, 30, 40, 20 });
        difficulty.Add(new int[5] { 0, 0, 10, 40, 50 });

        leagueName.Add("아마추어 리그");
        leagueName.Add("프로 리그");
        leagueName.Add("마스터 리그");
        leagueName.Add("그랜드마스터 리그");
        leagueName.Add("챔피언쉽 리그");

        rarityName.Add("브론즈");
        rarityName.Add("실버");
        rarityName.Add("골드");
        rarityName.Add("캡틴");
        rarityName.Add("레전드");

    }
    public static void Save()
    {
        savedata.myDeck = CardList.instance.myDeck;
        savedata.myItem = CardList.instance.myItem;
        savedata.playerDeck.Clear();
        for (int i = 0; i<9; i++)
        {
            for (int j =0; j<3; j++)
            {
                savedata.playerDeck.Add(CardList.instance.playerDeck[i, j]);            
            }
        }
        savedata.myTeamName = CardList.instance.myTeamName;
        savedata.cardocc = CardList.instance.cardocc;
        savedata.money = CardList.instance.money;
        savedata.teamListOrigin = TeamManager.teamListOrigin;

        savedata.day = Utils.day;
        savedata.league = Utils.league;
    }
    public static bool IsItemEquipped()
    {
        int r = UnityEngine.Random.Range(0, 5);
        if(r<2)
        {
            return true;
        }
        return false;
    }
    public static int GetRarity(int n)
    {
        int result=0;
        int r = UnityEngine.Random.Range(0, 100);

        switch (n)
        {
            case 0:
                if (r < 50) result = 0;
                else if (r < 90) result = 1;
                else result = 2;
                break;
            case 1:
                if (r < 40) result = 1;
                else if (r < 90) result = 2;
                else result = 3;
                break;
            case 2:
                if (r < 75) result = 3;
                else result = 4;
                break;
        }
        return result;
    }
    public static Color GetFrameColor(string attribute)
    {
        Color result = new Color(1, 1, 1, 1);
        switch (attribute)
        {
            case "바다":
                result = new Color(0, 0.45f, 1, 1);
                break;

            case "화염":
                result = new Color(1, 0, 0.3f, 1);
                break;

            case "숲":
                result = new Color(0.219f, 0.519f, 0.283f, 1);
                break;

            case "대지":
                result = new Color(0.47f, 0.26f, 0.25f, 1);
                break;

            case "빛":
                result = new Color(1, 1, 1, 1);
                break;

            case "암흑":
                result = new Color(0.5f, 0, 0.5f, 1);
                break;
        }
        return result;
    }
    public static int AttributeRelation(string str1, string str2)
    {
        switch(str1)
        {
            case "바다":
                if(str2 == "화염") { return 1; }
                if(str2 == "숲") { return -1; }
                break;

            case "화염":
                if (str2 == "숲") { return 1; }
                if (str2 == "바다") { return -1; }
                break;

            case "숲":
                if (str2 == "바다") { return 1; }
                if (str2 == "화염") { return -1; }
                break;

            case "대지":
                if (str2 == "빛") { return 1; }
                if (str2 == "암흑") { return -1; }
                break;

            case "빛":
                if (str2 == "암흑") { return 1; }
                if (str2 == "대지") { return -1; }
                break;

            case "암흑":
                if (str2 == "대지") { return 1; }
                if (str2 == "빛") { return -1; }
                break;
        }
        return 0;
    }
    public static int GetSign()
    {
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int r = rnd.Next(0, 20);
        if (r==0)
        {
            return 2;
        }
        return 1;
    }

    public static int OraFunctionPercentage()
    {
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int r = rnd.Next(20, 60);
        return r;
    }

    public static int[] Random3Int()
    {
        int[] result = { 0, 0, 0, 0, 0, 0};
        List<int> temp = new List<int>();
        temp.Add(0);
        temp.Add(1);
        temp.Add(2);
        temp.Add(3);
        temp.Add(4);
        temp.Add(5);
        for(int i=0; i<3; i++)
        {
            System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int r = rnd.Next(0, temp.Count);
            result[temp[r]] = 1;
            temp.RemoveAt(r);
        }
        return result;
    }

    public static int OraFunction(int min, int max, int x1, int y1, int x2, int y2, int x)
    {
        float result;
        if (x <= x1)
        {
            result = (y1 - min) * (float)x / (float)x1 + min;
        }
        else if(x <= x2)
        {
            result = (y2 - y1) * (float)(x - x1) / (float)(x2 - x1) + y1;
        }
        else
        {
            result = (max - y2) * (float)(x - x2) / (float)(max - x2) + y2;
        }
        return (int)result;
    }
    public static int GetValueWithSIgn(int origin_value, int value, int sign)
    {
        if (sign == 0)
        {
            return origin_value;
        }
        else if (sign == 1)
        {
            return origin_value + value;
        }
        else
        {
            return (int)(  origin_value + (origin_value * (0.01f * value))  );
        }

    }

    public static int[] GetOraTable()
    {
        int[] table = new int[12];
        var select3 = Random3Int();

        for (int i = 0; i < 6; i++)
        {
            System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
            if (select3[i] == 1)
            {
                var sign = GetSign();
                if (sign == 1)
                {
                    table[i] = OraFunction(Utils.oraWeight[i][0], Utils.oraWeight[i][1], Utils.oraWeight[i][2], Utils.oraWeight[i][3], Utils.oraWeight[i][4], Utils.oraWeight[i][5], rnd.Next(0, 100));
                    table[i + 6] = 1;
                }
                else
                {
                    table[i] = OraFunctionPercentage();
                    table[i + 6] = 2;
                }
            }
            else
            {
                table[i] = 0;
                table[i + 6] = 0;
            }
        }
        return table;
    }
}
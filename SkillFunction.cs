using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFunction
{
    public static int n;
    public static Skill  skill;
    public static Card_Battle ca, cd;
    public static CardObject oa, od;
    public static bool isQuick;

    static void SKillVoid(string str)
    {
        switch (str)
        {
            case "맹독":
                Poison();
                break;
            case "흡혈":
                Drain();
                break;
            case "스턴":
                Stunning();
                break;
            case "재생":
                Recovery();
                break;
        }
    }
    static int SkillInt(string str)
    {
        switch (str)
        {
            case "용맹":
                return Brave();
            case "공포":
                return Scaring();
            case "화상":
                return Flare();
            case "공격이용":
                return Mindcontrol();
            case "돌진":
                return Rush();
            case "속공":
                return SpeedAttack();
            case "전자기":
                return Magnet();
            case "장거리":
                return Long();
        }
        return 0;
    }
    public static void Execute(Skill skill, int n, Card_Battle ca, Card_Battle cd, CardObject oa, CardObject od)  // UI배틀에서 StateChange 기술스캔
    {
        SkillFunction.n = n;
        SkillFunction.skill = skill;
        SkillFunction.ca = ca;
        SkillFunction.cd = cd;
        SkillFunction.oa = oa;
        SkillFunction.od = od;
        isQuick = false;
        SKillVoid(skill.name);
    }
    public static int ExecuteInt(Skill skill, int n, Card_Battle ca, Card_Battle cd, CardObject oa, CardObject od)  // UI배틀에서 StateChange 기술스캔
    {
        SkillFunction.n = n;
        SkillFunction.skill = skill;
        SkillFunction.ca = ca;
        SkillFunction.cd = cd;
        SkillFunction.oa = oa;
        SkillFunction.od = od;
        isQuick = false;
        return SkillInt(skill.name);
    }
    public static void Execute_Quick(Skill skill, int n, Card_Battle ca, Card_Battle cd)  // UI배틀에서 StateChange 기술스캔
    {
        SkillFunction.n = n;
        SkillFunction.skill = skill;
        SkillFunction.ca = ca;
        SkillFunction.cd = cd;
        isQuick = true;
        SKillVoid(skill.name);
    }
    public static int ExecuteInt_Quick(Skill skill, int n, Card_Battle ca, Card_Battle cd)  // UI배틀에서 StateChange 기술스캔
    {
        SkillFunction.n = n;
        SkillFunction.skill = skill;
        SkillFunction.ca = ca;
        SkillFunction.cd = cd;
        isQuick = true;
        return SkillInt(skill.name);
    }
    //기술 description
    public static void Drain()
    {
        int v = (int)(SkillFunction.n * (0.01f * SkillFunction.skill.value));
        ca.chp += v;
        if(ca.chp > ca.hp)
        {
            ca.chp = ca.hp;
        }
        if(isQuick==false) oa.slider.value = ca.chp;
    }
    public static void Recovery()
    {
        int v = (int)(ca.hp * (0.01f * SkillFunction.skill.value));
        ca.chp += v;
        if (ca.chp > ca.hp)
        {
            ca.chp = ca.hp;
        }
        if (isQuick == false) oa.slider.value = ca.chp;
    }

    public static int Brave()
    {
        if (ca.rarity < cd.rarity)
        {
            return SkillFunction.skill.value;
        }
        else
        {
            return 0;
        }
    }
    public static int Scaring()
    {
        if (ca.rarity > cd.rarity)
        {
            return SkillFunction.skill.value;
        }
        else
        {
            return 0;
        }
    }
    public static int Magnet()
    {
        if(cd.item != null)
        {
            return skill.value;
        }
        else
        {
            return 0;
        }
    }
    public static int Long()
    {
        int distance = Mathf.Abs(ca.slot % 3 - cd.slot % 3);
        return distance * skill.value;
    }

    public static int Flare()
    {
        return (int)(cd.hp * 0.01f * skill.value);
    }


    public static void Poison()
    {
        System.Random rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
        int r = rnd.Next(0, 100);
        if (r < skill.value)
        {
            if (isQuick == false)
            {
                od.state.sprite = Resources.Load<Sprite>("poison");
                od.state.gameObject.SetActive(true);
                od.stateText.text = "기 절";
                od.GetComponent<CardObject>().Damaged(cd.chp);
            }
            else
            {
                cd.Damaged(cd.chp);
            }
        }
    }
    public static void Stunning()
    {
        System.Random rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
        int r = rnd.Next(0, 100);
        if (r < skill.value)
        {
            if (isQuick == false)
            {
                od.state.sprite = Resources.Load<Sprite>("shock");
                od.state.gameObject.SetActive(true);
                od.stateText.text = "";
                cd.stun = true;
            }
            else
            {
                cd.stun = true;
            }
        }
    }

    public static int Rush()
    {
        return ca.dp;
    }

    public static int Mindcontrol()
    {
        return cd.ap;
    }

    public static int SpeedAttack()
    {
        int result = ca.sp - cd.sp;
        if (result < 0) result = 0;
        return result;
    }
}

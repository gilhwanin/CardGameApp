using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickBattle
{
    public List<Card_Battle> TotalCard = new List<Card_Battle>();
    public List<Card_Battle> Survivor1 = new List<Card_Battle>();
    public List<Card_Battle> Survivor2 = new List<Card_Battle>();
    public List<Card_Battle> Survivor = new List<Card_Battle>();
    public int attackerSlot, defenserSlot, result, turn;
    public static string recentResult;
    public int team1, team2, damage;

    public int Fight(int team1, int team2)   // UI요소를 제거한 Computer끼리의 배틀
    {
        this.team1 = team1;
        this.team2 = team2;
        TotalCard.Clear();
        DisplayMyCard();
        DisplayOpponentCard();
        GameRoutine();
        return result;
    }

    public void DisplayMyCard()
    {
        for (int i = 0; i <= 2; i++)
        {
            Card_Battle card = new Card_Battle(CardList.instance.playerDeck[team1, i]);
            card.slot = i;
            TotalCard.Add(card);
        }
    }
    public void DisplayOpponentCard()
    {
        for (int i = 0; i <= 2; i++)
        {
            Card_Battle card = new Card_Battle(CardList.instance.playerDeck[team2, i]);
            card.slot = i + 3;
            TotalCard.Add(card);
        }
    }
    public void Summon()
    {
        for (int i = 0; i < 6; i++)
        {
            if (TotalCard[i].chp <= 0)
            {
                for (int j = 0; j < TotalCard[i].skill.Count; j++)
                {
                    if (TotalCard[i].skill[j].type > 2)
                    {
                        int ty = TotalCard[i].skill[j].type;
                        TotalCard.RemoveAt(i);
                        CardList.instance.tocken[-3 + ty].item = null;
                        Card_Battle card = new Card_Battle(CardList.instance.tocken[-3 + ty]);
                        card.slot = i;
                        TotalCard.Insert(i, card);
                        break;
                    }
                }
            }
        }
    }
    public void Splash()
    {
        int totalDamage = 0;
        int eachDamage;
        if (TotalCard[attackerSlot].species != "마법")
        {
            eachDamage = TotalCard[attackerSlot].ap - TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 0].dp;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 0].Damaged(eachDamage);

            eachDamage = TotalCard[attackerSlot].ap - TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 1].dp;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 1].Damaged(eachDamage);

            eachDamage = TotalCard[attackerSlot].ap - TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 2].dp;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 2].Damaged(eachDamage);
        }
        else
        {
            eachDamage = TotalCard[attackerSlot].ap;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 0].Damaged(eachDamage);

            eachDamage = TotalCard[attackerSlot].ap;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 1].Damaged(eachDamage);

            eachDamage = TotalCard[attackerSlot].ap;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 2].Damaged(eachDamage);
        }
        damage = totalDamage;
    }

    public int SurvivorRenew()
    {
        Survivor1.Clear();
        Survivor2.Clear();
        Survivor.Clear();

        for (int i = 0; i < TotalCard.Count; i++)
        {
            if (i <= 2)
            {
                if (TotalCard[i].chp > 0)
                {
                    Survivor.Add(TotalCard[i]);
                    Survivor1.Add(TotalCard[i]);
                }
            }
            else
            {
                if (TotalCard[i].chp > 0)
                {
                    Survivor.Add(TotalCard[i]);
                    Survivor2.Add(TotalCard[i]);
                }
            }
        }
        if (Survivor.Count == 0)
        {
            result = 3;
            return 3;
        }
        else
        {
            if (Survivor2.Count == 0)
            {
                result = 1;
                return 1;
            }
            if (Survivor1.Count == 0)
            {
                result = 2;
                return 2;
            }
            return 0;
        }
    }

    public int SelectAttacker()
    {
        int attacker = 0;
        int total = 0;
        List<Card_Battle> NotStunnedCard = new List<Card_Battle>();

        for (int i = 0; i < Survivor.Count; i++)
        {
            if (Survivor[i].stun == false)
            {
                NotStunnedCard.Add(Survivor[i]);
                total += Survivor[i].sp;
            }
            else Survivor[i].stun = false;
        }

        if (NotStunnedCard.Count == 0)
        {
            for (int i = 0; i < Survivor.Count; i++)
            {
                NotStunnedCard.Add(Survivor[i]);
                total += Survivor[i].sp;
            }
        }

        int r = Random.Range(0, total);
        int cumulative = 0;
        for (int i = 0; i < NotStunnedCard.Count; i++)
        {
            cumulative += NotStunnedCard[i].sp;
            if (r < cumulative)
            {
                attacker = NotStunnedCard[i].slot;
                break;
            }
        }
        return attacker;
    }

    public int SelectDefenser()
    {
        int defenser = 0;
        int r;

        if (attackerSlot <= 2)
        {
            for (int i = 0; i < Survivor2.Count; i++)
            {
                for (int j = 0; j < TotalCard[Survivor2[i].slot].skill.Count; j++)
                {
                    if (TotalCard[Survivor2[i].slot].skill[j].name == "호위")
                    {
                        return Survivor2[i].slot;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < Survivor1.Count; i++)
            {
                for (int j = 0; j < TotalCard[Survivor1[i].slot].skill.Count; j++)
                {
                    if (TotalCard[Survivor1[i].slot].skill[j].name == "호위")
                    {
                        return Survivor1[i].slot;
                    }
                }
            }
        }

        if (attackerSlot <= 2)
        {
            r = Random.Range(0, Survivor2.Count);
            defenser = Survivor2[r].slot;
        }
        else
        {
            r = Random.Range(0, Survivor1.Count);
            defenser = Survivor1[r].slot;
        }
        return defenser;
    }

    public bool IsHit()
    {
        if (TotalCard[attackerSlot].species == "야수") return true;
        int r = Random.Range(0, 100);
        if (r >= TotalCard[defenserSlot].ep)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int DealProcess_Basic()
    {
        int deal;
        if (TotalCard[attackerSlot].species != "마법 ") deal = TotalCard[attackerSlot].ap - TotalCard[defenserSlot].dp;
        else deal = TotalCard[attackerSlot].ap;
        if (deal < 0) deal = 0;
        return deal;
    }

    public int DealProcess_Condition()
    {
        int deal = 0;
        for (int i = 0; i < TotalCard[attackerSlot].skill.Count; i++)
        {
            deal = SkillFunction.ExecuteInt_Quick(TotalCard[attackerSlot].skill[i], damage, TotalCard[attackerSlot], TotalCard[defenserSlot]);
        }
        return deal;
    }

    public int DealProcess_Attribution()
    {
        int deal;
        deal = Utils.AttributeRelation(TotalCard[attackerSlot].attribute, TotalCard[defenserSlot].attribute);
        return deal;
    }

    public void DealProcess_StateChange()
    {
        var emptyObject = new CardObject();
        for (int i = 0; i < TotalCard[attackerSlot].skill.Count; i++)
        {
            SkillFunction.Execute_Quick(TotalCard[attackerSlot].skill[i], damage, TotalCard[attackerSlot], TotalCard[defenserSlot]);
        }
        for (int i = 0; i < TotalCard[defenserSlot].skill.Count; i++)
        {
            if (TotalCard[defenserSlot].skill[i].name == "가시갑옷")
            {
                TotalCard[attackerSlot].chp -= TotalCard[defenserSlot].dp;
            }
            if (TotalCard[defenserSlot].skill[i].name == "반격")
            {
                TotalCard[attackerSlot].chp -= TotalCard[defenserSlot].skill[i].value;
            }
        }
    }

    public int DealProcess_Critical()
    {
        int r = Random.Range(0, 100);
        if (r >= TotalCard[attackerSlot].cp)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
    public void DealProcess()
    {
        int alpha = DealProcess_Basic();
        int beta = DealProcess_Condition();
        int gamma = DealProcess_Attribution();
        int delta = DealProcess_Critical();

        float temp = (alpha + beta) * (1 + (0.5f * gamma) + (0.5f * delta));
        damage = (int)temp;
        TotalCard[defenserSlot].Damaged(damage);
    }


    public void Reward()
    {
        if (result == 1)
        {
            TeamManager.teamListOrigin[team2].defeat++;
            TeamManager.teamListOrigin[team1].win++;
            TeamManager.teamListOrigin[team1].point += Survivor1.Count;
            recentResult = Survivor1.Count + " : " + Survivor2.Count;
        }

        else if (result == 2)
        {
            TeamManager.teamListOrigin[team1].defeat++;
            TeamManager.teamListOrigin[team2].win++;
            TeamManager.teamListOrigin[team2].point += Survivor2.Count;
            recentResult = Survivor1.Count + " : " + Survivor2.Count;
        }
        else
        {
            TeamManager.teamListOrigin[team1].draw++;
            TeamManager.teamListOrigin[team2].draw++;
            recentResult = 0 + " : " + 0;
        }

    }

    public void GameRoutine()
    {
        turn = 0;
        while (SurvivorRenew() == 0)
        {
            turn++;
            attackerSlot = SelectAttacker();
            defenserSlot = SelectDefenser();

            for (int i = 0; i < TotalCard[attackerSlot].skill.Count; i++)
            {
                if (TotalCard[attackerSlot].skill[i].name == "광역")
                {
                    Splash();
                    goto Middle;
                }
            }

            if (IsHit() == true)
            {
                DealProcess();
            }
            Middle:
            if (turn >= 50)
            {
                result = 3;
                break;
            }
            Summon();
        }
        Reward();
    }

}

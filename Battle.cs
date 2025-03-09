using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    int teamNum;
    public GameObject prefab;

    public List<Card_Battle> TotalCard = new List<Card_Battle>();
    public List<Card_Battle> Survivor1 = new List<Card_Battle>();
    public List<Card_Battle> Survivor2 = new List<Card_Battle>();
    public List<Card_Battle> Survivor = new List<Card_Battle>();
    public List<GameObject> TotalObject = new List<GameObject>();
    public List<Vector2> ORGpos = new List<Vector2>();
    public int attackerSlot, defenserSlot, result, damage, turn;
    public static string recentResult;
    public Animator anim;
    public GameObject effect;

    void Start()
    {

        DisplayMyCard();
        DisplayOpponentCard();
        SaveOriginPosition();

        StartCoroutine("GameRoutine");
    }

    public void DisplayMyCard() //나의 카드 Object를 표시하고 카드를 할당
    {
        for (int i = 0; i <= 2; i++)
        {
            var spawnedCard = Instantiate(prefab);
            Vector3 pos = new Vector3(-500 + 500 * i, -450, 0);
            spawnedCard.transform.position = pos;
            spawnedCard.GetComponent<CardObject>().AssignCardUnit(CardList.instance.myDeck[CardList.instance.entry[i]]);
            Card_Battle card = new Card_Battle(spawnedCard.GetComponent<CardObject>().card);
            card.slot = i;
            spawnedCard.GetComponent<CardObject>().AssignCardBattle(card);
            TotalCard.Add(card);
            TotalObject.Add(spawnedCard);
        }
    }

    public void DisplayOpponentCard() //상대의 카드 Object를 표시하고 카드를 할당
    {
        teamNum = Utils.schedule[Utils.day][8];
        for (int i = 0; i <= 2; i++)
        {
            var spawnedCard = Instantiate(prefab);
            Vector3 pos = new Vector3(-500 + 500 * i, 450, 0);
            spawnedCard.transform.position = pos;
            spawnedCard.GetComponent<CardObject>().AssignCardUnit(CardList.instance.playerDeck[teamNum, i]);
            Card_Battle card = new Card_Battle(spawnedCard.GetComponent<CardObject>().card);
            card.slot = i + 3;
            spawnedCard.GetComponent<CardObject>().AssignCardBattle(card);
            TotalCard.Add(card);
            TotalObject.Add(spawnedCard);
        }
    }

    public void Summon()
    {
        for (int i=0; i<6; i++)
        {
            if(TotalCard[i].chp <= 0)
            {
                for(int j=0; j<TotalCard[i].skill.Count; j++)
                {
                    if(TotalCard[i].skill[j].type > 2)
                    {
                        int ty = TotalCard[i].skill[j].type;
                        TotalCard.RemoveAt(i);
                        TotalObject[i].SetActive(false);
                        TotalObject.RemoveAt(i);
                        var spawnedCard = Instantiate(prefab);
                        Vector3 pos;
                        if (i < 3) { pos = new Vector3(-500 + 500 * (i % 3), -450, 0); }
                        else { pos = new Vector3(-500 + 500 * (i % 3), 450, 0); }
                        spawnedCard.transform.position = pos;
                        CardList.instance.tocken[-3 + ty].item = null;
                        spawnedCard.GetComponent<CardObject>().AssignCardUnit(CardList.instance.tocken[-3 + ty]);
                        Card_Battle card = new Card_Battle(spawnedCard.GetComponent<CardObject>().card);
                        card.slot = i;
                        spawnedCard.GetComponent<CardObject>().AssignCardBattle(card);
                        TotalCard.Insert(i, card);
                        TotalObject.Insert(i, spawnedCard);
                        break;
                    }
                }
            }
        }
    }
    IEnumerator Splash()
    {
        SoundManager.instance.PlayFx("splash", 3);
        anim.SetInteger("SkillNum", 3);
        int totalDamage = 0;
        int eachDamage;
        if (TotalCard[attackerSlot].species != "마법")
        {
            eachDamage = TotalCard[attackerSlot].ap - TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 0].dp;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            StartCoroutine(TotalObject[3 - ((int)(attackerSlot / 3) * 3) + 0].GetComponent<CardObject>().Damaging(eachDamage));

            eachDamage = TotalCard[attackerSlot].ap - TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 1].dp;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            StartCoroutine(TotalObject[3 - ((int)(attackerSlot / 3) * 3) + 1].GetComponent<CardObject>().Damaging(eachDamage));

            eachDamage = TotalCard[attackerSlot].ap - TotalCard[3 - ((int)(attackerSlot / 3) * 3) + 2].dp;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            yield return StartCoroutine(TotalObject[3 - ((int)(attackerSlot / 3) * 3) + 2].GetComponent<CardObject>().Damaging(eachDamage));
        }
        else 
        {
            eachDamage = TotalCard[attackerSlot].ap;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            StartCoroutine(TotalObject[3 - ((int)(attackerSlot / 3) * 3) + 0].GetComponent<CardObject>().Damaging(eachDamage));

            eachDamage = TotalCard[attackerSlot].ap;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            StartCoroutine(TotalObject[3 - ((int)(attackerSlot / 3) * 3) + 1].GetComponent<CardObject>().Damaging(eachDamage));

            eachDamage = TotalCard[attackerSlot].ap;
            if (eachDamage < 0) eachDamage = 0;
            totalDamage += eachDamage;
            yield return StartCoroutine(TotalObject[3 - ((int)(attackerSlot / 3) * 3) + 2].GetComponent<CardObject>().Damaging(eachDamage));
        }
        damage = totalDamage;
    }

    public void SaveOriginPosition() //공격후 다시 돌아오기 위한 Global Positon을 기억
    {
        for(int i=0; i<TotalCard.Count; i++)
        {
            ORGpos.Add(new Vector2( TotalObject[i].GetComponent<Transform>().position.x, 
                                    TotalObject[i].GetComponent<Transform>().position.y ));
        }
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
            if(r<cumulative)
            {
                attacker = NotStunnedCard[i].slot;
                break;
            }
        }
        return attacker;
    }
    public void RemoveState()
    {
        for (int i = 0; i < TotalCard.Count; i++)
        {
            TotalObject[i].GetComponent<CardObject>().state.gameObject.SetActive(false);
        }
    }
    public int SelectDefenser()
    {
        int defenser = 0;
        int r;

        if(attackerSlot<=2)
        {
            for (int i = 0; i < Survivor2.Count; i++)
            {
                for (int j = 0; j < TotalCard[Survivor2[i].slot].skill.Count; j++)
                {
                    if(TotalCard[Survivor2[i].slot].skill[j].name == "호위")
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

        if (attackerSlot <=2)
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
            SoundManager.instance.PlayFx("miss", 2);
            return false;
        }
    }

    public int DealProcess_Basic()
    {
        int deal;
        if (TotalCard[attackerSlot].species != "마법") deal = TotalCard[attackerSlot].ap - TotalCard[defenserSlot].dp;
        else deal = TotalCard[attackerSlot].ap;
        if (deal < 0) deal = 0;
        return deal;
    }

    public int DealProcess_Condition() // 공격 할 때 적용되는 추가기술
    {
        int deal = 0;
        for (int i = 0; i < TotalCard[attackerSlot].skill.Count; i++)
        {
            deal = deal + SkillFunction.ExecuteInt(TotalCard[attackerSlot].skill[i], damage, TotalCard[attackerSlot], TotalCard[defenserSlot], TotalObject[attackerSlot].GetComponent<CardObject>(), TotalObject[defenserSlot].GetComponent<CardObject>());
        }
        return deal;
    }

    public int DealProcess_Attribution()
    {
        int deal;
        deal = Utils.AttributeRelation(TotalCard[attackerSlot].attribute, TotalCard[defenserSlot].attribute);
        return deal;
    }

    public void DealProcess_StateChange()  // 공격 후에 적용되는 기술 + 가시갑옷
    {
        for (int i = 0; i<TotalCard[attackerSlot].skill.Count; i++)
        {
            SkillFunction.Execute(TotalCard[attackerSlot].skill[i], damage, TotalCard[attackerSlot], TotalCard[defenserSlot], TotalObject[attackerSlot].GetComponent<CardObject>(), TotalObject[defenserSlot].GetComponent<CardObject>());
        }
        for (int i = 0; i < TotalCard[defenserSlot].skill.Count; i++)
        {
            if(TotalCard[defenserSlot].skill[i].name == "가시갑옷")
            {
                TotalObject[attackerSlot].GetComponent<CardObject>().Damaged(TotalCard[defenserSlot].dp);
            }
            if (TotalCard[defenserSlot].skill[i].name == "반격")
            {
                TotalObject[attackerSlot].GetComponent<CardObject>().Damaged(TotalCard[defenserSlot].skill[i].value);
            }
        }
    }

    public int DealProcess_Critical()
    {
        int r = Random.Range(0, 100);
        if (r >= TotalCard[attackerSlot].cp)
        {
            SoundManager.instance.PlayFx("hit", 0);
            anim.SetInteger("SkillNum", 1);
            return 0 ;
        }
        else
        {
            SoundManager.instance.PlayFx("hit_critical", 1);
            anim.SetInteger("SkillNum", 2);
            return 1;
        }
    }

    IEnumerator Move()
    {
        Transform transform1 = TotalObject[attackerSlot].GetComponent<Transform>();
        Transform transform2 = TotalObject[defenserSlot].GetComponent<Transform>();
        float distance = 0.5f * Mathf.Abs((attackerSlot % 3) - (defenserSlot % 3)) + 1;
        TotalObject[attackerSlot].transform.Find("CardCanvas").GetComponent<Canvas>().sortingOrder = 1;

        Vector2 targetPosition = new Vector2(transform2.position.x, (transform1.position.y + transform2.position.y) / 2 );
        while (Mathf.Abs(transform1.position.y - targetPosition.y) > 2f)
        {
            transform1.position = Vector2.MoveTowards(transform1.position, targetPosition, Time.deltaTime * 2000f * distance * Utils.speed);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

    IEnumerator MoveBack()
    {
        Transform transform1 = TotalObject[attackerSlot].GetComponent<Transform>();
        Transform transform2 = TotalObject[defenserSlot].GetComponent<Transform>();
        float distance = 0.5f * Mathf.Abs((attackerSlot % 3) - (defenserSlot % 3)) + 1;
        Vector2 targetPosition = new Vector2(ORGpos[attackerSlot][0], ORGpos[attackerSlot][1]);
        while (Mathf.Abs(transform1.position.y - targetPosition.y) > 2f)
        {
            transform1.position = Vector2.MoveTowards(transform1.position, targetPosition, Time.deltaTime * 2000f * distance * Utils.speed);
            yield return new WaitForSeconds(0.01f);
        }
        TotalObject[attackerSlot].transform.Find("CardCanvas").GetComponent<Canvas>().sortingOrder = 0;
        yield return null;
    }
    IEnumerator DealProcess()
    {
        int alpha = DealProcess_Basic();
        int beta = DealProcess_Condition();
        int gamma = DealProcess_Attribution();
        int delta = DealProcess_Critical();

        float temp = (alpha + beta) * (1 + (0.5f * gamma) + (0.5f * delta));
        damage = (int)temp;

        yield return StartCoroutine(TotalObject[defenserSlot].GetComponent<CardObject>().Damaging(damage));
    }

    public void Reward()  // 경기종료후 관련변수값 수정
    {
        if(result == 1)
        {
            TeamManager.teamListOrigin[teamNum].defeat++;
            TeamManager.teamListOrigin[9].win++;
            TeamManager.teamListOrigin[9].point+=Survivor1.Count;
            Utils.resultTable[9] = Survivor1.Count;
            Utils.resultTable[8] = 0;
            CardList.instance.money += 10 * (1+Utils.league);
            recentResult = Survivor1.Count + " : " + Survivor2.Count;
            GameObject.Find("UI").transform.Find("Victory").gameObject.SetActive(true);
        }
        else if(result == 2)
        {
            TeamManager.teamListOrigin[teamNum].win++;
            TeamManager.teamListOrigin[9].defeat++;
            TeamManager.teamListOrigin[teamNum].point += Survivor2.Count;
            Utils.resultTable[9] = 0;
            Utils.resultTable[8] = Survivor2.Count;
            recentResult = Survivor1.Count + " : " + Survivor2.Count;
            GameObject.Find("UI").transform.Find("Defeat").gameObject.SetActive(true);
        }
        else
        {
            TeamManager.teamListOrigin[teamNum].draw++;
            TeamManager.teamListOrigin[9].draw++;
            Utils.resultTable[9] = 0;
            Utils.resultTable[8] = 0;
            recentResult = 0 + " : " + 0;
            GameObject.Find("UI").transform.Find("Draw").gameObject.SetActive(true);
        }
        StatBoard.statBoardOn = true;
    }

    IEnumerator GameRoutine()
    {
        turn = 0;
        while (SurvivorRenew() == 0)
        {
            turn++;
            attackerSlot = SelectAttacker();
            defenserSlot = SelectDefenser();
            RemoveState();

            yield return StartCoroutine("Move");

            for (int i = 0; i < TotalCard[attackerSlot].skill.Count; i++)
            {
                if (TotalCard[attackerSlot].skill[i].name == "광역")
                {
                    effect.GetComponent<Transform>().position = new Vector3(0, 0, 0);
                    yield return StartCoroutine("Splash");
                    yield return new WaitForSeconds(0.5f);
                    goto Middle;
                }
            }

            if (IsHit() == true)
            {
                effect.GetComponent<Transform>().position = TotalObject[defenserSlot].GetComponent<Transform>().position;
                yield return StartCoroutine("DealProcess");
                DealProcess_StateChange();
                yield return new WaitForSeconds(0.5f);
            }

            Middle:
            yield return StartCoroutine("MoveBack");
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

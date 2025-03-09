using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public InputField input;
    public Button button;
    static bool b = false;
    public Text n1, n2, t1, t2;
    public GameObject prefab;
    private void Start()         //Schedule Scene일 경우 일정 표시
    {
        if (SceneManager.GetActiveScene().name == "Schedule")
        {
            n1.text = "Day-1: \n" +
                          "Day-2: \n" +
                          "Day-3: \n" +
                          "Day-4: \n" +
                          "Day-5: \n";
            n2.text = "Day-6: \n" +
                      "Day-7: \n" +
                      "Day-8: \n" +
                      "Day-9: \n";
            t1.text = TeamManager.teamListOrigin[Utils.schedule[0][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[1][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[2][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[3][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[4][8]].name + "\n";
            t2.text = TeamManager.teamListOrigin[Utils.schedule[5][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[6][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[7][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[8][8]].name + "\n";
            b = false;
            GameObject.Find("UI").transform.Find("Panel").transform.Find("Day").GetComponent<Text>().text = Utils.leagueName[Utils.league] + "\n" + (Utils.day + 1) + " 일차";
        }
    }
    public void LoadScene()   //팀 이름 결정 후 starting 멤버 결정 화면으로
    {
        CardList.instance.myTeamName = input.text;
        SceneManager.LoadScene("StartingMember");
    }

    public void GetCard() //뽑은 카드 가져오기
    {
        if (StatBoard.statBoardOn == false)
        {
            GameObject.Find("EventSystem").GetComponent<RandomCard>().GetCard();
            GameObject.Find("UI").transform.Find("Back").gameObject.SetActive(true);
            Destroy((GameObject)gameObject);
        }
    }

    public void GetItem() //뽑은 아이템 가져오기
    {
        GameObject.Find("EventSystem").GetComponent<ItemDisplay>().GetItem();
        GameObject.Find("UI").transform.Find("Back").gameObject.SetActive(true);
        Destroy((GameObject)gameObject);
    }
    public void RemoveCard() //StatBoard에서 카드버리기
    {
        if(CardList.instance.myDeck.Count>3)
        {
            if (CardList.instance.myDeck[StatBoard.cardNum].item != null)
            {
                CardList.instance.myDeck[StatBoard.cardNum].item.occ = false;
            }
            CardList.instance.myDeck.RemoveAt(StatBoard.cardNum);
            CardList.instance.RenewDeck();
            CardList.instance.RenewInventory();
            StatBoard.statBoardOn = false;
            Destroy(CardObject.temp);
        }
        else
        {
            StatBoard.statBoardOn = true;
            var warning = Instantiate(prefab, GameObject.Find("UI").GetComponent<Transform>());
            warning.transform.Find("Text").GetComponent<Text>().text = "최소 3장 이하의 카드를 가지고 있어야 합니다.";
            warning.transform.Find("Button").GetComponent<WarningButton>().errorName = "Minimum_Card";
        }
        Utils.Save();
    }
    public void Disequip() //StatBoard에서 장비해체
    {
        CardList.instance.myItem[CardList.instance.myDeck[StatBoard.cardNum].item.slot].occ = false;
        CardList.instance.myDeck[StatBoard.cardNum].item = null;
        CardList.instance.RenewDeck();
        CardList.instance.RenewInventory();
        StatBoard.statBoardOn = false;
        Destroy(CardObject.temp);
        Utils.Save();
    }
    public void RemoveItem() //ItemStatBoard에서 아이템 제거
    {
        if (CardList.instance.myItem[StatBoard.itemNum].occ == true)
        {
            CardList.instance.myDeck[CardList.instance.myItem[StatBoard.itemNum].connectedCard].item = null;
        }
        CardList.instance.myItem.RemoveAt(StatBoard.itemNum);
        CardList.instance.RenewDeck();
        CardList.instance.RenewInventory();
        StatBoard.statBoardOn = false;
        Destroy(ItemScript.temp);
        Utils.Save();
    }
    public void UseItem() ////ItemStatBoard에서 아이템 장착할 카드 고르기
    {
        int count = 0;
        for (int i = 0; i < CardList.instance.myDeck.Count; i++)
        {
            if (CardList.instance.myDeck[i].item == null && CardList.instance.myItem[StatBoard.itemNum].occ == false && CardList.instance.myDeck[i].species != "괴물")
            {
                if (CardList.instance.myDeck[i].species == CardList.instance.myItem[StatBoard.itemNum].type || CardList.instance.myItem[StatBoard.itemNum].type == "전체")
                {
                    Destroy(ItemScript.temp);
                    StatBoard.buttons[i] = Instantiate(button, GameObject.Find("UI").GetComponent<Transform>());
                    StatBoard.buttons[i].GetComponent<SelectCard>().n = i;
                    Vector3 pos = new Vector3(-Camera.main.orthographicSize * 1.4f + 330f * i, -Camera.main.orthographicSize * 0.28f, 0);
                    StatBoard.buttons[i].transform.position = pos;
                    count++;
                }              
            }
        }
        if (count == 0)
        {
            var warning = Instantiate(prefab, GameObject.Find("UI").GetComponent<Transform>());
            warning.transform.Find("Text").GetComponent<Text>().text = "장착할 수 있는 유닛이 없습니다.";
            warning.transform.Find("Button").GetComponent<WarningButton>().errorName = "No_Unit_For_Item";
        }
    }
    public void NextPage() //Schedule 일정 다음페이지
    {
        if (b == false)
        {
            n1.text = "Day-10: \n" +
                      "Day-11: \n" +
                      "Day-12: \n" +
                      "Day-13: \n" +
                      "Day-14: \n";
            n2.text = "Day-15: \n" +
                      "Day-16: \n" +
                      "Day-17: \n" +
                      "Day-18: \n";
            t1.text = TeamManager.teamListOrigin[Utils.schedule[0][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[1][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[2][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[3][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[4][8]].name + "\n";
            t2.text = TeamManager.teamListOrigin[Utils.schedule[5][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[6][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[7][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[8][8]].name + "\n";
            b = true;
        }
        else
        {
            if (b == true)
            {
                n1.text = "Day-1: \n" +
                          "Day-2: \n" +
                          "Day-3: \n" +
                          "Day-4: \n" +
                          "Day-5: \n";
                n2.text = "Day-6: \n" +
                          "Day-7: \n" +
                          "Day-8: \n" +
                          "Day-9: \n";
                t1.text = TeamManager.teamListOrigin[Utils.schedule[0][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[1][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[2][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[3][8]].name + "\n" +
                      TeamManager.teamListOrigin[Utils.schedule[4][8]].name + "\n";
                t2.text = TeamManager.teamListOrigin[Utils.schedule[5][8]].name + "\n" +
                          TeamManager.teamListOrigin[Utils.schedule[6][8]].name + "\n" +
                          TeamManager.teamListOrigin[Utils.schedule[7][8]].name + "\n" +
                          TeamManager.teamListOrigin[Utils.schedule[8][8]].name + "\n";
                b = false;
            }
        }
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    public void RemoveIncentive()
    {
        CardList.instance.money += Utils.incentive[Utils.league];
        GameObject.Find("UI").transform.Find("incentive").gameObject.SetActive(false);
        StatBoard.statBoardOn = false;
        Utils.Save();
        SaveSystem.Save(Utils.savedata, "save_001");
    }
}

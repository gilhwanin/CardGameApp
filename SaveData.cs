using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
	public List<Card_Unit> myDeck = new List<Card_Unit>();
	public List<Item> myItem = new List<Item>();
	public List<Card_Unit> playerDeck = new List<Card_Unit>();
	public string myTeamName;
	public bool[] cardocc = new bool[7];
	public int money;

	public List<Team> teamListOrigin = new List<Team>();

	public int day, league = 0;
}

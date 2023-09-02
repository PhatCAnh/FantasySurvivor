using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using Random = UnityEngine.Random;
using FantasySurvivor;
using UnityEngine.Serialization;

public class GameController : Controller<GameApp>
{
	public Monster monsterPrefab;
	
	public bool isStop => isEndGame || isStopGame;

	public bool isStopGame;

	public bool isEndGame;

	public TowerView tower;

	public List<Monster> listMonster = new List<Monster>();

	private void Awake()
	{
		Singleton<GameController>.Set(this);
	}
	private void Start()
	{
		StartGame();
	}

	private void Update()
	{
		// var test = Instantiate(_test);
		// Vector2 random = RandomPointOnCircleEdge(5);
		// Vector2 charPos = character.transform.position;
		// test.position = new Vector2(charPos.x, charPos.y) + random;
	}

	Vector2 RandomPointOnCircleEdge(float radius)
	{
		float angle = Random.Range(0, 2 * Mathf.PI); // Random angle in radians
		float x = radius * Mathf.Cos(angle);
		float y = radius * Mathf.Sin(angle);
		return new Vector2(x, y);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Singleton<GameController>.Unset(this);
	}

	public void ShowChoiceMap()
	{
		//app.resourceManager.ShowPopup(PopupType.ChoiceMap);
	}

	public void StartGame()
	{
		// character = SpawnCharacter(Vector2.zero);
		//monster = SpawnMonster(new Vector2(10, 10));

	}

	public void WinGame()
	{
	}

	public void LoseGame()
	{
	}

	public void RestartGame()
	{
	}

	public void CollectedItem(ItemType type)
	{

	}


	public Monster SpawnMonster()
	{
		Monster monsterIns = Instantiate(monsterPrefab);

		monsterIns.transform.position = RandomPositionSpawnMonster();

		monsterIns.Init(new MonsterModel(0.5f, 20, 10, 1f));

		listMonster.Add(monsterIns);

		return monsterIns;
	}

	public void MonsterDie(Monster mons, bool canDestroy = true)
	{
		listMonster.Remove(mons);
		if(canDestroy) Destroy(mons.gameObject);
	}

	public Monster GetFirstMonster()
	{
		if(listMonster.Count == 0) return null;
		return listMonster.FirstOrDefault(monster => Vector2.Distance(monster.transform.position, tower.transform.position) < tower.model.attackRange);
	}

	public void TowerDie(TowerView towerView)
	{
		Debug.Log($"Lose game");
	}

	private Vector2 RandomPositionSpawnMonster()
	{
		int posX;
		int posY;
		int randomTopDown = Random.Range(0, 2);
		if(randomTopDown == 0)
		{
			posX = Random.Range(-21, 21);
			posY = (Random.Range(0, 2) * 2 - 1) * 20;
		}
		else
		{
			posX = (Random.Range(0, 2) * 2 - 1) * 20;
			posY = Random.Range(-21, 21);
		}
		return new Vector2(posX, posY);
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
	// This is probably the class that is going to determine turn order
	List<GameObject> turnList;
	int turnListCurrentIndex;
	MapManager _mapManager;

	// Use this for initialization
	void Start ()
	{
		turnList = new List<GameObject> ();
		turnListCurrentIndex = 0;
		CreateCharacters ();

		_mapManager = (MapManager)FindObjectOfType (typeof(MapManager));
		
		MouseManager mouse_manager = (MouseManager)FindObjectOfType (typeof(MouseManager));
		mouse_manager.SetCharacterManager (this);

		// This stuff will eventually go into the CreateCharacters method
		GameObject temp = (GameObject)Instantiate(Resources.Load("PlayerCharacterObject"));
		GameObject temp2 = (GameObject)Instantiate(Resources.Load("EnemyCharacterObject"));

		temp.GetComponent<PlayerCharacter> ().CharacterTile = _mapManager.GraphicMap.tileDataMap.GetTile (0);
		temp2.GetComponent<EnemyCharacter> ().CharacterTile = _mapManager.GraphicMap.tileDataMap.GetTile (24);
		temp2.GetComponent<EnemyCharacter> ().Target = temp.GetComponent<PlayerCharacter> ();

		turnList.Add (temp);
		turnList.Add (temp2);

		//temp2.GetComponent<Character> ().BeginTurn ();
	}

	public void SelectedPath(RaycastHit hitInfo)
	{
		if(turnList[turnListCurrentIndex] != null && turnList[turnListCurrentIndex].GetComponent<Character>().FriendFoe == Character.FRIENDFOE.FRIEND)
		{
			if(turnList[turnListCurrentIndex].GetComponent<Character>().CurrentMovement >= turnList[turnListCurrentIndex].GetComponent<Character>().MaxMovement)
			{
				//turnList[turnListCurrentIndex].GetComponent<Character>().GetPathList().Clear();
				_mapManager.UpdateSelectedTile(new List<TDTile>(), turnList[turnListCurrentIndex].GetComponent<Character>().GetPathList());
				return;
			}
			else
			{
				_mapManager.SelectedPath(hitInfo, turnList[turnListCurrentIndex].GetComponent<PlayerCharacter>());
			}
			_mapManager.SelectedPath(hitInfo, turnList[turnListCurrentIndex].GetComponent<PlayerCharacter>());
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!(turnList[turnListCurrentIndex]).GetComponent<Character>().IsCharacterTurn)// && turnList[turnListCurrentIndex].GetComponent<Character>().FriendFoe == Character.FRIENDFOE.FRIEND)
		{
			turnListCurrentIndex++;

			if(turnListCurrentIndex >= turnList.Count)
			{
				turnListCurrentIndex = 0;
			}

			if(turnList[turnListCurrentIndex].GetComponent<Character>().FriendFoe == Character.FRIENDFOE.FRIEND)
			{
				turnList[turnListCurrentIndex].GetComponent<PlayerCharacter>()._pathList.Clear();
			}

			turnList[turnListCurrentIndex].GetComponent<Character>().BeginTurn();
			Debug.Log ("Player " + turnListCurrentIndex + "'s Turn");

			//turnList[turnListCurrentIndex].GetComponent<Character>().BeginTurn();
		}
	}

	void CreateCharacters()
	{
		//turnList [turnListCurrentIndex].GetComponent<PlayerCharacter> ().IsCharacterTurn = true;
	}

	public void Attack()
	{
		Debug.Log ("Attacking");
		turnList [turnListCurrentIndex].GetComponent<Character> ().Attack ();
	}

	public void Skills()
	{
		Debug.Log ("Showing Skills");
	}

	public void Guard()
	{
		Debug.Log ("Guarding");
	}

	public void EndTurn()
	{
		turnList [turnListCurrentIndex].GetComponent<Character> ().EndTurn ();
	}

	public void Move()
	{
		if (!turnList [turnListCurrentIndex].GetComponent<Character> ().Move && 
		    	turnList [turnListCurrentIndex].GetComponent<Character> ().CurrentMovement != turnList [turnListCurrentIndex].GetComponent<Character> ().MaxMovement)
		{
			turnList [turnListCurrentIndex].GetComponent<Character> ().Move = true;
			turnList [turnListCurrentIndex].GetComponent<Character> ().CharacterState = Character.STATE.MOVE;
		}
	}
}

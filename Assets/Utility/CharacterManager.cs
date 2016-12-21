using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    int prevCharacterCurrentTileIndex;
    int currentCharacterIndex;
    List<GameObject> characterList;

	// Use this for initialization
	void Start ()
	{
        characterList = new List<GameObject>();

		GameObject tempPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/Character"));
        tempPlayer.GetComponent<BaseCharacter>().InitializeCharacter(0);
        tempPlayer.GetComponent<BaseCharacter>().BeginTurn();
        characterList.Add(tempPlayer);

        //GameObject tempEnemy = (GameObject)Instantiate(Resources.Load("Prefabs/Character"));    // This needs to be an enemy
        //tempEnemy.GetComponent<BaseCharacter>().InitializeCharacter(24);
        //characterList.Add(tempEnemy);

        currentCharacterIndex = 0;
    }

    // Update is called once per frame
    void Update ()
	{
	    if(!characterList[currentCharacterIndex].GetComponent<BaseCharacter>().IsCharacterTurn)
        {
            currentCharacterIndex++;

            if(currentCharacterIndex >= characterList.Count)
            {
                currentCharacterIndex = 0;
            }

            characterList[currentCharacterIndex].GetComponent<BaseCharacter>().BeginTurn();
        }
	}

    public void SetCharacterPath(List<TDTile> path)
    {
        characterList[currentCharacterIndex].GetComponent<BaseCharacter>().SetPathList(path);
    }

    public int GetCurrentCharacterTileIndex()
    {
        return characterList[currentCharacterIndex].GetComponent<BaseCharacter>().GetCurrentTileIndex();
    }

    public void CharacterAttack()
    {
        characterList[currentCharacterIndex].GetComponent<BaseCharacter>().Attack();
    }

    public void CharacterSkills(Object SkillsButton)
    {
        characterList[currentCharacterIndex].GetComponent<BaseCharacter>().Skills();



    }

    public void CharacterGuard()
    {
        characterList[currentCharacterIndex].GetComponent<BaseCharacter>().Guard();
    }

    public void CharacterEndTurn()
    {
        characterList[currentCharacterIndex].GetComponent<BaseCharacter>().EndTurn();
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
	GameObject loneCharacter;
    int prevCharacterCurrentTileIndex;

	// Use this for initialization
	void Start ()
	{
		loneCharacter = (GameObject)Instantiate(Resources.Load("Prefabs/Character"));
        loneCharacter.GetComponent<BaseCharacter>().InitializeCharacter(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

    public void SetCharacterPath(List<TDTile> path)
    {
        loneCharacter.GetComponent<BaseCharacter>().SetPathList(path);
    }

    public int GetCurrentCharacterTileIndex()
    {
        return loneCharacter.GetComponent<BaseCharacter>().GetCurrentTileIndex();
    }
}

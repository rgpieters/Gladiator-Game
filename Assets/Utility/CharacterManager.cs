using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
    GameObject loneCharacter;

	// Use this for initialization
	void Start ()
    {
        loneCharacter = (GameObject)Instantiate(Resources.Load("Prefabs/Character"));
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}

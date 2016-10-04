using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour, IPointerClickHandler
{
    //TGMap _tileGraphicMap;
    //TDMap _tileDataMap;
    //nt _currentSelectedTile;
    //List<TDTile> prevList;
    //PathSearch pathSearch;
    //PlayerCharacter _playerCharacter;
    //CharacterManager _characterManager;

    PathManager _pathManager;
    float tileSize;
     
	const int LEFT_BUTTON = 0;
	const int RIGHT_BUTTON = 1;
	const int MIDDLE_BUTTON = 2;
	
	// Use this for initialization
	void Start ()
	{
        _pathManager = (PathManager)FindObjectOfType(typeof(PathManager));

        TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
        tileSize = tempMap.tileSize;
	}
	
	// Update is called once per frame
	void Update ()
	{
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
		{
            int x = Mathf.FloorToInt(hitInfo.point.x / tileSize);
       	    int z = Mathf.FloorToInt(hitInfo.point.z / tileSize);

            _pathManager.SetSelectedTile(x, z);
		}
    }

	public void OnPointerClick(PointerEventData eventData)
	{
        //_characterManager.Move ();
	}
}
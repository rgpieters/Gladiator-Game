using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
	PathSearch myPathSearch;
	List<TDTile> pathSolution;
	List<GameObject> pathMarkerList;
	TDTile selectedTileData;
	int selectedTileIndex;
	int prevSelectedTileIndex;
    int prevCharacterileIndex;
	float tileSizeX;
    bool showMarker;
    bool moveCharacter;

    public bool ShowMarker
    {
        get { return showMarker; }
        set { showMarker = value; }
    }

    void Start ()
	{
		selectedTileIndex = 0;

		TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
		tileSizeX = tempMap.size_x;

		myPathSearch = GetComponent<PathSearch>();
		pathSolution = new List<TDTile>();
		pathMarkerList = new List<GameObject>();

        Component selectedTile = transform.FindChild("SelectedTile");
        selectedTile.GetComponent<Renderer>().enabled = false;

        showMarker = false;
        moveCharacter = false;
    }
	
	void Update ()
	{
		TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
        CharacterManager tempCharacterManager = (CharacterManager)FindObjectOfType(typeof(CharacterManager));

		if(prevSelectedTileIndex != selectedTileIndex)
		{
			myPathSearch.AStar(tempMap.tileDataMap.GetTile(tempCharacterManager.GetCurrentCharacterTileIndex()), tempMap.tileDataMap.GetTile(selectedTileIndex));
            pathSolution = myPathSearch.GetSolution();

            tempCharacterManager.SetCharacterPath(pathSolution);

            PathMarkerMaintence();
		}
	}

	void PathMarkerMaintence()
	{
		int loopIterations = 0;

		if(pathMarkerList.Count != 0)
		{
			for(int i = 0; i < pathMarkerList.Count; i++)
			{
				Destroy(pathMarkerList[i]);
			}
		}
		pathMarkerList.Clear();

		if(selectedTileData.IsTraversable)
		{
			loopIterations = pathSolution.Count - 1;
		}
		else
		{
			loopIterations = pathSolution.Count;
		}

		for(int i = 1; i < loopIterations; i++)
		{
			GameObject tempPathMarker = (GameObject)Instantiate(Resources.Load("Prefabs/PathMarker", typeof(GameObject)));
			tempPathMarker.transform.position = new Vector3(pathSolution[i].Pos.x, pathSolution[i].Pos.y + 0.0001f, pathSolution[i].Pos.z);
			pathMarkerList.Add(tempPathMarker);
		}
	}

    public void ToggleMarker()
    {
        showMarker = !showMarker;
    }

    public void ToggleMoveCharacter()
    {
        moveCharacter = !moveCharacter;
    }

	public void SetSelectedTile(int x, int z)
	{
		TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
		Component selectedTile = transform.FindChild("SelectedTile");

        if (!showMarker)
        {
            selectedTile.GetComponent<Renderer>().enabled = false;
            return;
        }
        else
        {
            selectedTile.GetComponent<Renderer>().enabled = true;
        }

		prevSelectedTileIndex = selectedTileIndex;
		selectedTileIndex = (int)(x + (z * tileSizeX));

		selectedTileData = tempMap.tileDataMap.GetTile(selectedTileIndex);
		selectedTile.transform.position = new Vector3(x + 0.5f, tempMap.tileDataMap.GetTile(selectedTileIndex).Pos.y + 0.0001f, z + 0.5f);

		if(selectedTileData.IsTraversable)
		{
			selectedTile.GetComponent<Renderer>().material.color = Color.white;
		}
		else
		{
			selectedTile.GetComponent<Renderer>().material.color = Color.red;
		}
	}

    public void ClearPath()
    {
        for (int i = 0; i < pathMarkerList.Count; i++)
        {
            Destroy(pathMarkerList[i]);
        }
    }
}

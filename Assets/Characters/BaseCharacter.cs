using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : MonoBehaviour
{
	public enum CHARACTER_STATE
	{
		IDLE,
		REST
	}

	public float moveSpeed = 1.0f;
    public float rotSpeed = 10.0f;
	List<TDTile> pathList;  // This was initially public. I don't know why it would need to be
	int pathIndex;
    int currentTileIndex;
    bool isCharacterTurn = false;

    public bool IsCharacterTurn
    {
        get { return isCharacterTurn; }
        set { isCharacterTurn = value; }
    }

	// Use this for initialization
	void Start ()
	{
	
	}

	public void InitializeCharacter(int tileIndex) // TODO: Change to protected when classes are inheriting
	{
        TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
        tempMap.tileDataMap.GetTile(tileIndex).IsTraversable = false;
        transform.position = tempMap.tileDataMap.GetTile(tileIndex).Pos;


        pathList = new List<TDTile>();
		pathIndex = -1;
        currentTileIndex = tileIndex;
    }

	public void SetPathList(List<TDTile> list)
	{
		if (list.Count == 0)
			return;

		pathList.Clear();
		pathList = list;
        pathIndex = 0;
	}

    public int GetCurrentTileIndex()
    {
        return currentTileIndex;
    }

	// Update is called once per frame
	void Update () // TODO: Change to virtual when classes are inheriting
    {
        if(isCharacterTurn)
        {
            if (pathIndex == -1 || pathIndex == pathList.Count)
                return;

		    float step = rotSpeed * Time.deltaTime;
		
		    Vector3 targetDirection = new Vector3(pathList[pathIndex].Pos.x, transform.position.y, pathList[pathIndex].Pos.z) - transform.position; // This was setup with a characterPos variable. Let's see if this actually work for multiple characters
		    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
		    transform.rotation = Quaternion.LookRotation(newDirection);
		
		    step = moveSpeed * Time.deltaTime;
		
            if(transform.position.y != pathList[pathIndex].Pos.y)
            {
                // This works for going up the object but not down. For now, we're going to not worry about his until we have an animation
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, pathList[pathIndex].Pos.y, transform.position.z), step);
                transform.position = Vector3.MoveTowards(transform.position, pathList[pathIndex].Pos, step);
            }
            else
            {
		        transform.position = Vector3.MoveTowards(transform.position, pathList[pathIndex].Pos, step);
            }

		    if (transform.position == pathList[pathIndex].Pos)
		    {
                TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
                tempMap.tileDataMap.GetTile(currentTileIndex).IsTraversable = true;

                pathIndex++;
                if(pathIndex != pathList.Count)
                    currentTileIndex = pathList[pathIndex].Index;

                tempMap.tileDataMap.GetTile(currentTileIndex).IsTraversable = false;
                //_currentMovement++;

                //if (_pathIndex == _pathList.Count || _currentMovement == _maxMovement)
                //{
                //    _characterTile.IsTraversable = true;

                //    if (_pathIndex == _pathList.Count)
                //        _characterTile = _pathList[_pathIndex - 1];
                //    else
                //        _characterTile = _pathList[_pathIndex - 1];

                //    _characterTile.IsTraversable = false;

                //    GetComponent<Animation>().CrossFade("idle");
                //    Move = false;
                //    _characterState = STATE.IDLE;
                //    return;
                //}
            }
        }
	}
}

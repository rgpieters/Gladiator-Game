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


	// Use this for initialization
	void Start ()
	{
	
	}

	public void InitializeCharacter(int tileIndex) // TODO: Change to protected when classes are inheriting
	{
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
        transform.position = new Vector3(list[0].Pos.x, transform.position.y, list[0].Pos.z);
		pathIndex = 0;
	}

    public int GetCurrentTileIndex()
    {
        return currentTileIndex;
    }

	
	// Update is called once per frame
	void Update () // TODO: Change to virtual when classes are inheriting
    {
        if (pathIndex == -1)
            return;

		float step = rotSpeed * Time.deltaTime;
		
		Vector3 targetDirection = pathList[pathIndex].Pos - transform.position; // This was setup with a characterPos variable. Let's see if this actually work for multiple characters
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDirection);
		
		step = moveSpeed * Time.deltaTime;
		
		transform.position = Vector3.MoveTowards(transform.position, pathList[pathIndex].Pos, step);

		if (transform.position == pathList[pathIndex].Pos)
		{
			pathIndex++;
            currentTileIndex = pathList[pathIndex].Index;
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

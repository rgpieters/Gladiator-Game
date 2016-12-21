using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BaseCharacter : MonoBehaviour
{
    public enum CHARACTER_STATE
    {
        IDLE,
        REST
    }

    public float moveSpeed = 1.0f;
    public float rotSpeed = 10.0f;
    protected List<TDTile> pathList;  // This was initially public. I don't know why it would need to be
    protected List<BaseSkill> skillsList;
    protected Sprite characterPortrait;
    protected int pathIndex;
    protected int currentTileIndex;
    protected bool isCharacterTurn = false;
    protected bool moveCharacter = false;
    protected int maxMovement = 4;
    protected int currentMovement = 0;
    protected bool isInMenu = false;

    public bool IsInMenu
    {
        get { return isInMenu; }
        set { isInMenu = value; }
    }

    public bool MoveCharacter
    {
        get { return moveCharacter; }
        set { moveCharacter = value; }
    }

    public bool IsCharacterTurn
    {
        get { return isCharacterTurn; }
        set { isCharacterTurn = value; }
    }

    public List<BaseSkill> SkillsList
    {
        get { return skillsList; }
        set { skillsList = value; }
    }

    // Use this for initialization
    void Start ()
	{
	
	}

	public virtual void InitializeCharacter(int tileIndex) // TODO: Change to protected when classes are inheriting
	{
        TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
        tempMap.tileDataMap.GetTile(tileIndex).IsTraversable = false;
        transform.position = tempMap.tileDataMap.GetTile(tileIndex).Pos;

        pathList = new List<TDTile>();
		pathIndex = -1;
        currentTileIndex = tileIndex;

        skillsList = new List<BaseSkill>();

        BaseSkill tempSkill = new BaseSkill();
        tempSkill.SkillName = "Skill Number 1";
        BaseSkill tempSkill2 = new BaseSkill();
        tempSkill2.SkillName = "Skill Number 2";

        skillsList.Add(tempSkill);
        skillsList.Add(tempSkill2);

        byte[] fileData;

        fileData = File.ReadAllBytes("Assets/Resources/Character Portraits/Human.jpg");
        Texture2D tex = new Texture2D(2,2);
        tex.LoadImage(fileData);

        characterPortrait = Sprite.Create(tex, new Rect(0,0,50,50), new Vector2(0.5f,0.5f));
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

    public virtual void BeginTurn()
    {
        isCharacterTurn = true;
        TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
        tempMap.tileDataMap.GetTile(currentTileIndex).IsTraversable = true;
    }

    public virtual void Attack()
    {
        Debug.Log("Need target to attack.");
    }

    public virtual void Guard()
    {
        Debug.Log("Character is now guarding.");
    }

    public virtual void EndTurn()
    {

        TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
        tempMap.tileDataMap.GetTile(currentTileIndex).IsTraversable = false;

        PathManager tempPathManager = (PathManager)FindObjectOfType(typeof(PathManager));
        tempPathManager.ClearPath();

        moveCharacter = false;
        isCharacterTurn = false;
    }

	// Update is called once per frame
	protected virtual void Update ()
    {
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
        
        if (transform.position == pathList[pathIndex].Pos && !isInMenu)
        {
            //TGMap tempMap = (TGMap)FindObjectOfType(typeof(TGMap));
            //tempMap.tileDataMap.GetTile(currentTileIndex).IsTraversable = true;
        
            pathIndex++;
            if(pathIndex != pathList.Count && isCharacterTurn)
            {
                currentTileIndex = pathList[pathIndex].Index;
            }
            else
            {
                PathManager tempPathManager = (PathManager)FindObjectOfType(typeof(PathManager));
                tempPathManager.ClearPath();
                moveCharacter = false;
            }
        
            //tempMap.tileDataMap.GetTile(currentTileIndex).IsTraversable = false;
        
        
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
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	public enum STATE
	{
		IDLE,
		REST,
		MOVE,
		ATTACK
	}

	public enum FRIENDFOE
	{
		FRIEND,
		FOE
	}
	
	TDTile _characterTile;
	Vector3 _characterPos;
	public List<TDTile> _pathList;
	public float moveSpeed = 1.0f;
	public float rotSpeed = 10.0f;
	bool isCharacterMoving;
	bool _isCharacterTurn;
	int _pathIndex;
	STATE _characterState;
	FRIENDFOE _friendFoe;

	int _maxMovement;
	int _currentMovement;

	public int MaxMovement {
		get {
			return _maxMovement;
		}
		set {
			_maxMovement = value;
		}
	}

	public int CurrentMovement {
		get {
			return _currentMovement;
		}
		set {
			_currentMovement = value;
		}
	}

	public TDTile CharacterTile {
		get {
			return _characterTile;
		}
		set {
			_characterTile = value;
		}
	}

	public bool Move {
		get {
			return isCharacterMoving;
		}
		set {
			isCharacterMoving = value;
		}
	}

	public FRIENDFOE FriendFoe {
		get {
			return _friendFoe;
		}
		set {
			_friendFoe = value;
		}
	}

	public STATE CharacterState {
		get {
			return _characterState;
		}
		set {
			_characterState = value;
		}
	}

	public bool IsCharacterTurn {
		get {
			return _isCharacterTurn;
		}
		set {
			_isCharacterTurn = value;
		}
	}

	// Use this for initialization
	void Start ()
	{

	}

	protected void InitCharacter(FRIENDFOE friendFoe)
	{
		_friendFoe = friendFoe;
		_pathList = new List<TDTile> ();
		_pathIndex = 0;
		
		isCharacterMoving = false;
		transform.position = _characterTile.Pos;
		
		_pathList = new List<TDTile> ();
		_characterState = STATE.REST;
		_characterTile.IsTraversable = false;

		_isCharacterTurn = true;

		_maxMovement = 3;
		_currentMovement = 0;
	}
	
	// Update is called once per frame
	virtual public void Update ()
	{
		if(_isCharacterTurn)
		{
			switch(_characterState)
			{
			case STATE.IDLE:
				if(!GetComponent<Animation>().IsPlaying("idle"))
					GetComponent<Animation>().CrossFade("idle");
				break;

			case STATE.MOVE:
				{	
					float step = rotSpeed * Time.deltaTime;

					if (_pathList.Count > _pathIndex && isCharacterMoving)
					{
						if(!GetComponent<Animation>().IsPlaying("walk"))
							GetComponent<Animation>().CrossFade("walk");

						Vector3 targetDirection = _pathList[_pathIndex].Pos - _characterPos;
						Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
						transform.rotation = Quaternion.LookRotation(newDirection);

						step = moveSpeed * Time.deltaTime;

						transform.position = Vector3.MoveTowards(transform.position, _pathList[_pathIndex].Pos, step);
						_characterPos = transform.position;
						//_characterTile = _pathList[_pathIndex];

						if(transform.position == _pathList[_pathIndex].Pos)
						{
							_pathIndex++;
							_currentMovement++;

							if(_pathIndex == _pathList.Count || _currentMovement == _maxMovement)
							{
								_characterTile.IsTraversable = true;

								if(_pathIndex == _pathList.Count)
									_characterTile = _pathList[_pathIndex-1];
								else
									_characterTile = _pathList[_pathIndex-1];

								_characterTile.IsTraversable = false;

								GetComponent<Animation>().CrossFade("idle");
								Move = false;
								_characterState = STATE.IDLE;
								return;
							}
						}
					}
				}
				break;
			case STATE.ATTACK:
				{
					
				}
				break;
			}
		}
	}
	/*float elapsedTime;
	void Attack()
	{
		elapsedTime += Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Space) 
		    && elapsedTime > (float)_attack._perfectSpotPerIteration[0].GetValue(0) 
		    	&& elapsedTime < (float)_attack._perfectSpotPerIteration[0].GetValue(1))
		{
			Debug.Log("Perfect Hit!");
			elapsedTime = 0.0f;
			_characterState = STATE.IDLE;
		}
		else if (Input.GetKeyDown (KeyCode.Space) 
		    		&& elapsedTime < (float)_attack._perfectSpotPerIteration[0].GetValue(0))
		{
			Debug.Log("Too Early!");
			elapsedTime = 0.0f;
			_characterState = STATE.IDLE;
		}
		else if (Input.GetKeyDown (KeyCode.Space) 
		         && elapsedTime > (float)_attack._perfectSpotPerIteration[0].GetValue(1))
		{
			Debug.Log("Too Late!");
			elapsedTime = 0.0f;
			_characterState = STATE.IDLE;
		}
		else if (elapsedTime > (float)_attack._elapsedTimePerIteration[0])
		{
			Debug.Log("You failed to attack!");
			elapsedTime = 0.0f;
			_characterState = STATE.IDLE;
		}

		//Debug.Log (elapsedTime);
	}*/

	public void SetPathList(List<TDTile> list)
	{
		if(list.Count == 0)
			return;
		_pathList.Clear ();
		_pathList = list;
		_pathIndex = 0;
		_characterPos = _pathList [0].Pos;
		_characterState = STATE.MOVE;
	}

	public List<TDTile> GetPathList()
	{
		return _pathList;
	}

	virtual public void Attack()
	{

	}

	virtual public void BeginTurn()
	{
		Debug.Log ("BeginTurn");
		_isCharacterTurn = true;
	}

	public void EndTurn()
	{
		Debug.Log ("EndTurn");
		_currentMovement = 0;
		_characterState = STATE.IDLE;	// Need Guard state to check against
		_isCharacterTurn = false;
		_pathList.Clear ();
	}
}
using System;
using System.Collections.Generic;

public class TDTile
{
	public enum TYPE
	{
		OCEAN,
		GRASSLAND,
		PLAIN,
		MOUNTAIN,
		SELECTED
	}

	public enum UVS
	{
		BOTTOM_LEFT,
		TOP_LEFT,
		TOP_RIGHT,
		BOTTOM_RIGHT
	}

	int _nHValue; // Heuristic i.e. Distance from current node to target node
	int _nGValue; // Movement Cost
	int _nFValue; // G + H
	TDTile _parentTDTile;

	TYPE _tileType;
	int _index;
	UnityEngine.Vector3 _pos;
	bool _isTraversable;
	public UnityEngine.Vector2[] _tileUVs;
	Dictionary<int, UnityEngine.Vector2[]> UVs;
	int _numTilesPerRow;
	int _numRows;

	#region Mutators
	public TYPE TileType {
		get {
			return _tileType;
		}
		set {
			switch(value)
			{
			case TYPE.OCEAN:
				_isTraversable = false;
				break;
			case TYPE.GRASSLAND:
				_isTraversable = true;
				break;
			case TYPE.PLAIN:
				_isTraversable = true;
				break;
			case TYPE.MOUNTAIN:
				_isTraversable = false;
				break;
			}
			_tileType = value;
		}
	}

	public UnityEngine.Vector2 UV(UVS uv)
	{
		return _tileUVs [(int)uv];
	}

	public UnityEngine.Vector3 Pos {
		get {
			return _pos;
		}
		set {
			_pos = value;
		}
	}

	public int HValue {
		get {
			return _nHValue;
		}
		set {
			_nHValue = value;
		}
	}	

	public int GValue {
		get {
			return _nGValue;
		}
		set {
			_nGValue = value;
		}
	}

	public int FValue {
		get {
			return _nFValue;
		}
		set {
			_nFValue = value;
		}
	}

	public TDTile ParentTDTile {
		get {
			return _parentTDTile;
		}
		set {
			_parentTDTile = value;
		}
	}

	public bool IsTraversable {
		get {
			return _isTraversable;
		}
		set {
			_isTraversable = value;
		}
	}

	public int Index {
		get {
			return _index;
		}
		set {
			_index = value;
		}
	}

	#endregion

	public TDTile()
	{
		_numTilesPerRow = 4;
		_numRows = 2;

		PopulateUVsFromAtlas ();
		_tileType = TYPE.GRASSLAND;
		_index = -1;
		_isTraversable = true;
		_tileUVs = new UnityEngine.Vector2[4];
		_tileUVs [(int)UVS.BOTTOM_LEFT] 	= new UnityEngine.Vector2 (0, 0);
		_tileUVs [(int)UVS.TOP_LEFT] 		= new UnityEngine.Vector2 (0, 1);
		_tileUVs [(int)UVS.TOP_RIGHT] 		= new UnityEngine.Vector2 (1, 1);
		_tileUVs [(int)UVS.BOTTOM_RIGHT] 	= new UnityEngine.Vector2 (1, 0);

		_nHValue = -1;
		_nGValue = 10;
		_parentTDTile = null;
		_pos = new UnityEngine.Vector3 (0, 0, 0);
	}

	public TDTile(TDTile.TYPE type, int index, UnityEngine.Vector3 pos)
	{
		_numTilesPerRow = 4;
		_numRows = 2;

		PopulateUVsFromAtlas ();
		TileType = type;
		_index = index;
		_tileUVs = UVs[(int)type];

		_nHValue = -1;
		_nGValue = 10;
		_parentTDTile = null;

		_pos = pos;
	}

	void PopulateUVsFromAtlas()
	{
		// May be able to do all this dynamically

		UVs = new Dictionary<int, UnityEngine.Vector2[]> ();

		for(int y = 0; y < _numRows; y ++)
		{
			for(int x = 0; x < _numTilesPerRow; x++)
			{
				UnityEngine.Vector2[] tempUVs = new UnityEngine.Vector2[4];

				UnityEngine.Vector2 bl = new UnityEngine.Vector2 ( (float)x / _numTilesPerRow, (float)y / _numRows);
				UnityEngine.Vector2 tl = new UnityEngine.Vector2 ( (float)x / _numTilesPerRow, (float)y + 1 / _numRows);
				UnityEngine.Vector2 tr = new UnityEngine.Vector2 ( (float)(x + 1) / _numTilesPerRow, (float)y + 1 / _numRows);	
				UnityEngine.Vector2 br = new UnityEngine.Vector2 ( (float)(x + 1) / _numTilesPerRow, (float)y / _numRows);

				tempUVs[(int)UVS.BOTTOM_LEFT] 	= bl;
				tempUVs[(int)UVS.TOP_LEFT] 	 	= tl;
				tempUVs[(int)UVS.TOP_RIGHT] 	= tr;
				tempUVs[(int)UVS.BOTTOM_RIGHT]	= br;

				UVs[x + y * _numTilesPerRow] = tempUVs;

				if(x + y * _numTilesPerRow >= Enum.GetNames(typeof(TYPE)).Length)
					break;
			}
		}
	}

	public void TileSelected()
	{
		_tileUVs = UVs [(int)TYPE.SELECTED];
	}

	public void TileUnSelected()
	{
		_tileUVs = UVs [(int)_tileType];
	}
}
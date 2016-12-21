public class TDMap
{
	TDTile[] _tiles;
	int _mapWidth;
	int _mapHeight;
	int _numTiles;
	UnityEngine.Texture2D _texture;
	int _numRowsOnTexture;
	int _numTilesPerRowOnTexture;

	#region Mutators
	public TDTile[] Tiles {
		get {
			return _tiles;
		}
		set {
			_tiles = value;
		}
	}

	public int MapWidth {
		get {
			return _mapWidth;
		}
		set {
			_mapWidth = value;
		}
	}

	public int MapHeight {
		get {
			return _mapHeight;
		}
		set {
			_mapHeight = value;
		}
	}
	#endregion

	public TDMap(int width, int height, UnityEngine.Texture2D texture, int numRows, int numTilesPerRow)
	{
		_mapWidth = width;
		_mapHeight = height;
		_numTiles = _mapWidth * _mapHeight;
		//_texture = texture;
		//_numRowsOnTexture = numRows;
		//_numTilesPerRowOnTexture = numTilesPerRow;

		_tiles = new TDTile[_numTiles];

		BuildMap ();
	}

	public TDTile GetTile(int index)
	{
		if(index < 0 || index >= _numTiles)
			return null;

		return _tiles [index];
	}

	public void UpdateSelectedTile(int currTileIndex, int prevCurrTileIndex)
	{
		_tiles [prevCurrTileIndex].TileUnSelected ();
		_tiles [currTileIndex].TileSelected ();
	}

	public void UpdateSelectList(System.Collections.Generic.List<TDTile> currList, System.Collections.Generic.List<TDTile> prevList)
	{
		/*
		for(int i = 0; i < prevList.Count; i++)
		{
			_tiles [prevList[i].Index].TileUnSelected ();
		}
		*/

		for(int i = 0; i < _tiles.Length; i++)
		{
			_tiles[i].TileUnSelected();
		}

		for(int i = 0; i < currList.Count; i++)
		{
			_tiles [currList[i].Index].TileSelected ();
		}
	}

	void BuildMap()
	{
		for(int y = 0; y < _mapHeight; y++)
		{
			for(int x = 0; x < _mapWidth; x++)
			{
				_tiles[y * _mapWidth + x] = new TDTile(TDTile.TYPE.GRASSLAND, (y * _mapWidth + x), new UnityEngine.Vector3(x + .5f,0, y + .5f));
				/*switch(randomType)
				{
				case 0:
				{
					/*tempUVs[(int)TDTile.UVS.BOTTOM_LEFT]  = new UnityEngine.Vector2(0,0);
					tempUVs[(int)TDTile.UVS.TOP_LEFT] 	 = new UnityEngine.Vector2(0,1);
					tempUVs[(int)TDTile.UVS.TOP_RIGHT]	 = new UnityEngine.Vector2(1,1);
					tempUVs[(int)TDTile.UVS.BOTTOM_RIGHT] = new UnityEngine.Vector2(1,0);

					_tiles[y * _mapWidth + x] = new TDTile(TDTile.TYPE.OCEAN, (y * _mapWidth + x), tempUVs);
					break;
				}
				case (int)TDTile.TYPE.GRASSLAND:
					break;
				case (int)TDTile.TYPE.PLAIN:
					break;
				case (int)TDTile.TYPE.MOUNTAIN:
					break;

				}*/
			}
		}

        // ALWAYS ADD 0.5f TO WHATEVER POSITION YOU MANUALLY PUT IN

		_tiles[10] = new TDTile(TDTile.TYPE.OCEAN, 10, new UnityEngine.Vector3(0.5f,0,10.5f));
		_tiles[11] = new TDTile(TDTile.TYPE.MOUNTAIN, 11, new UnityEngine.Vector3(1.5f,0,11.5f));
		_tiles[12] = new TDTile(TDTile.TYPE.OCEAN, 12, new UnityEngine.Vector3(2.5f,0,12.5f));
		_tiles[13] = new TDTile (TDTile.TYPE.MOUNTAIN, 13, new UnityEngine.Vector3 (3.5f, 0, 13.5f));
		_tiles[14] = new TDTile (TDTile.TYPE.OCEAN, 14, new UnityEngine.Vector3 (4.5f, 0.0f, 2.5f));

        //_tiles[1] = new TDTile(TDTile.TYPE.GRASSLAND, 1, new UnityEngine.Vector3(1.5f, 0.5f, 0.5f));
        //_tiles[2] = new TDTile(TDTile.TYPE.GRASSLAND, 2, new UnityEngine.Vector3(2.5f, 1.0f, 0.5f));
        //_tiles[3] = new TDTile(TDTile.TYPE.GRASSLAND, 3, new UnityEngine.Vector3(3.5f, 0.5f, 0.5f));
    }
}
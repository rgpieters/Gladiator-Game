using UnityEngine;
using System.Collections.Generic;


[ExecuteInEditMode]
[RequireComponent( typeof(MeshFilter))]
[RequireComponent( typeof(MeshRenderer))]
[RequireComponent( typeof(MeshCollider))]
public class TGMap : MonoBehaviour {

	public int size_x = 100; 
	public int size_z = 50;
	public float tileSize = 1.0f;

	public Texture2D terrainTiles;
	public int tileResolution;
	public TDMap tileDataMap;

	int[,] _tileGraphicUVIndex;
	Mesh _tileMesh;

	// Use this for initialization
	void Start ()
	{
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;
		
		tileDataMap = new TDMap (size_x, size_z, terrainTiles, numRows, numTilesPerRow);

        PathSearch tempPathSearch = (PathSearch)FindObjectOfType(typeof(PathSearch));
        tempPathSearch.SetTileNodeList(tileDataMap.Tiles, size_x);

        BuildMesh ();
	}

	public void UpdateSelectedTile(int currTile, int prevTile)
	{
		Vector2[] tempUVs = new Vector2[size_x * 6 * size_z * 6];
		tempUVs = _tileMesh.uv;

		tempUVs[_tileGraphicUVIndex[prevTile, 0]] = tileDataMap.GetTile(prevTile).UV(TDTile.UVS.BOTTOM_LEFT);
		tempUVs[_tileGraphicUVIndex[prevTile, 1]] = tileDataMap.GetTile(prevTile).UV(TDTile.UVS.TOP_LEFT);
		tempUVs[_tileGraphicUVIndex[prevTile, 2]] = tileDataMap.GetTile(prevTile).UV(TDTile.UVS.TOP_RIGHT);
		
		tempUVs[_tileGraphicUVIndex[prevTile, 3]] = tileDataMap.GetTile(prevTile).UV(TDTile.UVS.BOTTOM_LEFT);
		tempUVs[_tileGraphicUVIndex[prevTile, 4]] = tileDataMap.GetTile(prevTile).UV(TDTile.UVS.TOP_RIGHT);
		tempUVs[_tileGraphicUVIndex[prevTile, 5]] = tileDataMap.GetTile(prevTile).UV(TDTile.UVS.BOTTOM_RIGHT);

		tempUVs[_tileGraphicUVIndex[currTile, 0]] = tileDataMap.GetTile(currTile).UV(TDTile.UVS.BOTTOM_LEFT);
		tempUVs[_tileGraphicUVIndex[currTile, 1]] = tileDataMap.GetTile(currTile).UV(TDTile.UVS.TOP_LEFT);
		tempUVs[_tileGraphicUVIndex[currTile, 2]] = tileDataMap.GetTile(currTile).UV(TDTile.UVS.TOP_RIGHT);
		
		tempUVs[_tileGraphicUVIndex[currTile, 3]] = tileDataMap.GetTile(currTile).UV(TDTile.UVS.BOTTOM_LEFT);
		tempUVs[_tileGraphicUVIndex[currTile, 4]] = tileDataMap.GetTile(currTile).UV(TDTile.UVS.TOP_RIGHT);
		tempUVs[_tileGraphicUVIndex[currTile, 5]] = tileDataMap.GetTile(currTile).UV(TDTile.UVS.BOTTOM_RIGHT);

		_tileMesh.uv = tempUVs;
	}	

	public void UpdateSelectList(List<TDTile> currList, List<TDTile> prevList)
	{
		Vector2[] tempUVs = new Vector2[size_x * 6 * size_z * 6];
		tempUVs = _tileMesh.uv;


		/*for(int i = 0; i < prevList.Count; i++)
		{
			tempUVs[_tileGraphicUVIndex[prevList[i].Index, 0]] = tileDataMap.GetTile(prevList[i].Index).UV(TDTile.UVS.BOTTOM_LEFT);
			tempUVs[_tileGraphicUVIndex[prevList[i].Index, 1]] = tileDataMap.GetTile(prevList[i].Index).UV(TDTile.UVS.TOP_LEFT);
			tempUVs[_tileGraphicUVIndex[prevList[i].Index, 2]] = tileDataMap.GetTile(prevList[i].Index).UV(TDTile.UVS.TOP_RIGHT);
			
			tempUVs[_tileGraphicUVIndex[prevList[i].Index, 3]] = tileDataMap.GetTile(prevList[i].Index).UV(TDTile.UVS.BOTTOM_LEFT);
			tempUVs[_tileGraphicUVIndex[prevList[i].Index, 4]] = tileDataMap.GetTile(prevList[i].Index).UV(TDTile.UVS.TOP_RIGHT);
			tempUVs[_tileGraphicUVIndex[prevList[i].Index, 5]] = tileDataMap.GetTile(prevList[i].Index).UV(TDTile.UVS.BOTTOM_RIGHT);
		}*/

		// This isn't too hot. Going to find another way eventually
		for(int i = 0; i < tileDataMap.Tiles.Length; i++)
		{
			tempUVs[_tileGraphicUVIndex[i, 0]] = tileDataMap.GetTile(i).UV(TDTile.UVS.BOTTOM_LEFT);
			tempUVs[_tileGraphicUVIndex[i, 1]] = tileDataMap.GetTile(i).UV(TDTile.UVS.TOP_LEFT);
			tempUVs[_tileGraphicUVIndex[i, 2]] = tileDataMap.GetTile(i).UV(TDTile.UVS.TOP_RIGHT);
			
			tempUVs[_tileGraphicUVIndex[i, 3]] = tileDataMap.GetTile(i).UV(TDTile.UVS.BOTTOM_LEFT);
			tempUVs[_tileGraphicUVIndex[i, 4]] = tileDataMap.GetTile(i).UV(TDTile.UVS.TOP_RIGHT);
			tempUVs[_tileGraphicUVIndex[i, 5]] = tileDataMap.GetTile(i).UV(TDTile.UVS.BOTTOM_RIGHT);	
		}

		for(int i = 0; i < currList.Count; i++)
		{
			tempUVs[_tileGraphicUVIndex[currList[i].Index, 0]] = tileDataMap.GetTile(currList[i].Index).UV(TDTile.UVS.BOTTOM_LEFT);
			tempUVs[_tileGraphicUVIndex[currList[i].Index, 1]] = tileDataMap.GetTile(currList[i].Index).UV(TDTile.UVS.TOP_LEFT);
			tempUVs[_tileGraphicUVIndex[currList[i].Index, 2]] = tileDataMap.GetTile(currList[i].Index).UV(TDTile.UVS.TOP_RIGHT);
			
			tempUVs[_tileGraphicUVIndex[currList[i].Index, 3]] = tileDataMap.GetTile(currList[i].Index).UV(TDTile.UVS.BOTTOM_LEFT);
			tempUVs[_tileGraphicUVIndex[currList[i].Index, 4]] = tileDataMap.GetTile(currList[i].Index).UV(TDTile.UVS.TOP_RIGHT);
			tempUVs[_tileGraphicUVIndex[currList[i].Index, 5]] = tileDataMap.GetTile(currList[i].Index).UV(TDTile.UVS.BOTTOM_RIGHT);			
		}

		_tileMesh.uv = tempUVs;
	}

	public void BuildMesh()
	{
		//int numTilesPerRow = terrainTiles.width / tileResolution;
		//int numRows = terrainTiles.height / tileResolution;

		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;

		int vsize_x = size_x * 6;
		int vsize_z = size_z * 6;
		int numVerts = vsize_x * vsize_z;

		_tileGraphicUVIndex = new int[numTiles,6];

		// Generate mesh data
		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[numTris * 3];

		int x, z;

		for(z = 0; z < size_z; z++)
		{
			for(x = 0; x < vsize_x; x += 6)
			{
				int squareIndex = z * size_x + (x / 6);
				int triOffset = squareIndex * 6;

				// Verts
				vertices[z * vsize_x + x] 	  = new Vector3(x/6, 0, z);
				vertices[z * vsize_x + x + 1] = new Vector3(x/6, 0,z+1);
				vertices[z * vsize_x + x + 2] = new Vector3((x/6)+1, 0,z+1);

				vertices[z * vsize_x + x + 3] = new Vector3(x/6, 0,z);
				vertices[z * vsize_x + x + 4] = new Vector3((x/6)+1, 0,z+1);
				vertices[z * vsize_x + x + 5] = new Vector3((x/6)+1, 0,z);

				// Tris
				triangles[triOffset + 0] = z * vsize_x + x;
				triangles[triOffset + 1] = z * vsize_x + x + 1;
				triangles[triOffset + 2] = z * vsize_x + x + 2;
				
				triangles[triOffset + 3] = z * vsize_x + x + 3;
				triangles[triOffset + 4] = z * vsize_x + x + 4;
				triangles[triOffset + 5] = z * vsize_x + x + 5;

				// Normals
				normals[z * vsize_x + x] 	 = Vector3.up;
				normals[z * vsize_x + x + 1] = Vector3.up;
				normals[z * vsize_x + x + 2] = Vector3.up;

				normals[z * vsize_x + x + 3] = Vector3.up;
				normals[z * vsize_x + x + 4] = Vector3.up;
				normals[z * vsize_x + x + 5] = Vector3.up;

				// Add UVs here
				uv[z * vsize_x + x] 	= tileDataMap.GetTile(squareIndex).UV(TDTile.UVS.BOTTOM_LEFT);
				uv[z * vsize_x + x + 1] = tileDataMap.GetTile(squareIndex).UV(TDTile.UVS.TOP_LEFT);
				uv[z * vsize_x + x + 2] = tileDataMap.GetTile(squareIndex).UV(TDTile.UVS.TOP_RIGHT);
				
				uv[z * vsize_x + x + 3] = tileDataMap.GetTile(squareIndex).UV(TDTile.UVS.BOTTOM_LEFT);
				uv[z * vsize_x + x + 4] = tileDataMap.GetTile(squareIndex).UV(TDTile.UVS.TOP_RIGHT);
				uv[z * vsize_x + x + 5] = tileDataMap.GetTile(squareIndex).UV(TDTile.UVS.BOTTOM_RIGHT);

				_tileGraphicUVIndex[squareIndex, 0] = z * vsize_x + x;
				_tileGraphicUVIndex[squareIndex, 1] = z * vsize_x + x + 1;
				_tileGraphicUVIndex[squareIndex, 2] = z * vsize_x + x + 2;
				_tileGraphicUVIndex[squareIndex, 3] = z * vsize_x + x + 3;
				_tileGraphicUVIndex[squareIndex, 4] = z * vsize_x + x + 4;
				_tileGraphicUVIndex[squareIndex, 5] = z * vsize_x + x + 5;
			}
		}

		// Create new mesh
		_tileMesh = new Mesh ();
		_tileMesh.vertices = vertices;
		_tileMesh.triangles = triangles;
		_tileMesh.normals = normals;
		_tileMesh.uv = uv;

		// Assign mesh to our filter/renderer/collider
		MeshFilter mesh_filter = GetComponent<MeshFilter> ();
		MeshRenderer mesh_render = GetComponent<MeshRenderer> ();
		MeshCollider mesh_collider = GetComponent<MeshCollider> ();

		mesh_filter.mesh = _tileMesh;
		mesh_collider.sharedMesh = _tileMesh;
		mesh_render.sharedMaterial.mainTexture = terrainTiles;
		mesh_render.sharedMaterial.mainTexture.filterMode = FilterMode.Point;
	}
}

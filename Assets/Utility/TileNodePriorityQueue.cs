using UnityEngine;
using System.Collections.Generic;


public class TileNodePriorityQueue
{
	List<TDTile> innerList;

	public TileNodePriorityQueue()
	{
		innerList = new List<TDTile> ();
	}

	public void Engueue(TDTile node)
	{
		if (innerList.Count == 0)
			innerList.Add (node);
		else
		{
			for(int i = 0; i < innerList.Count + 1; ++i)
			{
				if(innerList.Count == i)
				{
					innerList.Add(node);
					break;
				}
				else if(node.FValue >= innerList[i].FValue)
					continue;
				else
				{
					innerList.Insert(i, node);
					break;
				}
			}
		}
	}

	public TDTile Dequeue()
	{
		TDTile temp = innerList [0];
		innerList.RemoveAt (0);
		return temp;
	}

	public void Remove(TDTile node)
	{
		innerList.Remove (node);
	}

	public bool Empty()
	{
		if (innerList.Count == 0)
			return true;
		else
			return false;
	}

	public bool Contains(TDTile node)
	{
		return innerList.Contains (node);
	}

	public TDTile Peek()
	{
		return innerList [0];
	}

	public void Clear()
	{
		innerList.Clear ();
	}
}
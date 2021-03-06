//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using PathFinding.General;
using Test;
using System.Collections.Generic;

//Moves to random locations using path finding
public class TestBot1:MonoBehaviour
{
	Vector2 target;
	PathContainer path;
	bool moving = false;	

	void Start(){
		setNewPath();
	}

	void Update(){
		doPath();
	}

	void setNewPath(){
		int x = 0;
		int y = 0;
		Vector2 currentLocation = Map.convertMapToGridPoint(transform.position);
		do{
			x = UnityEngine.Random.Range(0, Map.Grid.Width);
			y = UnityEngine.Random.Range(0, Map.Grid.Height);
		}while(!Map.Grid.Grid[x,y].Walkable || (x == currentLocation.x && y == currentLocation.y));

		target = new Vector2(x,y);

		path = Map.addDirectedPath((int)currentLocation.x, (int)currentLocation.y, x, y, new TestHeuristic());
	}


	public void doPath(){		
		if (path.Complete && !moving) {
			List<PathNode> builtPath = path.getPath ();

			MoveToTarget movement = (GetComponent ("MoveToTarget") as MoveToTarget);
			movement.setTargetsFromArray (Map.convertGridToMapPath(builtPath));
			moving = true;
		}
		else if (moving){
			moving = !reachedTarget();
			if (!moving){
				setNewPath();
			}
		}
	}

	public bool reachedTarget(){
		bool flag = false;
		Vector2 currentLocation = Map.convertMapToGridPoint(transform.position);
		if (currentLocation == target){
			flag = true;
		}
		return flag;
	}
}
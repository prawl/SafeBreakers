using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;
using Pathfinding;

public class EnemyController : MonoBehaviour {

	public GameObject enemy;
	public TileSystem tileSystem;
	public TileIndex startTile, endTile;
	//The points and tiles to move to
	private Vector3 startPosition, endPosition;
	private Seeker seeker;
	private CharacterController controller;
	//The calculated path
	private Path path;
	//The AI's speed per second
	private float speed = 2;
	//Array of all Vector3 data in A* path
	private Vector3[] pathArray;
	private int currentPos;
	public bool up, down, right, left;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().castShadows = true;
		GetComponent<Renderer>().receiveShadows = true;
		startPosition = tileSystem.GetTile (startTile).gameObject.transform.position;
		startPosition.y = 1f;
		transform.position = startPosition;
		endPosition = tileSystem.GetTile (endTile).gameObject.transform.position;
		endPosition.y = 1f;
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (startPosition, endPosition, OnPathComplete);
	}

	public void GetStartDirection(Vector3 currentPos, Vector3 targetPos){
		if((currentPos.x > targetPos.x) && (currentPos.z == targetPos.z)){
			left = true;
			right = false;
			down = false;
			up = false;
		}
		if((currentPos.x < targetPos.x) && (currentPos.z == targetPos.z)){
			left = false;
			right = true;
			down = false;
			up = false;
		}
		if((currentPos.z > targetPos.z) && (currentPos.x == targetPos.x)){
			left = false;
			right = false;
			down = true;
			up = false;
		}
		if((currentPos.z < targetPos.z) && (currentPos.x == targetPos.x)){
			left = false;
			right = false;
			down = false;
			up = true;
		}
		if((currentPos.x == targetPos.x) && (currentPos.z == targetPos.z)){
			left = false;
			right = false;
			down = false;
			up = false;
		}
	}

	public void OnPathComplete(Path p){
		if(!p.error){
			path = p;
			pathArray = path.vectorPath.ToArray ();
			ChangeYvalue (pathArray);
			GetStartDirection (startPosition , pathArray[currentPos]);
		}
	}

	public void ChangeYvalue(Vector3[] path){
		for(int i = 0; i < path.Length; i++){
			Vector3 temp = path[i];
			temp.y = 1f;
			path[i] = temp;
		}
	}
	public void GetDirection(Vector3 currentPos, Vector3 targetPos){
		if((currentPos.x > targetPos.x) && (currentPos.z == targetPos.z) && !V3Equal (currentPos, targetPos)){
			left = true;
			right = false;
			down = false;
			up = false;
		}
		if((currentPos.x < targetPos.x) && (currentPos.z == targetPos.z) && !V3Equal (currentPos, targetPos)){
			left = false;
			right = true;
			down = false;
			up = false;
		}
		if((currentPos.z > targetPos.z) && (currentPos.x == targetPos.x) && !V3Equal (currentPos, targetPos)){
			left = false;
			right = false;
			down = true;
			up = false;
		}
		if((currentPos.z < targetPos.z) && (currentPos.x == targetPos.x) && !V3Equal (currentPos, targetPos)){
			left = false;
			right = false;
			down = false;
			up = true;
		}
		if(V3Equal (currentPos, targetPos)){
			left = false;
			right = false;
			down = false;
			up = false;
		}
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude (a - b) < 0.0000001;
	}

	// Update is called once per frame
	public void FixedUpdate () {
		if(path == null){
			//We have no path to move after yet
			return;
		}
		if (!V3Equal(pathArray[currentPos], transform.position) && (currentPos < pathArray.Length)) {
			Vector3 dir = (pathArray[currentPos]-transform.position).normalized;
			dir *= Time.fixedDeltaTime * speed;
			controller.Move (dir);
		}
		if(V3Equal(pathArray[currentPos], transform.position)){
			currentPos++;
		}

		if (currentPos == pathArray.Length) {
			seeker.StartPath (endPosition, startPosition, OnPathComplete);
			currentPos = 0;
		}
		GetDirection (transform.position, pathArray[currentPos]);
	}
}

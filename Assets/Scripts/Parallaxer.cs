using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour {

	class PoolObject {
		public Transform transform;
		public GameObject go;
		public bool inUse;
		public PoolObject(GameObject go, Transform t) { 
			this.go = go;
			transform = t; 
		}
		public void Use() { inUse = true; }
		public void Dispose() { inUse = false; }
	}

	[System.Serializable]
	public struct XSpawnRange {
		public float min;
		public float max;
	}

	public GameObject Prefab;
	public int poolSize;
	public float spawnRate;

	public XSpawnRange xSpawnRange;
	public Vector3 defaultSpawnPos;
	public bool spawnImmediate;
	public Vector3 immediateSpawnPos;

	float spawnTimer;
	PoolObject[] poolObjects;
	GameManager game;
	public GameObject[] regulars;
	public GameObject[] vanishers;
	public GameObject[] shifters;
	public GameObject[] rotaters;

	void Awake() {
		Configure();
	}

	// Use this for initialization
	void Start () {
		game = GameManager.Instance;
		regulars = GameObject.FindGameObjectsWithTag("Regular");
		vanishers = GameObject.FindGameObjectsWithTag("Vanisher");
		shifters = GameObject.FindGameObjectsWithTag("Shifter");
		rotaters = GameObject.FindGameObjectsWithTag("Rotater");
	}

	void OnEnable() {
		GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
	}

	void OnDisable() {
		GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
	}

	void OnGameOverConfirmed() {
		for(int i = 0; i < poolObjects.Length; i++){
			poolObjects[i].Dispose();
			poolObjects[i].transform.position = Vector3.one * 1000;
		}

		if(spawnImmediate){
			SpawnImmediate();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(game.GameOver) return;

		Shift();
		spawnTimer += Time.deltaTime;
		if(spawnTimer > spawnRate){
			Spawn();
			spawnTimer = 0;
		}
	}

	void Configure() {
		poolObjects = new PoolObject[poolSize];

		for(int i = 0; i < poolObjects.Length; i++){
			GameObject go = Instantiate(Prefab) as GameObject;
			Transform t = go.transform;
			t.SetParent(transform);
			t.position = Vector3.one * 1000;
			poolObjects[i] = new PoolObject(go, t);
		}

		if(spawnImmediate){
			SpawnImmediate();
		}
	}

	void Spawn() {
		PoolObject po = GetPoolObject();
		if (po == null){
			return;
		}

		Vector3 Pos = Vector3.zero;
		Pos.x = Random.Range(xSpawnRange.min, xSpawnRange.max);
		Pos.y = defaultSpawnPos.y;
		if(CheckHorizontal(regulars, Pos.y) || CheckHorizontal(vanishers, Pos.y) || CheckHorizontal(shifters, Pos.y) || CheckHorizontal(rotaters, Pos.y)){
			Debug.Log("" + po.go.name + "moving up to avoid overlapping");
			Pos.y += 3;
		}
		po.transform.position = Pos;

	}

	void SpawnImmediate() {
		PoolObject po = GetPoolObject();
		if (po == null){
			return;
		}

		Vector3 Pos = Vector3.zero;
		Pos.x = Random.Range(xSpawnRange.min, xSpawnRange.max);
		Pos.y = immediateSpawnPos.y;
		po.transform.position = Pos;

		Spawn();
	}

	void Shift() {
		for(int i = 0; i < poolObjects.Length; i++){
			CheckDisposeObject(poolObjects[i]); 
		}
	}

	void CheckDisposeObject(PoolObject poolObject) {
		if(poolObject.transform.position.y < -defaultSpawnPos.y - 5) {
			poolObject.Dispose();
			poolObject.transform.position = Vector3.one * 1000;
		}
	}

	PoolObject GetPoolObject() {
		for(int i = 0; i < poolObjects.Length; i++){
			if(!poolObjects[i].inUse){
				poolObjects[i].Use();
				return poolObjects[i];
			}
		}
		return null;
	}

	bool CheckHorizontal(GameObject[] gos, float y){
		foreach(GameObject go in gos){
			if(Mathf.Abs(go.transform.position.y - y) <= 3){
				return true;
			}
		}

		return false;
	}

}

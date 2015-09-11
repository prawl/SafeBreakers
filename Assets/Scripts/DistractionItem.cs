using UnityEngine;
using System.Collections;

public class DistractionItem : MonoBehaviour {

  private GameObject player;
  public GameObject deployItem;

	void Initialize() {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Start () {
    Initialize();
    CreateItem();
	}
	
	void Update () {
	
	}

  void OnTriggerEnter(Collider col) {
    print(col.gameObject.name);
  }

  void CreateItem () {
    Instantiate(deployItem);
  }
}

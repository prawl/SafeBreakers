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

  void OnTriggerEnter(Collider other) {
    print("hi");
  }

  void CreateItem () {
    Instantiate(deployItem);
  }
}

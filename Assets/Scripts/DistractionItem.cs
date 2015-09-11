using UnityEngine;
using System.Collections;

public class DistractionItem : MonoBehaviour {

  private GameObject player;
  private Vector3 itemPosition;
  public GameObject deployItem;
  public GameObject itemIsTouchingTile;
  public bool itemExistInGame;

	void Initialize() {
		player = GameObject.FindGameObjectWithTag ("Player");
    itemExistInGame = false;
	}

	void Start () {
    Initialize();
    CreateItem();
	}
	
	void Update () {
	}

  void OnTriggerEnter(Collider col) {
    CaptureTileTouchingItem(col);
  }

  void CaptureTileTouchingItem(Collider col){
    if(col.gameObject.transform.parent.gameObject.name != null){
      itemIsTouchingTile = col.gameObject.transform.parent.gameObject;
    }
  }

  void CreateItem () {
    itemPosition = player.transform.position;
    itemPosition.x += .25f;
    Instantiate(deployItem, itemPosition, player.transform.rotation);
    ActivateItem();
    Invoke("DestroyItem", 3f);
  }

  void DestroyItem() {
    if(ItemActive()){
      Destroy(GameObject.Find("deployment_item(Clone)"));
      DeactivateItem();
    }
  }

  bool ItemActive(){
    return itemExistInGame;
  }

  void ActivateItem(){
    itemExistInGame = true;
  }

  void DeactivateItem(){
    itemExistInGame = false;
  }
}

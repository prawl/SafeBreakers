using UnityEngine;
using System.Collections;

public class TileRepoter : MonoBehaviour {

  public GameObject itemIsTouchingTile;
  private string gameObjectName;

  void OnCollisionEnter(Collision col) {
    if (col.gameObject.transform.parent.transform.parent.name != null) {
      print(col.gameObject.transform.parent.transform.parent.name);
    }

    print(gameObjectName);
    // if(col.gameObject.transform.parent.transform.parent.name == "Ground" && col.gameObject != null){
    // // itemIsTouchingTile = col.gameObject.transform.parent.transform.parent.gameObject;
    //   print("WOOOHOOO");
    // }
    // if(col.gameObject.tag == "Ground" ){
    //   // itemIsTouchingTile = col.gameObject.name;
    //   print(col.gameObject.name);
    // }
  }
}

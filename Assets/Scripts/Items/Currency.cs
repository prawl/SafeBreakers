using UnityEngine;
using System.Collections;

public class Currency : MonoBehaviour {

private static int currency;

  public static int GetCurrency(){
    return currency;
  }

  public static int SetCurrency(int newValue){
    return currency = newValue;
  }
}

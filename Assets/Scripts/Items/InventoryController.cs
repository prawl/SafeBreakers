using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour {

  private static int currencyAmount;

  public static string DisplayCurrency(){
    return Currency.GetCurrency().ToString();
  }

  public static void ResetCurrency(){
    Currency.SetCurrency(0);
  }

  public static void AddCurrency(int amount){
    currencyAmount = Currency.GetCurrency();
    currencyAmount += amount;
    Currency.SetCurrency(currencyAmount);
  }
}

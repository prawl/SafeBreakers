using UnityEngine;
using System.Collections;

 public class PrefabManager : MonoBehaviour
 {
    // Assign the prefab in the inspector
    public GameObject deployItem;
    //Singleton
    private static PrefabManager m_Instance = null;
    public static PrefabManager Instance
    {
       get
       {
          if (m_Instance == null)
          {
             m_Instance = (PrefabManager)FindObjectOfType(typeof(PrefabManager));
          }
          return m_Instance;
       }
    }
 }

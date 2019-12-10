using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else Destroy(this.gameObject);

    }// Start is called before the first frame update
    #endregion
    public string[] var_name;
    public float[] var;
    public string[] switchName;
    public bool[] switchFlags;
    public List<Item> itemList = new List<Item>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

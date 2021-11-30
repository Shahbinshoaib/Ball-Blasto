using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> marbels;

    private int listCount = 10;
    private List<GameObject> marbelList = new List<GameObject>();
    //private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< listCount; i++)
        {
            GameObject marbel = marbels[Random.Range(0, 6)];
            marbelList.Add(marbel);
        }
        SpawnMarbels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AddMarbel(GameObject marbel)
    //{
    //    targetMarbels.Add(marbel);
    //    i--;
    //    SpawnMarbels();
    //    Debug.Log("Marbel Added");
    //    Debug.Log("Total Marbel: " + targetMarbels.Count);
    //}

    void SpawnMarbels()
    {
        for(int i = 0; i < listCount; i++)
        {
            Debug.Log("Making Balls");
            Instantiate(marbelList[i], new Vector3(-0.57f + (i * 1.5f), 0.2f, 9.9f), marbelList[i].transform.rotation);

        }
    }
}

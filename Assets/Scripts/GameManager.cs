using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetMarbels;
    // Start is called before the first frame update
    void Start()
    {
        SpawnMarbels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMarbel(GameObject marbel)
    {
        targetMarbels.Add(marbel);
        SpawnMarbels();
        Debug.Log("Marbel Added");
        Debug.Log("Total Marbel: " + targetMarbels.Count);
    }

    void SpawnMarbels()
    {
        for (int i = 0; i < targetMarbels.Count; i++)
        {
            Instantiate(targetMarbels[i], new Vector3(-0.57f + i, 0.58f, 9.9f), targetMarbels[i].transform.rotation);
        }
    }
}

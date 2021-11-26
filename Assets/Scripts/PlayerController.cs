using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] marbels;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ShootMarbel();
    }

    void ShootMarbel()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomMarbel = Random.Range(0, marbels.Length);
            Instantiate(marbels[randomMarbel], transform.position, marbels[randomMarbel].transform.rotation);
        }
    }
}

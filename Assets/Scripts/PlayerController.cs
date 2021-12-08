using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] marbels;
    private GameManager gameManager;
    public bool isMarbelPlaced;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        isMarbelPlaced = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootMarbel();
        RotateMouse();
   
    }

    void ShootMarbel()
    {
        if (isMarbelPlaced)
        {
            int randomMarbel = Random.Range(0, marbels.Length);
            Instantiate(marbels[randomMarbel], gameObject.transform.position, marbels[randomMarbel].transform.rotation);
            isMarbelPlaced = false;
            gameManager.MakeKinematic(false);
        }
    }

    void RotateMouse()
    {
        transform.LookAt(GetWorldPosition());
    }

    Vector3 GetWorldPosition()
    {
        Vector3 worldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, 1000))
        {
            worldPosition = hitData.point;
            worldPosition.y = 1;

        }
        return worldPosition;
        
    }
}

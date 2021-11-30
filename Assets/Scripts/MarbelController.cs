using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbelController : MonoBehaviour
{
    private Rigidbody marbelRB;
    public GameManager gameManager;
    public float marbelSpeed;
    private bool isMarbelInMotion;

    // Start is called before the first frame update
    void Start()
    {
        marbelRB = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isMarbelInMotion)
        {
            ShootMarbel();
        }
    }

    void ShootMarbel()
    {
        marbelRB.AddForce(Vector3.forward * marbelSpeed * Time.deltaTime, ForceMode.Impulse);

    }

}

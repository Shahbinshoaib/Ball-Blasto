using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbelController : MonoBehaviour
{
    private Rigidbody marbelRB;
    public float marbelSpeed;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        marbelRB = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Player")
        {
            marbelRB.AddForce(Vector3.forward * marbelSpeed * Time.deltaTime, ForceMode.Impulse);
            
        }
        if (collision.gameObject.tag != gameObject.tag && collision.gameObject.tag != "Player")
        {
            Debug.Log("Marbel no match");
            gameManager.AddMarbel(gameObject);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}

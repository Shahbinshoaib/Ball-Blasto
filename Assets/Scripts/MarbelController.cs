using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbelController : Marbel
{
    
    private float marbelSpeed = 20;
    public bool isInitiallyCollided;
    private int collisionTime = 0;
    private Marbel firstMarbel = new Marbel();
    private Marbel secondMarbel = new Marbel();
    private Marbel hittingMarbel = new Marbel();

    private Rigidbody marbelRB;
    private GameObject player;
    private PlayerController playerController;
    public GameManager gameManager;
    public Marbel marbel = new Marbel();


    //public int collisionMatchCount = 0;

    //public Marbel marbel;

    //public bool isInMotion = true;
    //public string color;
    //public int index;
    //public bool isCollidedWithSameTag;


    // Start is called before the first frame update
    void Start()
    {
        marbelRB = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //FOR PLAYER BALL ONLY
        ShootMarbel();
    }

    //FOR PLAYER BALL ONLY
    void ShootMarbel()
    {
        if (Input.GetMouseButtonDown(0) && (isNotInMotion == false))
        {
            marbelRB.AddForce(player.gameObject.transform.forward * marbelSpeed, ForceMode.Impulse);
            isNotInMotion = true;
            StartCoroutine(SetNextMarbel());
            //gameManager.MakeKinematic();
        }
        
    }

    //FOR PLAYER BALL ONLY
    //Delay for setting next ball
    private IEnumerator SetNextMarbel()
    {
        yield return new WaitForSeconds(0.2f);
        //will place a new ball after 0.2 sec
        playerController.isMarbelPlaced = true;
       
    }

    private void OnCollisionEnter(Collision collision)
    {


        //if (!isInitiallyCollided)
        //{
        //    isInitiallyCollided = true; //Setting Collision to null
        //}

        //Runs as many time as in no. of marbels in a combination
        if (!isNotPlayerMarbel && collision.gameObject.tag != "Plane" && collisionTime < 2)
        {
                hittingMarbel.marbelObject = gameObject;
                hittingMarbel.index = index;
                hittingMarbel.color = gameObject.tag;
                hittingMarbel.position = transform.position;
                hittingMarbel.isNotPlayerMarbel = isNotPlayerMarbel;
                


            if (collisionTime == 0)
            {
                    MarbelController marbelController = collision.gameObject.GetComponent<MarbelController>();
                    firstMarbel.marbelObject = marbelController.gameObject;
                    firstMarbel.index = marbelController.index;
                    firstMarbel.position = marbelController.position;
                    firstMarbel.color = marbelController.color;
                    firstMarbel.isNotPlayerMarbel = marbelController.isNotPlayerMarbel;
             


            }
            if(collisionTime == 1)
            {
                    MarbelController marbelController = collision.gameObject.GetComponent<MarbelController>();
                    secondMarbel.marbelObject = marbelController.gameObject;
                    secondMarbel.index = marbelController.index;
                    secondMarbel.position = marbelController.position;
                    secondMarbel.color = marbelController.color;
                    secondMarbel.isNotPlayerMarbel = marbelController.isNotPlayerMarbel;
                    marbelRB.isKinematic = true;

            }
            Debug.Log("FI"+firstMarbel.index);
            Debug.Log("SI"+secondMarbel.index);

            if (firstMarbel.index < secondMarbel.index && collisionTime > 0)
            {
                gameManager.CollisionDetected(firstMarbel, secondMarbel, hittingMarbel);
            }else if(collisionTime > 0)
            {
                gameManager.CollisionDetected(secondMarbel, firstMarbel, hittingMarbel);
            }
            
            collisionTime++;

        
        }

        ////if (collision.gameObject.tag == gameObject.tag && collision.contactCount > collisionMatchCount)
        ////{
        ////    Debug.Log("Destroy " + gameObject.tag);
        ////}

    }

    //public void LockPosition()
    //{
    //    marbelRB.constraints = RigidbodyConstraints.FreezePositionX;
    //    marbelRB.constraints = RigidbodyConstraints.FreezePositionZ;
    //}
}

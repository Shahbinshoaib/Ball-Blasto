using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeRemaining = 180;
    public bool timerIsRunning = false;
    public AudioSource audioSource;
    public AudioClip pikachuVoice;
    public TextMeshProUGUI timerText;
    private int listCount = 100;
    public List<GameObject> marbels; //Given marbels prefabs
    private List<Marbel> marbelsList = new List<Marbel>();
    private MarbelController marbelController;


    //INIT
    void Start()
    {
        timerIsRunning = true;
        GenerateRandomMarbelList();
        //SpawnMarbels();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    //IMPORTANT
    void GenerateRandomMarbelList()
    {
        for (int i = 0; i < listCount; i++)
        {
            //For selecting a random marbel from prefabs
            int randomNum = RanNum();
            //Creating a new marbel instance of Marbel
            Marbel marbel = new Marbel();
      
            //Setting the attributes
            marbel.marbelObject = marbels[randomNum];
            marbel.position = SpawnPos(i);
            marbel.index = i;
            marbel.color = marbels[randomNum].tag;
            marbel.isNotInMotion = true;
            marbel.isNotPlayerMarbel = true;

            //Adding it to Marbel list of Marbel
            marbelsList.Add(marbel);

            //Generating marbels from above list
            marbelController = Instantiate(marbelsList[i].marbelObject, marbel.position, marbelsList[i].marbelObject.transform.rotation).GetComponent<MarbelController>();

            //Assigning every marbel their specific value
            marbelController.marbelObject = marbelsList[i].marbelObject;
            marbelController.position = marbelsList[i].position;
            marbelController.index = marbelsList[i].index;
            marbelController.color = marbelsList[i].color;
            marbelController.isNotInMotion = true;
            marbelController.isNotPlayerMarbel = true;
           //marbelController.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            marbelsList[i].marbelObject = marbelController.gameObject;
        }

    }

    //IMPORTANT
    public void CollisionDetected(Marbel firstMarbel, Marbel secondMarbel, Marbel hittingMarbel)
    {
        MakeKinematic(true);
        List<Marbel> deleteList = new List<Marbel>();
       
        //Condition # 1: If both marbel matches
        if (firstMarbel.color == hittingMarbel.color && secondMarbel.color == hittingMarbel.color)
        {
            
            Debug.Log("Both Matched");
            deleteList.Add(firstMarbel);
            deleteList.Add(secondMarbel);
            deleteList.Add(hittingMarbel);

            //Condition # 1.1: If second have a matching partner to n
            CheckMarbelsOnRight(firstMarbel, secondMarbel, hittingMarbel, deleteList);

            //Condition # 1.2: If first have a matching partner to -n
            CheckMarbelsOnLeft(firstMarbel, secondMarbel, hittingMarbel, deleteList);


        }
        //Condition # 2: If first marbel matches and second does not
        else if (firstMarbel.color == hittingMarbel.color && secondMarbel.color != hittingMarbel.color)
        {
            
            Debug.Log("First Matched");
            //Condition # 2.1: If first have a matching partner to -n
            CheckMarbelsOnLeft(firstMarbel, secondMarbel, hittingMarbel, deleteList);

            if (deleteList.Count < 3)
            {
                AddMarbelsToMainList(firstMarbel, hittingMarbel);
            }


        }
        //Condition # 3: if second marbel matches and first does not
        else if (firstMarbel.color != hittingMarbel.color && secondMarbel.color == hittingMarbel.color)
        {
            Debug.Log("Second Matched");
            //Condition # 3.1: If second have a matching partner to -n
            CheckMarbelsOnRight(firstMarbel, secondMarbel, hittingMarbel, deleteList);

            if (deleteList.Count < 3)
            {
                AddMarbelsToMainList(firstMarbel, hittingMarbel);
            }
        }
        //Condition # 4: If none marbel matches
        else if (firstMarbel.color != hittingMarbel.color && secondMarbel.color != hittingMarbel.color)
        {
            AddMarbelsToMainList(firstMarbel, hittingMarbel);

        }

        //Destroy each item from deletelist
        DestroyMarbels(deleteList);
        
    }

    //IMPORTANT
    void CheckMarbelsOnLeft(Marbel firstMarbel, Marbel secondMarbel, Marbel hittingMarbel, List<Marbel> deleteList)
    {
        for (int i = firstMarbel.index - 1; i >= 0; i--)
        {
            if (marbelsList[i].color == hittingMarbel.color)
            {
                //Add to list
                Debug.Log("Adding from left");
                deleteList.Add(marbelsList[i]);
            }
            else
            {
                i = -1;
            }
        }
    }

    //IMPORTANT
    void CheckMarbelsOnRight(Marbel firstMarbel, Marbel secondMarbel, Marbel hittingMarbel, List<Marbel> deleteList)
    {
        //Condition # 3.1: If second have a matching partner to n
        for (int i = secondMarbel.index + 1; i <= marbelsList.Count; i++)
        {
            if (marbelsList[i].color == hittingMarbel.color)
            {
                //Add to list
                Debug.Log("Adding from right");
                deleteList.Add(marbelsList[i]);
            }
            else
            {
                i = marbelsList.Count + 1;
            }
        }
    }

    //IMPORTANT
    void AddMarbelsToMainList(Marbel firstMarbel, Marbel hittingMarbel)
    {
        //Add to list
        marbelsList.Add(new Marbel());//Added a new empty marbel
        int firstIndex = firstMarbel.index;
        Debug.Log(firstIndex);

        for (int i = marbelsList.Count - 1; i > firstIndex + 1; i--)
        {
            marbelsList[i] = marbelsList[i - 1];
            marbelsList[i].index = i;
            marbelsList[i].marbelObject.GetComponent<MarbelController>().index = i;
            marbelsList[i].position = SpawnPos(i);
            marbelsList[i].marbelObject.GetComponent<MarbelController>().position = SpawnPos(i);
            marbelsList[i].marbelObject.transform.position = SpawnPos(i);
        }

        marbelsList.RemoveAt(firstIndex + 1);//Remove the marbel at hitting position
        marbelsList.Insert(firstIndex + 1, hittingMarbel); //added the hitting marbel
        marbelsList[firstIndex + 1].index = firstIndex + 1;//fixing the index
        marbelsList[firstIndex + 1].marbelObject.transform.position = SpawnPos(firstIndex + 1);
        marbelsList[firstIndex + 1].position = SpawnPos(firstIndex + 1);


        //CHECKING
        for (int i = 0; i < marbelsList.Count; i++)
        {
            Debug.Log(marbelsList[i].index);
        }
    }

    //IMPORTANT
    void DestroyMarbels(List<Marbel> deleteList)
    {
        if (deleteList.Count >= 3)
        {
            for (int i = 0; i < deleteList.Count; i++)
            {
                Debug.Log("Removing..." + deleteList[i].index);
                marbelsList.RemoveAt(deleteList[i].index);
                Destroy(deleteList[i].marbelObject);

            }
            //audioSource.PlayOneShot(pikachuVoice);
        }
    }


    public void MakeKinematic(bool status)
    {
        for (int i = 0; i < marbelsList.Count; i++)
        {
            marbelsList[i].marbelObject.GetComponent<Rigidbody>().isKinematic = status;
        }

    }

    //SUPPORT
    int RanNum()
    {
        int randomNum = Random.Range(0, 6);
        return randomNum;
    }

    //SUPPORT
    Vector3 SpawnPos(int i)
    {
        Vector3 marbelSpawnPos;

        if(i > 100)
        {
            marbelSpawnPos = new Vector3(-110f + i, 0.5f, 0);
        }
        else if(i > 80)
        {
            marbelSpawnPos = new Vector3(-90f + i, 0.5f, -3);
        }
        else if(i > 60)
        {
            marbelSpawnPos = new Vector3(-70f + i, 0.5f, -6);
        }
        else if (i > 40)
        {
            marbelSpawnPos = new Vector3(-50f + i, 0.5f, -9);
        }
        else if (i > 20)
        {
           
            marbelSpawnPos = new Vector3(-30f + i, 0.5f, -12);
        }
        else
        {
            marbelSpawnPos = new Vector3(-10f + i, 0.5f, -15);
        }
        return marbelSpawnPos;
    }
}

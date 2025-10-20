using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectiblesUI : Singleton<CollectiblesUI>
{
    [SerializeField] GameObject inGameUi;
    [SerializeField] TextMeshProUGUI collectibles;

    [SerializeField] int collectedCollectibles;


    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 1)
        {
            inGameUi.SetActive(true);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        /*Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 1)
        {
            inGameUi.SetActive(true);
        }*/
        collectibles.SetText("Collectibles: " + collectedCollectibles);
       
    }

  /*  public void AddCollectiblePoint(int value)
    {
        collectedCollectibles += value;
    }*/

    public void setCollectiblePoints(int value)
    {
        collectedCollectibles = value;
    }

}

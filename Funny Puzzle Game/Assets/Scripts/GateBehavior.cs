using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehavior : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CollectiblesUI.Instance.getPoints == 2)
        {
            NetworkServer.UnSpawn(this.gameObject);
        }
    }
}

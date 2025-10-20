using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MessageHandler : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSendMessage("Waos");
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Collectible>())
        {
            CmdSendMessage(" has collected a collectible");
            //CollectiblesUI.Instance.AddCollectiblePoint(1);
        }
    }


    [Command]
    private void CmdSendMessage(string message)
    {
        RpcSendMessage($"{connectionToClient.connectionId}: {message}");
    }

    [ClientRpc]
    private void RpcSendMessage(string message)
    {
        Debug.Log(message);
    }

    // update collectible
    //[SyncVar]
    void increaseScore(int value)
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Collectible : NetworkBehaviour
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

        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Waos");
        }*/

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        /*if(collision != isLocalPlayer)
        { 
            return; 
        }*/
        //Debug.Log("Collectible Obtained!");
        //CmdSendMessage("has gotten a collectible!");
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            Destroy(this.gameObject);
        }

    }

    /*[Command]
    private void CmdSendMessage(string message)
    {
        RpcSendMessage($"{connectionToClient.connectionId}: {message}");
    }

    [ClientRpc]
    private void RpcSendMessage(string message)
    {
        Debug.Log(message);
    }*/


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    Rigidbody2D rb;

    //Projectile to Break walls
    [SerializeField]
    GameObject projectilePrefab;

    //[SyncVar] [SerializeField] int collectedPickup;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!isLocalPlayer)
        {
            return;
        }
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rb.AddForce(movement);
        //this.transform.position += (Vector3)movement * Time.deltaTime;

        if(Input.GetMouseButtonDown(0))
        {
            CmdSpawnProjectile();
        }

    }

    [Command]
    void CmdSpawnProjectile()
    {
        //RpcSpawnProjectile();
        GameObject obj = Instantiate(projectilePrefab, this.transform.position + new Vector3(0, 2.0f, 0), Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.value, Random.value, 0).normalized * 10;
        //RpcSpawnProjectile(obj);
        NetworkServer.Spawn(obj, connectionToClient);

    }

    [ClientRpc]
    void RpcSpawnProjectile(GameObject obj)
    {
        obj.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.value, Random.value, 0).normalized * 10;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collectible>())
        {
            if(!isLocalPlayer)
            {  return; }
            else 
            {
                //collectedPickup++;
                //CmdUpdatePickup(collectedPickup);
                CmdUpdatePickup(1);
            }
        }
    }

    [Command]
    void CmdUpdatePickup(int value)
    {
        RpcUpdatePickup(value);
    }
    [ClientRpc]
    void RpcUpdatePickup(int value)
    {
        CollectiblesUI.Instance.AddCollectiblePoint(value);
        Debug.Log("collected");
    }
}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleport : NetworkBehaviour
{
    [Scene, SerializeField]
    private string sceneToTeleportTo;

    [SerializeField]
    private string spawnName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer) return;

        if(collision.GetComponent<PlayerController>())
        {
            Debug.Log("Waos it works");
            StartCoroutine(SendPlayer(collision.gameObject));
        }
    }


    [ServerCallback]
    IEnumerator SendPlayer(GameObject player)
    {
        NetworkIdentity identity = null;
        identity = player.GetComponent<NetworkIdentity>();
        if (identity == null)
            yield break;

        NetworkConnectionToClient conn = identity.connectionToClient;
        if (conn == null) yield break;

        //disconnect player
        conn.Send(new SceneMessage { sceneName = this.gameObject.scene.path, sceneOperation = SceneOperation.UnloadAdditive, customHandling = true });
        NetworkServer.RemovePlayerForConnection(conn, RemovePlayerOptions.KeepActive);

        //send player to new scene
        conn.Send(new SceneMessage { sceneName = sceneToTeleportTo, sceneOperation = SceneOperation.LoadAdditive, customHandling = true });
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByPath(sceneToTeleportTo));

        NetworkStartPosition[] positions =  GameObject.FindObjectsOfType<NetworkStartPosition>();
        Vector3 position = Vector3.zero;
        foreach(NetworkStartPosition pos in positions)
        {
            if(pos.gameObject.scene.path == sceneToTeleportTo && pos.gameObject.name == spawnName)
            {
                position = pos.gameObject.transform.position;
                break;
            }
        }


        player.transform.position = position;

        yield return new WaitForEndOfFrame();
        NetworkServer.AddPlayerForConnection(conn, player);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

}

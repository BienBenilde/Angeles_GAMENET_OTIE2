using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerReadyState : MonoBehaviour
{

    PuzzleGameNetworkRoomPlayer _roomPLayer;
    [SerializeField]
    TextMeshProUGUI nameText;

    [SerializeField]
    TextMeshProUGUI readyText;


    public PuzzleGameNetworkRoomPlayer RoomPlayer => _roomPLayer;


    // Start is called before the first frame update
    void Start()
    {
        SetReady(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoomPlayer(PuzzleGameNetworkRoomPlayer roomPLayer)
    {
        _roomPLayer = roomPLayer;
        _roomPLayer.OnChangeReady += SetReady;
        _roomPLayer.OnChangeName += SetName;
        _roomPLayer.OnDisconnected += DestroyState;
    }

    public void DestroyState()
    {
        Destroy(this);
    }

    public void SetName(string playerName)
    {
        nameText.text = playerName;
    }

    public void SetReady(bool isReady)
    {
        readyText.text = isReady ? "Ready" : "Not Ready";
        readyText.color = isReady ? Color.green : Color.red;
    }
}

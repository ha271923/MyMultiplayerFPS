using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonSimpleLauncher : MonoBehaviourPunCallbacks
{

    public PhotonView playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PhotonSimpleLauncher: Connected to Master");
        // Attempt to join a random room, or create one if none are available
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4, // Set the maximum number of players
            IsVisible = true,
            IsOpen = true
        };

        PhotonNetwork.JoinRandomOrCreateRoom(
            expectedCustomRoomProperties: null, // No filter on custom properties
            expectedMaxPlayers: 0,              // No filter on max players
            matchingType: MatchmakingMode.FillRoom,
            roomOptions: roomOptions,           // Use the options defined above
            sqlLobbyFilter: null,               // No SQL filter
            roomName: null                      // Let Photon pick a random name if creating
        );
    }


    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonSimpleLauncher: {playerPrefab.name} joined a room.");
        Vector3 vector = new Vector3(0, 1.3f, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, vector, Quaternion.identity);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"PhotonSimpleLauncher: Failed to join a random room (Code: {returnCode}, Message: {message}), creating a new room.");
        PhotonNetwork.CreateRoom("MyTestRoom1"); // Creates a room with a random name
    }

}
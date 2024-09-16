using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoomManager : MonoBehaviour
{
    [SerializeField] GameObject _exitRoom;
    [SerializeField] GameObject _explodedExitRoom;

    void Start()
    {
        _exitRoom.SetActive(true);
        _explodedExitRoom.SetActive(false);
    }

    public void ExplodeExitRoom()
    {
        _exitRoom.SetActive(false);
        _explodedExitRoom.SetActive(true);
    }
}

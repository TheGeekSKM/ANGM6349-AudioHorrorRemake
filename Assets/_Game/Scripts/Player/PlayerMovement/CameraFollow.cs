using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 _offset;
    public Transform _player;

    // Update is called once per frame
    void Update() 
    {
        transform.position = _player.position + _offset;
    }
}

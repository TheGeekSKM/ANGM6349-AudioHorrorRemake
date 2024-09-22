using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerManager : MonoBehaviour
{
    void Update()
    {
        if (PlayerController.Instance) transform.position = PlayerController.Instance.PlayerMovement.transform.position;
        else transform.position = Camera.main.transform.position;
    }
}

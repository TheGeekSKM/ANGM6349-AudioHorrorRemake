using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingHingeController : MonoBehaviour
{
    [SerializeField] EnemyController _enemyController;

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
         // get the direction from the enemy controller
        Vector3 direction = _enemyController.Direction;

        // constantly rotate along the axis that is perpendicular to the direction
        transform.Rotate(Vector3.Cross(direction, Vector3.up), 1f);
    }

}

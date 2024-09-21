using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public static EnvironmentController Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    [Header("Materials")]
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _walkingMaterial;

    [SerializeField] List<MeshRenderer> _environmentMeshRenderers;

    [Header("Player Spawn")]
    [SerializeField] Transform _playerSpawn;

    public void SetWalkingState(bool isWalking)
    {
        foreach (var renderer in _environmentMeshRenderers)
            renderer.material = isWalking ? _walkingMaterial : _normalMaterial;
    }

    public void MovePlayerToSpawn()
    {
        PlayerController.Instance.PlayerTransform.position = _playerSpawn.position;
    }
}

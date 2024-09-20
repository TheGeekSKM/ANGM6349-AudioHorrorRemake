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

    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _walkingMaterial;

    [SerializeField] List<MeshRenderer> _environmentMeshRenderers;

    public void SetWalkingState(bool isWalking)
    {
        foreach (var renderer in _environmentMeshRenderers)
            renderer.material = isWalking ? _walkingMaterial : _normalMaterial;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    [SerializeField] private string _layerName;

    // Start is called before the first frame update
    void Awake()
    {
        var renderer = GetComponent<MeshRenderer>();
        renderer.sortingLayerName = _layerName;
    }
}

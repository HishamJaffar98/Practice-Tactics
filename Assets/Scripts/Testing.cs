using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem newGridSystem;
    [SerializeField] private Transform gridDebugObjectPrefab;
    void Awake()
    {
        newGridSystem = new GridSystem(10, 10,2f);
        newGridSystem.DebugObjects(gridDebugObjectPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

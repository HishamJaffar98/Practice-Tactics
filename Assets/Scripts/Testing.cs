using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] Unit unit;
    //KEEP FOR FUTURE TESTING
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
		{
            unit.MoveActionComponent.GetValidActionGridPositionList();
		}
    }
}

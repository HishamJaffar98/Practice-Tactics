using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectVisual : MonoBehaviour
{
    private MeshRenderer unitSelectedVisual;
    void Start()
    {
        unitSelectedVisual = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSelectedVisual(bool toggleValue)
	{
        unitSelectedVisual.enabled = toggleValue;
    }
}

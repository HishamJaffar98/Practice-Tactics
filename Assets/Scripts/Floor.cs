using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class Floor : MonoBehaviour, IPointerDownHandler
{
    public static event Action<Vector3> OnFloorClicked;
	public void OnPointerDown(PointerEventData eventData)
	{
        OnFloorClicked?.Invoke(eventData.pointerPressRaycast.worldPosition);
	}


	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

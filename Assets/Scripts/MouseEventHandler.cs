using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseEventHandler : MonoBehaviour, IPointerDownHandler
{
    public static event Action<Vector3> OnGameObjectClicked;
	public void OnPointerDown(PointerEventData eventData)
	{
        OnGameObjectClicked?.Invoke(eventData.pointerPressRaycast.worldPosition);
	}


	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// This class detects valid mouse interactable objects and get's their stats. 
/// </summary>
public class MouseInteractableDetector : MonoBehaviour
{
	#region Singelton Instance
	public static MouseInteractableDetector Instance { get; private set; }
	#endregion

	#region Structs
	[SerializeField] private LayerMask mouseDetectLayerMask;
	#endregion

	#region Unity Cycle Functions
    void Awake()
    {
		if(Instance!=null)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			Instance = this;
		}
	}
	#endregion

	#region Public Functions
	public Vector3 GetPosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mouseDetectLayerMask))
		{
			return hit.point;
		}
		else
		{
			return Vector3.zero;
		}
	}

	public GameObject GetValidObjectInteractedByMouse()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mouseDetectLayerMask))
		{
			return hit.collider.gameObject;
		}
		else
		{
			return null;
		}
	}
	#endregion
}

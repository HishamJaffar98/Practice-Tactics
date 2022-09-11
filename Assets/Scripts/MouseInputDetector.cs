using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseInputDetector : MonoBehaviour
{
	#region Variables
	[SerializeField] private LayerMask mouseDetectLayerMask;
	public static MouseInputDetector instance;
	#endregion

	#region Events
	public static event Action<Vector3> OnGameObjectClicked;
	#endregion

	#region Unity Cycle Functions
	void Awake()
    {
		if (instance != null)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
	}

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit = GetInformationOfValidHitObject();
			if (hit.collider != null)
			{
				OnGameObjectClicked?.Invoke(hit.point);
			}
		}
	}
	#endregion

	#region Private Functions
	private RaycastHit GetInformationOfValidHitObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mouseDetectLayerMask);
		return hit;
	}
	#endregion
}

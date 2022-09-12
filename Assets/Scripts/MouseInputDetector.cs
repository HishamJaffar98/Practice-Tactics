using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseInputDetector : MonoBehaviour
{
	#region Variables
	[SerializeField] private LayerMask mouseDetectLayerMask;
	#endregion

	#region Events
	public static event Action<Vector3> OnFloorClicked;
	public static event Action<Unit> OnUnitClicked;
	#endregion

	#region Unity Cycle Functions
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit = GetInformationOfValidHitObject();
			if (hit.collider != null)
			{
				if (hit.collider.gameObject.tag == TagManager.Floor)
				{
					OnFloorClicked?.Invoke(hit.point);
				}
				else if (hit.collider.gameObject.tag == TagManager.Unit)
				{
					OnUnitClicked?.Invoke(hit.transform.GetComponent<Unit>());
				}
			}
		}
	}
	#endregion

	#region Private Functions
	public RaycastHit GetInformationOfValidHitObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mouseDetectLayerMask);
		return hit;
	}
	#endregion
}

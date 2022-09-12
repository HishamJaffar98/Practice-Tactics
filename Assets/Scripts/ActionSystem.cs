using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible of handling behaviour of various mouse interactable components
/// This acts as the base action class where most behaviour is stored
/// </summary>

public class ActionSystem : MonoBehaviour
{
    #region Singleton Instance
    public static ActionSystem Instance { get; private set; }
    #endregion

    #region Cached Components
    private Unit selectedUnit;
	#endregion

	#region Properties
    public Unit SelectedUnit
	{
        get
		{
            return selectedUnit;
		}
	}
	#endregion

	#region Events
	public event Action<Unit> OnSelectedUnitChanged;
    #endregion

    #region Unity Cycle Fucntions
    private void Awake()
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

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
            if(TryToGetUnit())
		    {
                return;
		    }
            if(selectedUnit!=null)
			{
                selectedUnit.MoveActionComponent.SetMovementParameters(MouseInteractableDetector.Instance.GetPosition());
            }
        }
	}
	#endregion

	#region Private Functions
	private bool TryToGetUnit()
	{
        GameObject interactableObject = MouseInteractableDetector.Instance.GetValidObjectInteractedByMouse();
        if(interactableObject)
		{
            if (interactableObject.TryGetComponent<Unit>(out Unit unit))
            {
                SelectUnit(unit);
                return true;
            }
        }
        return false;
    }

    public void SelectUnit(Unit unit)
    {
        if(selectedUnit!=unit || selectedUnit==null)
		{
            selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(unit);
        }
    }
    #endregion
}

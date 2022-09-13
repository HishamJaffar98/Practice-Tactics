using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class is responsible of handling behaviour of various mouse interactable components
/// This acts as the base action class where most behaviour is stored
/// </summary>

public class ActionSystem : MonoBehaviour
{
    #region Singleton Instance
    public static ActionSystem Instance { get; private set; }
    #endregion

    #region Events
    public event Action OnSelectedUnitChanged;
    public event Action OnUnitActionChanged;
    public event Action<bool> OnBusyChanged;
    public event Action OnActionStarted;
    #endregion

    #region Variables
    private bool isActionBusy=false;
    #endregion

    #region Cached Components
    private Unit selectedUnit;
    private BaseAction selectedAction;
    #endregion

    #region Properties
    public Unit SelectedUnit
	{
        get
		{
            return selectedUnit;
		}
	}

    public BaseAction SelectedAction
	{
        get
		{
            return selectedAction;
		}
	}

	public void SetSelectedAction(BaseAction action)
	{
		selectedAction = action;
        OnUnitActionChanged?.Invoke();
	}
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
        if (isActionBusy)
            return;

        if (!TurnSystem.Instance.IsPlayerTurn)
            return;

        //if (EventSystem.current.IsPointerOverGameObject())
        //	return;

        if (Input.GetMouseButtonDown(0))
        {
            if (TryToGetUnit())
                return;
            HandleSelectedAction();
        }
    }
    #endregion

    #region Private Functions
    private bool TryToGetUnit()
    {
        GameObject interactableObject = MouseInteractableDetector.Instance.GetValidObjectInteractedByMouse();
        if (interactableObject)
        {
            if (interactableObject.TryGetComponent<Unit>(out Unit unit))
            {
                if (unit == selectedUnit)
                    return false;
                if (unit.IsEnemy)
                    return false;

                SetSelectedUnit(unit);;
                return true;
            }
        }
        return false;
    }
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetAction<MoveAction>());
        OnSelectedUnitChanged?.Invoke();
    }

    private void HandleSelectedAction()
    {
        GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseInteractableDetector.Instance.GetPosition());
        if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
        {
            return;
        }

        if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
        {
            return;
        }
        SetBusy();
        selectedAction.TakeAction(mouseGridPosition, ClearBusy);
        OnActionStarted?.Invoke();
    }

    private void SetBusy()
    {
        isActionBusy = true;
        OnBusyChanged?.Invoke(isActionBusy);
    }
    private void ClearBusy()
	{
        isActionBusy = false;
        OnBusyChanged?.Invoke(isActionBusy);
    }
    #endregion
}

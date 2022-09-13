using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionsUIManager : MonoBehaviour
{
	[SerializeField] GameObject actionButtonPrefab;
	[SerializeField] Transform actionButtonsContainer;
	[SerializeField] private TextMeshProUGUI actionPointsText;

	private List<ActionButton> buttonsCurrentlyInContainer = new List<ActionButton>();
	private void OnEnable()
	{
		ActionSystem.Instance.OnSelectedUnitChanged += ActionsUIManager_OnSelectedUnitChanged;
		ActionSystem.Instance.OnUnitActionChanged += ActionsUIManager_OnSelectedActionChanged;
		ActionSystem.Instance.OnActionStarted += ActionUIManager_OnActionStarted;
	}

	private void OnDisable()
	{
		ActionSystem.Instance.OnSelectedUnitChanged -= ActionsUIManager_OnSelectedUnitChanged;
		ActionSystem.Instance.OnUnitActionChanged -= ActionsUIManager_OnSelectedActionChanged;
		ActionSystem.Instance.OnActionStarted -= ActionUIManager_OnActionStarted;
	}

	private void ActionsUIManager_OnSelectedUnitChanged()
	{
		CreateUnitActionButtons();
		UpdateSelectedVisual();
		UpdateActionPoints();
	}

	private void ActionsUIManager_OnSelectedActionChanged()
	{
		UpdateSelectedVisual();
	}

	private void ActionUIManager_OnActionStarted()
	{
		UpdateActionPoints();
	}


	private void CreateUnitActionButtons()
	{
		DestroyOldUnitActionButtons();
		buttonsCurrentlyInContainer.Clear();
		Unit selectedUnit = ActionSystem.Instance.SelectedUnit;
		foreach (BaseAction baseAction in selectedUnit.ActionArray)
		{
			GameObject actionButton = Instantiate(actionButtonPrefab, actionButtonsContainer);
			actionButton.GetComponent<ActionButton>().SetActionButton(baseAction);
			buttonsCurrentlyInContainer.Add(actionButton.GetComponent<ActionButton>());
		}
	}

	private void DestroyOldUnitActionButtons()
	{
		foreach(Transform buttons in actionButtonsContainer)
		{
			Destroy(buttons.gameObject);
		}
	}

	private void UpdateSelectedVisual( )
	{
		foreach(Transform button in actionButtonsContainer)
		{
			button.GetComponent<ActionButton>().UpdateSelectedVisual();
		}
	}
	private void UpdateActionPoints()
	{
		Unit selectedUnit = ActionSystem.Instance.SelectedUnit;
		actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
	}


}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI actionButtonText;
    [SerializeField] Button button;
    [SerializeField] Image selectedImage;
    private BaseAction baseAction;

    public void SetActionButton(BaseAction action)
	{
        this.baseAction = action;
        actionButtonText.text = action.GetActionName();
        button.onClick.AddListener(() => {
            ActionSystem.Instance.SetSelectedAction(action);
        });

    }
   
    public void UpdateSelectedVisual()
	{
        BaseAction selectedBaseAction = ActionSystem.Instance.SelectedAction;
        selectedImage.enabled = (selectedBaseAction==baseAction);
    }
 
}

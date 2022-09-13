using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{

    private void Start()
    {
        ActionSystem.Instance.OnBusyChanged += ToggleBusyUI;
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ToggleBusyUI(bool isActionBusy)
    {
        if (isActionBusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}

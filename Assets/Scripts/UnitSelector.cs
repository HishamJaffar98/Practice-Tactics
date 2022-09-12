using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    #region Singleton Instance
    public static UnitSelector Instance { get; private set; }
    #endregion

    #region Variables
    private Unit selectedUnit;
	#endregion

	#region Events
	public event Action<Unit> OnSelectedUnitChanged;
	#endregion

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
	void OnEnable()
    {
        MouseInputDetector.OnUnitClicked += SelectUnit;
    }

    // Update is called once per frame
    void OnDisable()
    {
        MouseInputDetector.OnUnitClicked -= SelectUnit;
    }

    private void SelectUnit(Unit unit)
    {
        if(selectedUnit!=unit || selectedUnit==null)
		{
            selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(unit);
        }
    }
}

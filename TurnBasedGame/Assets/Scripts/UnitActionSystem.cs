using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance {  get; private set; }

    public event EventHandler OnSelectedUnitChange;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake() {
        if (Instance != null) { // Error Checking Singleton
            Debug.LogError("More Than One UnitActionSystem! " + transform + " - " + Instance); // Logs error & where abouts its coming from.
            Destroy(gameObject); // Destroys the GameObject to stop breaking.
            return; // Returns to stop instance being set again.
        }

        Instance = this; // Setting Instance (Singleton)
    }
    private void Update() {

        if (Input.GetMouseButtonDown(0)) {
            if (TryHandleUnitSelection()) return; // Selected Unit & Returns Early (Stops Movement On Selection)
            
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from camera to mouse position
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))  // If successful raycast to unit layer...
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit)) // Try to find Unit Component (If Successful..)
            {
                SetSelectedUnit(unit); // Update Selected Unit with unit clicked
                return true; // Diferent Unit Selected
            }
        }

        return false; // Different Unit Not Selected
    }

    private void SetSelectedUnit(Unit unit) {
        selectedUnit = unit; // Update Selected Unit
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty); // Fire Selected Unit Event (Visuals)
    }

    public Unit GetSelectedUnit() {
        return selectedUnit;
    }
}

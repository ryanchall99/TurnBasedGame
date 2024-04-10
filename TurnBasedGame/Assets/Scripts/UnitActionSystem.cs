using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

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
                selectedUnit = unit; // Update Selected Unit with unit clicked
                return true; // Diferent Unit Selected
            }
        }

        return false; // Different Unit Not Selected
    }
}

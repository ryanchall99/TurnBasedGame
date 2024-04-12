using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectedUnitChange;

        UpdateVisual(); // Runs Visuals Code On Start (Starting Unit).
    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit) { // If Selected Unit Is Same As Unit Set Here...
            meshRenderer.enabled = true; // Enable its mesh renderer.
        }
        else {
            meshRenderer.enabled = false; // Disable its mesh renderer.
        }
    }
}

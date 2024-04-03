using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld Instance;

    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Two Instances Of MouseWorld Not Allowed!");
        }
    }

    public static Vector3 GetPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from Main Camera to Mouse Position
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.mousePlaneLayerMask); // Raycast that outputs the Hit, Has no set distance & Only uses Mouse Plane Layer

        return raycastHit.point; // Returns the raycast impact point in world space
    }
}

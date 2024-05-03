using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [Header("Movement & Rotation")]
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotateSpeed = 10f;

    [Header("Animation")]
    [SerializeField] private Animator unitAnimator;

    private Vector3 targetPosition;
    private GridPosition gridPosition;

    private void Awake() {
        targetPosition = transform.position; // Initialising Target Position to units starting position 
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position); // Gets Grid Position Based On Units Initial Transform
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update() {
        HandleMovement();

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if (newGridPosition != gridPosition) // New Grid Position not the same as current stored grid position
        {
            // Unit Changed Grid Position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition); // Update GridPosition Data
            gridPosition = newGridPosition; // Update GridPosition
        }
    }

    private void HandleMovement() {
        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized; // Move Direction (No Magnitude)
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed); // Unit Faces towards movement direction

            unitAnimator.SetBool(IS_WALKING, true); // Update Animation (Walking)
        }
        else {
            unitAnimator.SetBool(IS_WALKING, false); // Update Animation (Idle)
        }
    }

    public void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition; // Setting Member Variable
    }
}

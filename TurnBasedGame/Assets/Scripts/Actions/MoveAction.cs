using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAction : BaseAction
{
    private const string IS_WALKING = "IsWalking";

    [Header("Testing")]
    [SerializeField] private int maxMoveDistance = 4;

    [Header("Movement & Rotation")]
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotateSpeed = 10f;

    [Header("Animation")]
    [SerializeField] private Animator unitAnimator;

    private Vector3 targetPosition;

    protected override void Awake()
    {
        base.Awake(); // Run BaseAction Awake then rest of MoveAction Awake
        targetPosition = transform.position; // Initialising Target Position to units starting position 
    }

    private void Update()
    {
        if (!isActive)
        {
            return; // Stop code running
        }
            
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized; // Move Direction (No Magnitude)

        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            unitAnimator.SetBool(IS_WALKING, true); // Update Animation (Walking)
        }
        else
        {
            unitAnimator.SetBool(IS_WALKING, false); // Update Animation (Idle)
            isActive = false; // Reached target
        }

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed); // Unit Faces towards movement direction
    }

    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition); // Moving to grid position (Converted to world space)
        isActive = true;
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    // Skip to next iteration if not valid grid space
                    continue;
                }

                if(unitGridPosition == testGridPosition)
                {
                    // Skip Iteration (Same grid position as unit)
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Skip Iteration (Unit already in space)
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}

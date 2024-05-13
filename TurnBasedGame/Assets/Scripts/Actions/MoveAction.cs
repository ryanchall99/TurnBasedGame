using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
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
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position; // Initialising Target Position to units starting position 
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized; // Move Direction (No Magnitude)
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed); // Unit Faces towards movement direction

            unitAnimator.SetBool(IS_WALKING, true); // Update Animation (Walking)
        }
        else
        {
            unitAnimator.SetBool(IS_WALKING, false); // Update Animation (Idle)
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition; // Setting Member Variable
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
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}

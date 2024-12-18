using UnityEngine;
using System;
using System.Collections.Generic;

public class AG2Agent : MonoBehaviour
{
    [Header("Agent Properties")]
    public string agentId;
    public string role;
    
    [Header("State")]
    public float energy = 100f;
    public float social = 50f;
    public Vector3 targetPosition;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;

    // Internal state
    private bool isMoving = false;
    private Dictionary<string, float> relationships = new Dictionary<string, float>();

    private void Start()
    {
        // Generate unique ID if none assigned
        if (string.IsNullOrEmpty(agentId))
        {
            agentId = System.Guid.NewGuid().ToString();
        }

        // Initialize starting position as current target
        targetPosition = transform.position;
    }

    private void Update()
    {
        // Basic movement
        if (isMoving)
        {
            MoveToTarget();
        }

        // Decrease energy over time
        energy -= Time.deltaTime;
        energy = Mathf.Clamp(energy, 0f, 100f);
    }

    private void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            // Calculate direction to target
            Vector3 direction = (targetPosition - transform.position).normalized;
            
            // Rotate towards target
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Move towards target
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            isMoving = false;
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        targetPosition = newTarget;
        isMoving = true;
    }

    public void UpdateRelationship(string otherAgentId, float value)
    {
        relationships[otherAgentId] = Mathf.Clamp(value, -1f, 1f);
    }

    public float GetRelationship(string otherAgentId)
    {
        return relationships.ContainsKey(otherAgentId) ? relationships[otherAgentId] : 0f;
    }

    public void RestoreEnergy(float amount)
    {
        energy = Mathf.Clamp(energy + amount, 0f, 100f);
    }

    public void UpdateSocial(float delta)
    {
        social = Mathf.Clamp(social + delta, 0f, 100f);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw line to target when selected in editor
        if (isMoving)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, targetPosition);
        }
    }
}
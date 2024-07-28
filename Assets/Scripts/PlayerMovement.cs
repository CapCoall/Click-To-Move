using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = 0;
        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.2f;

        _lineRenderer.material = new Material(Shader.Find("Sprites/Default")) { color = Color.red };    
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();
    }
     private void MoveToCursor()
     {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit) 
        {
            Vector3 _targetPoint = hit.point;
            _agent.destination = hit.point;
        }
        DrawPath();
     }

    private void DrawPath()
    {
        if (_agent.path.corners.Length < 2)
        {
            _lineRenderer.positionCount = 0;
            return;
        }

        _lineRenderer.positionCount = _agent.path.corners.Length;
        _lineRenderer.SetPositions(_agent.path.corners);
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity; 
        Vector3 localVelocity =transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("ZSpeed", speed);
    }   
}

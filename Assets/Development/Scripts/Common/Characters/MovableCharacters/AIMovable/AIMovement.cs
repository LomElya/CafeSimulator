using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed = 5f;

    private float _speedRate = 1f;

    private NavMeshAgent _agent;
    private Action _completeAction;

    public bool IsRun { get; private set; }

    public bool Completed { get; private set; }
    public float NormalizedSpeed => _agent.velocity.magnitude / 5f;
    public float RemainingDistance => _agent.remainingDistance;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        enabled = false;
        Completed = false;

        _agent.speed = _speed * _speedRate;
    }

    private void Update()
    {
        if (_agent.pathPending ||
           _agent.pathStatus == NavMeshPathStatus.PathInvalid ||
           _agent.path.corners.Length == 0)
            return;

        if (_agent.remainingDistance < _agent.stoppingDistance + float.Epsilon)
        {
            _completeAction?.Invoke();
            _completeAction = null;

            Completed = true;
            enabled = false;
        }
    }

    public float CalculateRemainingDistance()
    {
        var points = _agent.path.corners;

        if (points.Length < 2)
            return 0;

        float distance = 0;

        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);

        return distance;
    }

    public void Warp(Vector3 position) => _agent.Warp(position);

    public void Move(Vector3 target)
    {
        IsRun = true;
        Completed = false;
        _completeAction = null;

        _agent.ResetPath();
        _agent.SetDestination(target);
        enabled = true;
    }

    public AIMovement MoveAI(Vector3 target)
    {
        Move(target);
        return this;
    }

    public void Stop()
    {
        IsRun = false;
        _completeAction = null;
        _agent.ResetPath();
    }

    public void OnComplete(Action completeAction)
    {
        _completeAction = completeAction;
    }

    public void Enable() => _agent.enabled = true;
    public void Disable() => _agent.enabled = false;
    public void SetPriority(int value) => _agent.avoidancePriority = value;
    public void Look(Vector3 direction) => transform.DOLookAt(transform.position + direction, 1f);
}

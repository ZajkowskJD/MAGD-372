using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class WizardController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float proximityBuffer = 0.5f;
    [SerializeField] private float reactionTime = 0.2f;

    private NavMeshAgent agent;
    private float distance = 0;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        _Wander();
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(agent.destination, transform.position);
    }

    private async void _Wander() {
        Debug.Log("start");
        agent.destination = waypoints[Random.Range(0, waypoints.Length)].position;
        while (true) {
            while (distance > proximityBuffer)
            {
                await Task.Yield();
            }
            var duration = Time.time + reactionTime;
            while (Time.time < duration)
            {
                await Task.Yield();
            }
            Debug.Log("finish");
            agent.destination = waypoints[Random.Range(0, waypoints.Length)].position;
        }
    }
}

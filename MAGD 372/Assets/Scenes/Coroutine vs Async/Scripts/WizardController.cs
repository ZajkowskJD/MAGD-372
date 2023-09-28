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
    [SerializeField] private float maxWaitTime = 1f;
    [SerializeField] private float minWaitTime = 0.5f;
    [SerializeField] private Transform[] lockPoints;

    private NavMeshAgent agent;
    private float distance = 0;
    private int maxGrabs = 3;
    private DodgeballController[] heldBalls = {null,null,null};

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        Wander();
        AttackPattern();
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(agent.destination, transform.position);
    }

    private async void Wander() {
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
            agent.destination = waypoints[Random.Range(0, waypoints.Length)].position;
        }
    }

    private async void AttackPattern()
    {
        float prepTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
        while(Time.time < prepTime)
        {
            await Task.Yield();
        }
        GameObject[] grabbable = GameObject.FindGameObjectsWithTag("unlocked");
        for(int i = 0; i < maxGrabs; i++)
        {
            DodgeballController chosen = grabbable[Random.Range(0, grabbable.Length)].GetComponent<DodgeballController>();
            chosen.LockToPoint(lockPoints[i]);
            heldBalls[i] = chosen;
        }
        await Task.Delay(1000);
        float magnitude = 30f;
        for(int i = 0; i < maxGrabs; i++)
        {
            Vector3 aimAtPlayer = -(transform.position - GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(0f,1f,0f)).normalized;
            heldBalls[i].DisableLock();
            heldBalls[i].Launch(aimAtPlayer, magnitude);
            await Task.Delay(500);
        }
        await Task.Delay(1000);
        AttackPattern();
    }
}

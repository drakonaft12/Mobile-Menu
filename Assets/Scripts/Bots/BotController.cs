using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;




[RequireComponent(typeof(NavMeshAgent))]
public class BotController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject player, playerSteals;
    [SerializeField] GameObject componentSteals, Eyes;
    Slider slider;
    NavMeshPath path1, path2;

    Vector3 point;
    float statSteals = 10, stealsPoint = 0, length = 2f;

    public float ThisSteals { get => stealsPoint/ statSteals; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        componentSteals.TryGetComponent(out slider);
        point = player.transform.position;
        StealsHUD.me.BotAdd(this);
        path1 = new NavMeshPath();
        path2 = new NavMeshPath();
    }

    private void Update()
    {

        if (Physics.Raycast(Eyes.transform.position, player.transform.position - Eyes.transform.position, out var hit))
        {

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == player)
                {
                    StealsPointsAdd(40);
                }
                else if (hit.collider.gameObject == playerSteals)
                {
                    StealsPointsAdd(10);
                }
                else if (stealsPoint > 0)
                    stealsPoint -= Time.deltaTime;
            }


        }

        StealsFunction();

        if (agent.remainingDistance < length)
        {
            point = FindPath(point);
        }
        agent.CalculatePath(point, path1);
        if ((player.transform.position - transform.position).magnitude <= 4 && Viet)
        {
            agent.FindClosestEdge(out var hit1);
            agent.CalculatePath(hit1.position, path1);
        }
        ResetPath();



        //if (slider != null)
            //slider.value = stealsPoint / statSteals;
        //componentSteals.transform.LookAt(Camera.main.transform);
    }

    private void StealsFunction()
    {
        if (Viet)
        {
            point = player.transform.position;

        }
        if (!Viet && stealsPoint >= statSteals)
        {
            Viet = true;
        }
        if (Viet && stealsPoint <= statSteals / 2)
        {
            Viet = false;
        }
    }

    void ResetPath()
    {
        if (path1.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(path1.corners[path1.corners.Length - 1]);
        }
        else
        {
            if (agent.remainingDistance < length)
            {
                agent.CalculatePath(FindPath(transform.position), path2);
                agent.SetPath(path2);
            }
        }
    }

    bool Viet = false;
    void StealsPointsAdd(float val)
    {
        if (stealsPoint < statSteals)
        {
            stealsPoint += 2 * Time.deltaTime * val / (transform.position - player.transform.position).magnitude *
                val / (transform.position - player.transform.position).magnitude;
        }
    }

    Vector3 FindPath(Vector3 vector)
    {
        Vector3 vector3 = vector;
        for (int i = 0; i < 20; i++)
        {
            vector3 = vector + Vector3.forward * Random.Range(-15f, 15f) + Vector3.right * Random.Range(-15f, 15f);
            if (!agent.Raycast(vector3, out var hit)) { return vector3; }
        }
        return vector;
    }

}

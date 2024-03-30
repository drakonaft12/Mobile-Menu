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
    [SerializeField] GameObject componentSteals;
    Slider slider;

    Vector3 point;
    float statSteals = 10, stealsPoint = 0, length = 0.3f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        componentSteals.TryGetComponent(out slider);
        point = player.transform.position;
    }

    private void Update()
    {
        if (!agent.SetDestination(point))
        {
            point += (point-player.transform.position)*0.5f;
        }

        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out var hit))
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

            if ( (transform.position - point).magnitude < length) 
            {
                point += Vector3.forward * Random.Range(-5f, 5f) + Vector3.right * Random.Range(-5f, 5f);
            }
        }
        Debug.Log(length);
        
        StealsFunction();

        if (slider != null)
            slider.value = stealsPoint / statSteals;
        //componentSteals.transform.LookAt(Camera.main.transform);
    }

    private void StealsFunction()
    {
        if (Viet)
            point = player.transform.position;
        if (!Viet && stealsPoint >= statSteals)
        {
            Viet = true;
        }
        if (Viet && stealsPoint <= statSteals/2)
        {
            Viet = false;
        }
    }

    bool Viet = false;
    void StealsPointsAdd(float val)
    {
        if (stealsPoint < statSteals) { stealsPoint += Time.deltaTime * val / (transform.position - player.transform.position).magnitude * 
                                                       val / (transform.position - player.transform.position).magnitude; }
    }

    

}

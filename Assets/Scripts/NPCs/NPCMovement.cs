using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [Header("Movement and action setup.")]
    [Tooltip("All the positions the NPC will move toward in order.")]
    [SerializeField] Transform[] targetPos;
    [Tooltip("The amount of time an NPC will spend at each location.")]
    [SerializeField] int[] timeSpent;
    [Tooltip("The name of the animation state that should trigger at each location.")]
    [SerializeField] string[] animName;
    [Tooltip("The way the player should be facing when stopped for animation.")]
    [SerializeField] Transform[] viewPos;
    [Tooltip("The animation controller for the NPC.")]
    [SerializeField] Animator animController;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(AnimLoop());
    }

    private void OnEnable()
    {
        StartCoroutine(AnimLoop());
    }

    IEnumerator AnimLoop()
    {
        int currentLoopPos = 0;
        yield return new WaitForSeconds(1f);
        while (gameObject.activeSelf)
        {
            agent.SetDestination(targetPos[currentLoopPos].position);
            animController.SetBool("walking", true);
            while (agent.remainingDistance > 0.01f)
            {
                yield return new WaitForSeconds(0.1f);
            }
            animController.SetBool("walking", false);
            agent.isStopped = true;
            transform.LookAt(viewPos[currentLoopPos].position);
            animController.SetBool(animName[currentLoopPos], true);
            yield return new WaitForSeconds(timeSpent[currentLoopPos]);
            animController.SetBool(animName[currentLoopPos], false);
            agent.isStopped = false;
            currentLoopPos++;
            if (currentLoopPos > targetPos.Length - 1)
                currentLoopPos = 0;
        }
    }
}

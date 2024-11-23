using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform queuePosition;         // NPC'nin sıraya gireceği nokta
    public Transform shootingPosition;      // NPC'nin ateş etmesi gereken nokta
    public Transform cashRegisterPosition;  // NPC'nin ödeme yapacağı kasa noktası
    public Transform exitPoint;             // NPC'nin çıkış yapacağı nokta
    private NavMeshAgent agent;             // NPC'nin hareketini kontrol eden NavMeshAgent

    public enum NPCState
    {
        Queueing,
        WalkingToShootingPosition,
        Shooting,
        Paying,
        Exiting
    }

    public NPCState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = NPCState.Queueing;
        MoveToQueuePosition();
    }

    void MoveToQueuePosition()
    {
        if (queuePosition != null)
        {
            agent.SetDestination(queuePosition.position);
        }
    }

    void MoveToShootingPosition()
    {
        if (shootingPosition != null)
        {
            agent.SetDestination(shootingPosition.position);
        }
    }

    void MoveToCashRegister()
    {
        if (cashRegisterPosition != null)
        {
            agent.SetDestination(cashRegisterPosition.position);
        }
    }

    void MoveToExitPoint()
    {
        if (exitPoint != null)
        {
            agent.SetDestination(exitPoint.position);
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case NPCState.Queueing:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        currentState = NPCState.WalkingToShootingPosition;
                        MoveToShootingPosition();
                    }
                }
                break;

            case NPCState.WalkingToShootingPosition:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        currentState = NPCState.Shooting;
                        GetComponent<NPCShooting>().StartShooting();
                    }
                }
                break;

            case NPCState.Shooting:
                // Atış işlemi NPCShooting scripti tarafından yönetiliyor
                break;

            case NPCState.Paying:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Ödeme işlemi burada yapılabilir
                        Debug.Log("NPC ödeme yapıyor.");
                        currentState = NPCState.Exiting;
                        MoveToExitPoint();
                    }
                }
                break;

            case NPCState.Exiting:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        Debug.Log("NPC poligondan çıktı.");
                        Destroy(gameObject); // NPC'yi yok et
                    }
                }
                break;
        }
    }

    public void OnShootingFinished()
    {
        Debug.Log("NPC tüm atışlarını tamamladı ve kasa noktasına doğru yürüyor.");
        currentState = NPCState.Paying;
        MoveToCashRegister();
    }
}
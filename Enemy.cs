using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform enemy;
    public LayerMask whatIsGround, whatIsPlayer;

    //patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAtk;
    bool alreadyAtk;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;





public float health = 1500;
  public void TakeDamage(float amount)
  {
    health -= amount;
    if (health <= 0)
    {
        die();
    }
  }
  void die()
  {
    Destroy(gameObject);
  }











    private void Awake()
    {
        enemy = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
     playerInSightRange = Physics.CheckSphere(transform.position, sightRange , whatIsPlayer);
     playerInAttackRange = Physics.CheckSphere(transform.position, attackRange , whatIsPlayer);

     if(!playerInSightRange && !playerInAttackRange) patroling();
     if(playerInSightRange && ! playerInAttackRange) chasePlayer();
     if(playerInAttackRange && playerInSightRange) attackPlayer();
    }

    void patroling()
    {
     if(!walkPointSet) searchWalkPoint();
     if(walkPointSet) agent.SetDestination(walkPoint);
     
     Vector3 distanceToWalkPoint = transform.position - walkPoint;
     if(distanceToWalkPoint.magnitude < 1f) walkPointSet = false;


    }

    void searchWalkPoint()
    {
     float randomZ = Random.Range(-walkPointRange, walkPointRange);
     float randomX = Random.Range(-walkPointRange, walkPointRange);

     walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

     if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
     {
        walkPointSet = true;
     }
    }

    void chasePlayer()
    {
        agent.SetDestination(enemy.position);
    }

    void attackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(enemy);

        if(!alreadyAtk)
        {
            alreadyAtk = true;
            Invoke(nameof(resetAttack), timeBetweenAtk);
        }

        void resetAttack()
        {
            alreadyAtk = false;
        }
    }






}

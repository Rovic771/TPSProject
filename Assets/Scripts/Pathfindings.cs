using UnityEngine;
using UnityEngine.AI;

public class Pathfindings : MonoBehaviour
{
    [SerializeField] private Transform[] objectifs;
    [SerializeField] private GameObject player;
    private NavMeshAgent _agent;
    private Vector3 _currentDestination;
    private int _currentDestinationIndex = 0;
    public bool _detectedPlayer = false;
    private EnemyDetection _detectionEnemy;
    private PlayerManager _playerManager;
    
    private void Start()
    {
        _playerManager = player.GetComponent<PlayerManager>();
        _detectionEnemy = GetComponentInChildren<EnemyDetection>();
        _currentDestination = objectifs[0].position;
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null && objectifs != null)
        {
            _agent.SetDestination(_currentDestination);
        }
    }

    public void PursuitPlayer(Vector3 playerPos)
    {
        _currentDestination = playerPos;
        _agent.SetDestination(_currentDestination);
        if (!_detectedPlayer && _detectionEnemy.canDetect)
        {
            _detectedPlayer = true;
        }
    }
    
    public void GoToLastPlayerPos(Vector3 lastPlayerPos)
    {
        PursuitPlayer(lastPlayerPos);
    }
    
    void Update()
    {
        Debug.Log("Enemy va a " + _currentDestination);
        if (Vector3.Distance(transform.position, _currentDestination) < 1.5f)
        {
            if (!_detectedPlayer)
            {
                _currentDestinationIndex += 1;
                _currentDestination = objectifs[_currentDestinationIndex % objectifs.Length].position;
                _agent.SetDestination(_currentDestination);
            }
            else
            {
                Debug.Log("Joueur tué");
                _detectedPlayer = false;
                StartCoroutine(_playerManager.DieDelay());
                StartCoroutine(_detectionEnemy.DetectionDelay());
            }
        }

        if (_detectedPlayer)
        {
            PursuitPlayer(player.transform.position);
        }
    }
}


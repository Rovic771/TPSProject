using UnityEngine;
using UnityEngine.AI;

public class Pathfindings : MonoBehaviour
{
    [SerializeField] private Transform[] objectifs;
    [SerializeField] private GameObject player;
    private NavMeshAgent _agent;
    private Vector3 _currentDestination;
    private int _currentDestinationIndex = 0;
    private bool _detectedPlayer = false;
    private Detection _detectionSystem;
    private PlayerManager _playerManager;
    
    private void Start()
    {
        _playerManager = player.GetComponent<PlayerManager>();
        _detectionSystem = GetComponentInChildren<Detection>();
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
        if (!_detectedPlayer && _detectionSystem.canDetect)
        {
            _detectedPlayer = true;
        }
    }
    
    void Update()
    {
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
                _detectedPlayer = false;
                StartCoroutine(_playerManager.DieDelay());
                StartCoroutine(_detectionSystem.DetectionDelay());
            }
        }

        if (_detectedPlayer)
        {
            PursuitPlayer(player.transform.position);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeGameManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera _playerCamera;
    public Camera playerCamera => _playerCamera;

    [SerializeField] private Camera _cannonCamera;
    public Camera cannonCamera => _cannonCamera;

    [Header("Components")]
    [SerializeField] private Transform _respawnPosition;
    public Transform respawnPosition => _respawnPosition;

    [SerializeField] private ProjectileThrow _cannon;
    public ProjectileThrow cannon => _cannon;
    private TrajectoryPredictor _cannonPredictor;
    public TrajectoryPredictor cannonPredictor => _cannonPredictor;
    private MouseLook _cannonLook;
    public MouseLook cannonLook => _cannonLook;
    [SerializeField] private PlayerMovement _playerMovement;
    public PlayerMovement playerMovement => _playerMovement;

    private GameState _currentState;
    public GameState currentState => _currentState;


    private void Start()
    {
        _cannonPredictor = _cannon.gameObject.GetComponent<TrajectoryPredictor>();
        _cannonLook = _cannon.gameObject.GetComponent<MouseLook>();
        SetState(new ShootingState(this));
    }

    private void Update()
    {
        _currentState?.OnUpdate();

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

    public void SetState(GameState newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public void EnablePlayerCamera()
    {
        playerCamera.enabled = true;
        cannonCamera.enabled = false;
    }
    
    public void EnableCannonCamera()
    {
        playerCamera.enabled = false;
        cannonCamera.enabled = true;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //debug reset scene
    }
}

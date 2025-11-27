using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlimeGameManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera _playerCamera;
    public Camera playerCamera => _playerCamera;

    [SerializeField] private Camera _cannonCamera;
    public Camera cannonCamera => _cannonCamera;

    [SerializeField] private FreeLookCamera _cannonFreeLook;
    public FreeLookCamera cannonFreeLook => _cannonFreeLook;

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

    [Header("Slime HP Bar")]
    [SerializeField] private GameObject _lifespanDisplayer;
    [SerializeField] private Image _slimeLifespanBar;
    [SerializeField] private Image _slimeManualStopBar;


    private void Start()
    {
        _cannonPredictor = _cannon.gameObject.GetComponent<TrajectoryPredictor>();
        _cannonLook = _cannon.gameObject.GetComponent<MouseLook>();
        _cannonFreeLook = _cannonCamera.GetComponent<FreeLookCamera>();
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
    public void ShowLifespanBar()
    {
        _lifespanDisplayer.SetActive(true);
    }

    public void HideLifespanBar()
    {
        _lifespanDisplayer.SetActive(false);
    }

    public void UpdateHPBar(float currentLifespan, float maxLifespan)
    {
        Vector3 barScale = _slimeLifespanBar.transform.localScale;
        barScale.x = currentLifespan / maxLifespan;

        _slimeLifespanBar.transform.localScale = barScale;
        //_slimeLifespanBar.transform.localScale;
    }

    public void UpdateManualStopbar(float currentLifespan, float maxLifespan)
    {
        Vector3 barScale = _slimeManualStopBar.transform.localScale;
        barScale.x = currentLifespan / maxLifespan;

        _slimeManualStopBar.transform.localScale = barScale;
    }
}

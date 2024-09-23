using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using SaiUtils.StateMachine;

public class PlayerArtDisplay : MonoBehaviour
{
    [SerializeField] GameObject _playerArt;
    [SerializeField] GameObject _playerFloorReticle;
    [SerializeField] Transform _player;
    [SerializeField] List<GameObject> _playerAnimationList;
    [SerializeField] Animator _playerAnimator;
    bool _artEnabled;
    Vector3 _offset;
    PlayerMovement _playerMovement;

    StateMachine _playerAnimationStateMachine;

    public PlayerLookMonsterCloseAnimState PlayerLookMonsterCloseAnimState { get; private set; }
    public PlayerLookMonsterFarAnimState PlayerLookMonsterFarAnimState { get; private set; }
    public PlayerWalkAnimState PlayerWalkAnimState { get; private set; }

    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int LookingAround_MonsterClose = Animator.StringToHash("LookingAround_MonsterClose");
    private static readonly int LookingAround_MonsterFar = Animator.StringToHash("LookingAround_MonsterFar");

    void Awake()
    {
        _playerAnimationStateMachine = new StateMachine();

        PlayerLookMonsterCloseAnimState = new PlayerLookMonsterCloseAnimState(_playerAnimator, LookingAround_MonsterClose, 0.1f);
        PlayerLookMonsterFarAnimState = new PlayerLookMonsterFarAnimState(_playerAnimator, LookingAround_MonsterFar, 0.1f);
        PlayerWalkAnimState = new PlayerWalkAnimState(_playerAnimator, Walk, 0.1f);

        _playerAnimationStateMachine.AddAnyTransition(PlayerWalkAnimState, new BlankPredicate());
        _playerAnimationStateMachine.AddAnyTransition(PlayerLookMonsterCloseAnimState, new BlankPredicate());
        _playerAnimationStateMachine.AddAnyTransition(PlayerLookMonsterFarAnimState, new BlankPredicate());

        _playerAnimationStateMachine.SetState(PlayerLookMonsterFarAnimState);

        Debug.Log($"PlayerAnimStateMachine Set Up: {_playerAnimationStateMachine != null}");
    }


    void Start()
    {
        SetPlayerArtInactive();

        // at the start of the game, the player art is disabled
        _artEnabled = false;
        _offset = new Vector3(0, -0.5f, 0);

        _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    [Button]
    void FillList()
    {
        // clears the _playerAnimationList
        _playerAnimationList.Clear();

        // fills the _playerAnimationList with all the children of the _playerArt
        foreach (Transform child in _playerArt.transform)
        {
            _playerAnimationList.Add(child.gameObject);
        }
    }

    [Button]
    public void SetPlayerArtActive()
    {
        if (_artEnabled) return;

        // sets the player art to active and sets the position to the player's position
        _playerArt.SetActive(true);
        _playerArt.transform.position = _player.position;

        // sets a random animation from the _playerAnimationList to active
        int randomIndex = Random.Range(0, _playerAnimationList.Count);
        _playerAnimationList[randomIndex].SetActive(true);
        _playerFloorReticle.SetActive(true);

        switch (_playerMovement.PlayerDirection)
        {
            case PlayerDirection.Up:
                _playerArt.transform.rotation = Quaternion.Euler(0, 0, 0);
                _playerFloorReticle.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case PlayerDirection.Right:
                _playerArt.transform.rotation = Quaternion.Euler(0, 90, 0);
                _playerFloorReticle.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case PlayerDirection.Down:
                _playerArt.transform.rotation = Quaternion.Euler(0, 180, 0);
                _playerFloorReticle.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case PlayerDirection.Left:
                _playerArt.transform.rotation = Quaternion.Euler(0, 270, 0);
                _playerFloorReticle.transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            default:
                break;
        }

        _artEnabled = true;
    }

    [Button]
    public void SetPlayerArtInactive()
    {
        // // sets the player art to inactive
        // _playerArt.SetActive(false);
        // foreach (GameObject anims in _playerAnimationList)
        // {
        //     anims.SetActive(false);
        // }
        // _playerFloorReticle.SetActive(false);
        // _artEnabled = false;
    }

    public void PlayerMove(bool isMoving)
    {

        Debug.Log("PlayerMove: " + isMoving);
        if (isMoving) 
        {
            _playerAnimationStateMachine.ChangeState(PlayerWalkAnimState);
            return;
        }
        
        if (EnemyController.Instance && _playerMovement)
        {
            if (EnemyController.Instance.Direction == _playerMovement.MoveDirection)
            {
                _playerAnimationStateMachine.ChangeState(PlayerLookMonsterFarAnimState);
                return;
            }
            else
            {
                _playerAnimationStateMachine.ChangeState(PlayerLookMonsterCloseAnimState);
                return;
            }
        }

        _playerAnimationStateMachine?.ChangeState(PlayerLookMonsterFarAnimState);
    }

    void Update()
    {
        if (_artEnabled)
        {
            _playerArt.transform.position = _player.position + _offset;
            _playerFloorReticle.transform.position = _player.position;

            // sets the player art to face the MoveDirection from the PlayerMovement script
            _playerArt.transform.LookAt(_player.position + _playerMovement.MoveDirection);
            _playerAnimationList[0].transform.LookAt(_player.position + _playerMovement.MoveDirection);
            _playerArt.transform.rotation = Quaternion.Euler(0, _playerArt.transform.rotation.eulerAngles.y, 0);
            _playerAnimationList[0].transform.rotation = Quaternion.Euler(0, _playerAnimationList[0].transform.rotation.eulerAngles.y, 0);
            
            _playerFloorReticle.transform.LookAt(_player.position + _playerMovement.MoveDirection);
        }

        _playerAnimationStateMachine?.Update();
    }

    void FixedUpdate()
    {
        _playerAnimationStateMachine?.FixedUpdate();
    }

}

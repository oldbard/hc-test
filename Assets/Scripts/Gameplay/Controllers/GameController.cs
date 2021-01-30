using UnityEngine;

namespace Gameplay.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] PlayerController[] _players;
        [SerializeField] PathManager _pathManager;
        [SerializeField] float _distanceToChangeNode = 0.3f;

        bool _levelIsRunning;

        void Start()
        {
            _levelIsRunning = true;
            for (var i = 0; i < _players.Length; i++)
            {
                var player = _players[i];
                
                player.Init(i);
                UpdatePlayerTarget(player);
            }
        }

        void Update()
        {
            if(!_levelIsRunning) return;

            foreach(var player in _players)
            {
                UpdatePlayerPath(player);
            }
        }

        void UpdatePlayerPath(PlayerController player)
        {
            var diff = player.Position - _pathManager.CurrentTargetPos(player.Id);

            if (diff.sqrMagnitude <= _distanceToChangeNode * _distanceToChangeNode)
            {
                if (_pathManager.IsLastTarget(player.Id))
                {
                    _levelIsRunning = false;
                    Debug.Log("Reached destination!");
                    player.CompletedRun();
                }
                else
                {
                    _pathManager.SkipToNextTarget(player.Id);
                    UpdatePlayerTarget(player);
                }
            }
        }

        void UpdatePlayerTarget(PlayerController player)
        {
            player.SetTargetNode(_pathManager.CurrentTargetPos(player.Id));
        }
    }
}
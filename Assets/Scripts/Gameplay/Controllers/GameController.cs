using System;
using UnityEngine;

namespace Gameplay.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] PlayerController[] _players;
        [SerializeField] PathManager _pathManager;
        [SerializeField] float _distanceToChangeNode = 0.3f;

        public Action<bool> GameOver;

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
            if (!_levelIsRunning) return;

            foreach (var player in _players)
            {
                if (!_levelIsRunning) return;

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
                    Debug.Log("Reached destination!");
                    EndGame(player.Id);
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

        void EndGame(int winnerId)
        {
            _levelIsRunning = false;

            foreach (var player in _players)
            {
                player.CompletedRun(winnerId);
            }

            GameOver?.Invoke(winnerId == 1);
        }
    }
}
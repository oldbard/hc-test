using UnityEngine;

namespace Gameplay.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] PlayerController _player;
        [SerializeField] PathManager _pathManager;
        [SerializeField] float _distanceToChangeNode = 0.3f;

        void Start()
        {
            UpdatePlayerTarget();
        }

        void Update()
        {
            var diff = _player.Position - _pathManager.CurrentTargetPos;

            //        Debug.Log($"Distance: {diff.sqrMagnitude}, Distance to Change: {_distanceToChangeNode * _distanceToChangeNode}");

            if (diff.sqrMagnitude <= _distanceToChangeNode * _distanceToChangeNode)
            {
                if (_pathManager.IsLastTarget)
                {
                    Debug.Log("Reached destination!");
                }
                else
                {
                    _pathManager.SkipToNextTarget();
                    UpdatePlayerTarget();
                }
            }
        }

        void UpdatePlayerTarget()
        {
            _player.SetTargetNode(_pathManager.CurrentTargetPos);
        }
    }
}
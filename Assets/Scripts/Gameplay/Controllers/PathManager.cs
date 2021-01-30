using System;
using UnityEngine;

namespace Gameplay.Controllers
{
    public class PathManager : MonoBehaviour
    {
        [Serializable]
        class PathNodes
        {
            [SerializeField] Transform[] _nodes;
            int _currentTarget;

            public int CurrentTarget
            {
                get => _currentTarget;
            }

            public int Length
            {
                get => _nodes.Length;
            }

            public bool IsLastTarget
            {
                get => _currentTarget == _nodes.Length - 1;
            }

            public void SkipToNextTarget()
            {
                _currentTarget++;
                if (_currentTarget >= _nodes.Length)
                {
                    throw new Exception("Ops! Level should have ended!");
                }
            }

            public Transform GetNode()
            {
                return _nodes[_currentTarget];
            }

            public Transform GetNode(int idx)
            {
                return _nodes[idx];
            }
        }

        [SerializeField] PathNodes[] _pathNodes;


        public Vector3 CurrentTargetPos(int playerId)
        {
            return _pathNodes[playerId].GetNode().position;
        }

        public bool IsLastTarget(int playerId)
        {
            return _pathNodes[playerId].IsLastTarget;
        }

        public void SkipToNextTarget(int playerId)
        {
            _pathNodes[playerId].SkipToNextTarget();
        }

        void OnDrawGizmos()
        {
            if(!Application.isPlaying) return;

            for(var i = 0; i < _pathNodes.Length; i++)
            {
                var playerNodes = _pathNodes[i];

                for(var j = 0; j < playerNodes.Length; j++)
                {
                    var node = playerNodes.GetNode(j);
                    Gizmos.color = j == playerNodes.CurrentTarget ? Color.green : Color.blue;
                    Gizmos.DrawSphere(node.transform.position, 1f);
                }
            }
        }
    }
}
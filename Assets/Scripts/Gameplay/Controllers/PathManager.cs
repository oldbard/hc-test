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

            public int Length
            {
                get => _nodes.Length;
            }

            public Transform GetNode(int id)
            {
                return _nodes[id];
            }
        }

        [SerializeField] PathNodes[] _pathNodes;

        int[] _currentTarget = new int[2];

        public Vector3 CurrentTargetPos(int playerId)
        {
            return _pathNodes[playerId].GetNode(_currentTarget[playerId]).position;
        }

        public bool IsLastTarget(int playerId)
        {
            return _currentTarget[playerId] == _pathNodes[playerId].Length - 1;
        }

        public void SkipToNextTarget(int playerId)
        {
            _currentTarget[playerId]++;
            if (_currentTarget[playerId] >= _pathNodes[playerId].Length)
            {
                throw new System.Exception("Ops! Level should have ended!");
            }
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
                    Gizmos.color = j == _currentTarget[i] ? Color.green : Color.blue;
                    Gizmos.DrawSphere(node.transform.position, 1f);
                }
            }
        }
    }
}
using UnityEngine;

namespace Gameplay.Controllers
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] Transform[] _pathNodes;

        int _currentTarget = 0;

        public Vector3 CurrentTargetPos
        {
            get => _pathNodes[_currentTarget].transform.position;
        }

        public bool IsLastTarget
        {
            get => _currentTarget == _pathNodes.Length - 1;
        }

        public void SkipToNextTarget()
        {
            _currentTarget++;
            if (_currentTarget >= _pathNodes.Length)
            {
                throw new System.Exception("Ops! Level should have ended!");
            }
        }

        void OnDrawGizmos()
        {
            for (var i = 0; i < _pathNodes.Length; i++)
            {
                var node = _pathNodes[i];

                Gizmos.color = i == _currentTarget ? Color.green : Color.blue;
                Gizmos.DrawSphere(node.transform.position, 1f);
            }
        }
    }
}
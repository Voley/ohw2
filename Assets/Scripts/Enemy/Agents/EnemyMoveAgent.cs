using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        public bool IsReached
        {
            get { return this.isReached; }
        }

        [SerializeField] private MoveComponent moveComponent;

        private Vector2 destination;

        private bool isReached;

        private void OnEnable()
        {
            FindObjectOfType<GameManager>().AddListener(this);
        }

        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
            this.isReached = false;
        }

        public void FixedUpdateGame()
        {
            if (this.isReached) {
                return;
            }

            var vector = this.destination - (Vector2)this.transform.position;
            if (vector.magnitude <= 0.25f) {
                this.isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this.moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}
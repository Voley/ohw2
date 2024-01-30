using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour, IGameFixedUpdateListener, IGamePrepareListener, IGameStartListener {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;

        public void PrepareGame()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
            character.gameObject.SetActive(false);
        }

        public void StartGame()
        {
            character.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (this.character != null) {
                this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
            }
        }

        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();

        public void FixedUpdateGame()
        {
            if (this._fireRequired) {
                this.OnFlyBullet();
                this._fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }

    }
}
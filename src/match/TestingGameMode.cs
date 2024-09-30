using UnityEngine;

namespace rose.row.match
{
    public class TestingGameMode : GameModeBase
    {
        public override void ActorDied(Actor actor, Vector3 position, bool wasSilentKill)
        {
            base.ActorDied(actor, position, wasSilentKill);
        }

        public override void CapturePointChangedOwner(CapturePoint capturePoint, int oldOwner, bool initialOwner)
        {
            base.CapturePointChangedOwner(capturePoint, oldOwner, initialOwner);
        }

        public override bool Equals(object other)
        {
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override WeaponManager.LoadoutSet GetLoadout(Actor actor)
        {
            return base.GetLoadout(actor);
        }

        public override string GetName()
        {
            return GameModes.k_TestingGameMode;
        }

        public override int ModifyOrderPriority(Order order, int currentPriority)
        {
            return base.ModifyOrderPriority(order, currentPriority);
        }

        public override void OnSpawnWave()
        {
            base.OnSpawnWave();
        }

        public override void OnSquadSpawned(Squad squad)
        {
            base.OnSquadSpawned(squad);
        }

        public override void OnSurrender()
        {
            base.OnSurrender();
        }

        public override void PlayerAcceptedLoadoutFirstTime()
        {
            base.PlayerAcceptedLoadoutFirstTime();
        }

        public override void SetupOrders()
        {
            base.SetupOrders();
        }

        public override bool ShowRemainingTimeToPlayerSpawn()
        {
            return base.ShowRemainingTimeToPlayerSpawn();
        }

        public override void StartGame()
        {
            base.StartGame();
        }

        public override int TimeToPlayerRespawn()
        {
            return base.TimeToPlayerRespawn();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Win(int winningTeam)
        {
            base.Win(winningTeam);
        }

        private void Awake()
        {
        }
    }
}
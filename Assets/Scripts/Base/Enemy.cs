using System.Linq;

public class Enemy : Creature {

    private Pathfinding pathFinder;
    private bool forcePathRecaculation = false;

    private new void Start() {
        base.Start();
        pathFinder = new Pathfinding(GetMovePossibilities);
    }

    private TilePos[] GetMovePossibilities(TilePos move) {
        return AreaGenerator.GenerateSphericalArea(move, 1);
    }

    private TilePos[] GetAttackPossibilities() {
        return AreaGenerator.GenerateSphericalArea(GetPos(), 1);
    }

    public override void TakeDamage(int d) {
        health -= d;
        if(health <= 0) {
            GameMaster.instance.UnregisterEnemy(this);
            Destroy(this.gameObject);
        }
    }

    public void PerformTurn() {
        TilePos[] attackTiles = GetAttackPossibilities();
        Player player = GameMaster.instance.GetPlayer();
        TilePos playerPosition = player.GetPos();

        if(attackTiles.Contains(playerPosition)) {
            doRecall = true;
            forcePathRecaculation = true;

            player.TakeDamage(damage);

            GameMaster.instance.UnregisterEnemy(this);
            Move(playerPosition, player);
            GameMaster.instance.RegisterNewEnemy(this);

            return;
        }


        TilePos nextMove = pathFinder.GetNextMove(GetPos(), playerPosition, ref forcePathRecaculation);
        if(nextMove == null)
            return;

        GameMaster.instance.UnregisterEnemy(this);
        Move(nextMove, null);
        GameMaster.instance.RegisterNewEnemy(this);
    }
}
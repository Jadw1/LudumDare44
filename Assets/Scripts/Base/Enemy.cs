using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Enemy : Creature {

    private static Random random = new Random();

    private Pathfinding pathFinder;
    private bool forcePathRecaculation = false;

    [SerializeField]
    private GameObject[] prefabs;

    private new void Start() {
        base.Start();
        pathFinder = new Pathfinding(GetMovePossibilities);
    }

    public void Reset() {
        Start();
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

            TileEntity ent = GameMaster.instance.GetTileEntity(this.position) as RealItem;
            if (ent == null) {
                GameObject prefab = prefabs[random.Next(prefabs.Length)];

                if (prefab != null) {
                    GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
                    RealItem item = obj.GetComponent<RealItem>();
                    item.Start();
                    GameMaster.instance.RegisterNewItem(item);
                }
            }

            Destroy(this.gameObject);
        }
    }

    public void PerformTurn() {
        TilePos[] attackTiles = GetAttackPossibilities();
        Player player = GameMaster.instance.GetPlayer();
        TilePos playerPosition = player.GetPos();

        if(TilePos.CalculateDistance(playerPosition, position) > 15) {
            TilePos[] randomMoves = GetMovePossibilities(position);
            int xd = 0;
            TilePos mv = randomMoves[random.Next(randomMoves.Length)];
            while(GameMaster.instance.IsEnemyThere(mv) && xd < 10) {
                mv = randomMoves[random.Next(randomMoves.Length)];
            }
            if(GameMaster.instance.IsEnemyThere(mv))
                return;

            GameMaster.instance.UnregisterEnemy(this);
            Move(mv, null);
            GameMaster.instance.RegisterNewEnemy(this);
            return;
        }


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
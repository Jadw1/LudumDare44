public interface IAbility {
    void Execute(TilePos pos);
    TilePos[] GetValidTiles();
}

public interface IAbility {
    void Execute(TilePos pos, TileEntity entity);
    TilePos[] GetValidTiles();
    TilePos GetRelativeTile();
}

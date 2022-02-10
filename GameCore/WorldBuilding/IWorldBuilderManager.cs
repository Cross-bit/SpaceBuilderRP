public interface IWorldBuilderManager
{
    void AddBlockToWorld(Settings.Blocks_types blockToPlaceType);
    void RemoveBlockFromWorld(SymetricBlock blockToRemove);
}
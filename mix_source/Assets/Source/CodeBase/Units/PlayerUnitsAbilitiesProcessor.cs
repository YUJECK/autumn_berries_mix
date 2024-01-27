using autumn_berries_mix.Grid;
using autumn_berries_mix.Source.CodeBase.Scenes;

namespace autumn_berries_mix.Units
{
    public class PlayerUnitsAbilitiesProcessor : SelectedTileProcessor
    {
        private GameplayScene _scene;

        public PlayerUnitsAbilitiesProcessor(GameplayScene scene)
        {
            _scene = scene;
        }

        public override void ProcessPointedTile(GridTile tile)
            => ProcessTileWithFlag(tile, false);

        public override void ProcessSelectedTile(GridTile tile)
            => ProcessTileWithFlag(tile, true);

        private void ProcessTileWithFlag(GridTile tile, bool flag)
        {
            foreach (var unit in _scene.PlayerUnitsPull)
            {
                foreach (var ability in unit.PlayerAbilitiesPull)
                {
                    ability.OnTilePointed(tile, flag);

                    if (tile.Empty)
                    {
                        ability.OnEmptyTilePointed(tile, flag);
                    }

                    if (tile.TileStuff != null)
                    {
                        ability.OnEntityPointed(tile.TileStuff, flag);

                        if (tile.TileStuff.TryGetComponent(out Unit unitOnTile))
                        {
                            ability.OnUnitPointed(unitOnTile, flag);

                            switch (unitOnTile)
                            {
                                case PlayerUnit playerUnit:
                                    ability.OnPlayerUnitPointed(playerUnit, flag);
                                    break;
                                case EnemyUnit enemyUnit:
                                    ability.OnEnemyUnitPointed(enemyUnit, flag);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
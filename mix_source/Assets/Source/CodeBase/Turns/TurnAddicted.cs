namespace autumn_berries_mix.Turns
{
    public interface ITurnAddicted
    {
        void OnPlayerTurn(PlayerTurn turn);

        void OnEnemyTurn(EnemyTurn turn);
    }
}
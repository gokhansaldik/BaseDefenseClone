namespace Interface
{
    public interface IHealth
    {
         bool IsDead { get; }
         void TakeDamage(int damage);
    }
}
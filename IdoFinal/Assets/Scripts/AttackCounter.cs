public class AttackCounter
{
    private int counter;

    public int CurrentCounter { get => counter; }

    public void CountAttacks()
    {
        counter++;
        if (counter >= 11)
        {
            counter = 0;
        }
    }
}

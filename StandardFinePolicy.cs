public class StandardFinePolicy : IFinePolicy
{
    private readonly decimal _dailyFine;

    public StandardFinePolicy(decimal dailyFine)
    {
        _dailyFine = dailyFine;
    }

    public decimal Calculate(int daysLate)
    {
        if (daysLate <= 0)
        {
            return 0m;
        }

        return daysLate * _dailyFine;
    }
}
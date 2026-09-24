public class PremiumMember : Member
{
    public PremiumMember(string membershipId, string name)
        : base(membershipId, name)
    {
    }

    public override int MaxActiveLoans => 10;

    public override decimal FineMultiplier => 0.5m;
}
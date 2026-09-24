public class StandardMember : Member
{
    public StandardMember(string membershipId, string name)
        : base(membershipId, name)
    {
    }

    public override int MaxActiveLoans => 3;

    public override decimal FineMultiplier => 1.0m;
}
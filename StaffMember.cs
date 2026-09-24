public class StaffMember : Member
{
    public StaffMember(string membershipId, string name)
        : base(membershipId, name)
    {
    }

    public override int MaxActiveLoans => int.MaxValue;

    public override decimal FineMultiplier => 0.0m;
}
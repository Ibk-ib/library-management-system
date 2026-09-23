public interface IReservable
{
    bool IsReserved { get; }
    string? ReservedFor { get; }

    void Reserve(Member member);
    void CancelReservation();
}
public class ReservationQueue
{
    private readonly Queue<Member> _members = new();

    public int Count => _members.Count;

    public void Enqueue(Member member)
    {
        _members.Enqueue(member);
    }

    public Member? Peek()
    {
        if (_members.Count == 0)
        {
            return null;
        }

        return _members.Peek();
    }

    public Member? Dequeue()
    {
        if (_members.Count == 0)
        {
            return null;
        }

        return _members.Dequeue();
    }
}
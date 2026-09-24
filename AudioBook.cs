public class AudioBook : LibraryItem, IReservable, IDigital
{
    public string Narrator { get; }
    public int DurationMinutes { get; }
    public long FileSizeMb { get; }
    public string DownloadUrl { get; }

    public AudioBook(
        string title,
        string narrator,
        int durationMinutes,
        int publicationYear,
        long fileSizeMb,
        string downloadUrl
    ) : base(title, publicationYear, new StandardFinePolicy(0.75m))
    {
        Narrator = narrator;
        DurationMinutes = durationMinutes;
        FileSizeMb = fileSizeMb;
        DownloadUrl = downloadUrl;
    }

    public override string ItemType => "AudioBook";

    public override int LoanPeriodDays => 14;

    public override string Describe()
    {
        return $"{base.Describe()} — narrated by {Narrator}, {DurationMinutes} min";
    }
}
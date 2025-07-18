namespace Movies.Infrastructure.Data.raw
{
    public class CsvRecord
    {
        public int Rank { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Actors { get; set; } = null!;
        public string Year { get; set; } = null!;
        public int Runtime { get; set; }
        public double Rating { get; set; }
        public double Votes { get; set; }
        public string Revenue { get; set; } = null!;
        public string Metascore { get; set; } = null!;
    }
}

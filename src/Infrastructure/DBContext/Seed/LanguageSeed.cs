using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBContext.Seed;

public static class LanguageSeed
{
    public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.Languages.AnyAsync(cancellationToken))
        {
            var languages = new List<Language>
            {
                new() { Name = "English", ISO2Code = "EN", ISO3Code = "ENG", ISONumeric = "001" },
                new() { Name = "Spanish", ISO2Code = "ES", ISO3Code = "SPA", ISONumeric = "002" },
                new() { Name = "French", ISO2Code = "FR", ISO3Code = "FRA", ISONumeric = "003" },
                new() { Name = "German", ISO2Code = "DE", ISO3Code = "DEU", ISONumeric = "004" },
                new() { Name = "Italian", ISO2Code = "IT", ISO3Code = "ITA", ISONumeric = "005" },
                new() { Name = "Portuguese", ISO2Code = "PT", ISO3Code = "POR", ISONumeric = "006" },
                new() { Name = "Russian", ISO2Code = "RU", ISO3Code = "RUS", ISONumeric = "007" },
                new() { Name = "Chinese", ISO2Code = "ZH", ISO3Code = "CHN", ISONumeric = "008" },
                new() { Name = "Japanese", ISO2Code = "JA", ISO3Code = "JPN", ISONumeric = "009" },
                new() { Name = "Korean", ISO2Code = "KO", ISO3Code = "KOR", ISONumeric = "010" },
                new() { Name = "Arabic", ISO2Code = "AR", ISO3Code = "ARA", ISONumeric = "011" },
                new() { Name = "Hindi", ISO2Code = "HI", ISO3Code = "HIN", ISONumeric = "012" },
                new() { Name = "Bengali", ISO2Code = "BN", ISO3Code = "BEN", ISONumeric = "013" },
                new() { Name = "Turkish", ISO2Code = "TR", ISO3Code = "TUR", ISONumeric = "014" },
                new() { Name = "Dutch", ISO2Code = "NL", ISO3Code = "NLD", ISONumeric = "015" },
                new() { Name = "Polish", ISO2Code = "PL", ISO3Code = "POL", ISONumeric = "016" },
                new() { Name = "Thai", ISO2Code = "TH", ISO3Code = "THA", ISONumeric = "017" },
                new() { Name = "Greek", ISO2Code = "EL", ISO3Code = "GRE", ISONumeric = "018" },
                new() { Name = "Filipino", ISO2Code = "TL", ISO3Code = "TGL", ISONumeric = "019" },
                new() { Name = "Serbian", ISO2Code = "SR", ISO3Code = "SRB", ISONumeric = "020" },
                new() { Name = "Bulgarian", ISO2Code = "BG", ISO3Code = "BGR", ISONumeric = "021" },
                new() { Name = "Norwegian", ISO2Code = "NO", ISO3Code = "NOR", ISONumeric = "022" },
                new() { Name = "Slovenian", ISO2Code = "SL", ISO3Code = "SLV", ISONumeric = "023" },
                new() { Name = "Malay", ISO2Code = "MS", ISO3Code = "MSA", ISONumeric = "024" },
                new() { Name = "Danish", ISO2Code = "DA", ISO3Code = "DAN", ISONumeric = "025" },
                new() { Name = "Indonesian", ISO2Code = "ID", ISO3Code = "IND", ISONumeric = "026" },
                new() { Name = "Hungarian", ISO2Code = "HU", ISO3Code = "HUN", ISONumeric = "027" },
                new() { Name = "Romanian", ISO2Code = "RO", ISO3Code = "RON", ISONumeric = "028" },
                new() { Name = "Hebrew", ISO2Code = "HE", ISO3Code = "HEB", ISONumeric = "029" },
                new() { Name = "Vietnamese", ISO2Code = "VI", ISO3Code = "VIE", ISONumeric = "030" },
                new() { Name = "Ukrainian", ISO2Code = "UK", ISO3Code = "UKR", ISONumeric = "031" },
                new() { Name = "Czech", ISO2Code = "CS", ISO3Code = "CES", ISONumeric = "032" },
                new() { Name = "Croatian", ISO2Code = "HR", ISO3Code = "HRV", ISONumeric = "033" }
            };

            await context.Languages.AddRangeAsync(languages, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}

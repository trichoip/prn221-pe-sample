using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class CorrespondingAuthor
    {
        [RegularExpression(@"AU\d{4}", ErrorMessage = "format: AUxxxx (x is digit)")]
        public string AuthorId { get; set; } = null!;
        [StringLength(100, MinimumLength = 6)]
        public string AuthorName { get; set; } = null!;

        [YearRange(ErrorMessage = " 2023 or > 1901")]
        public DateTime AuthorBirthday { get; set; }
        public string Bio { get; set; } = null!;
        public string Skills { get; set; }
        public int InstitutionId { get; set; }

        public virtual InstitutionInformation? Institution { get; set; }

    }

    public class YearRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            DateTime date = Convert.ToDateTime(value);

            return date.Year > 1901 || date.Year == 2023;
        }
    }

}

using System.ComponentModel.DataAnnotations;

namespace GoodToHave.Data;

public class Account
{
    public int Id { get; set; }

    [StringLength(20)]
    public string AccountNo { get; set; }

    [Required]
    public decimal Balance { get; set; }
}
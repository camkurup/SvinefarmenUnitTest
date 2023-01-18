using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvinefarmenUnitTest.Model;

public partial class Lightlog
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "int")]
    public int? Leveloflight { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Timeoflog { get; set; }

    [Column(TypeName = "int")]
    public int? Lightlevelinstable { get; set; }
}

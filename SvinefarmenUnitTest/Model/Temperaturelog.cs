using System;
using System.Collections.Generic;

namespace SvinefarmenUnitTest.Model;

public partial class Temperaturelog
{
    public int Id { get; set; }

    public int? Temperature { get; set; }

    public DateTime? Timeoflog { get; set; }

    public bool? Lighton { get; set; }
}

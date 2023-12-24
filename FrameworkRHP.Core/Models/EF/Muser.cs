using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrameworkRHP.Core.Models.EF;

public partial class Muser
{
    [Key]
    public int Intuserid { get; set; }

    public string Txtusername { get; set; } = null!;

    public string Txtpassword { get; set; } = null!;

    public string Txtfullname { get; set; } = null!;

    public bool? Bitactive { get; set; }

    public DateTime? Dtinserted { get; set; } = null!;

    public string? Txtinsertedby { get; set; }

    public DateTime? Dtupdated { get; set; } = null!;

    public string? Txtupdated { get; set; }
    public virtual ICollection<Muserrole> Muserroles { get; set; } = new List<Muserrole>();
}

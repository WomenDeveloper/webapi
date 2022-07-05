using System;
using System.Collections.Generic;

namespace WebAPI_DB.Models
{
    public partial class Kategoriler
    {
        public Kategoriler()
        {
            Filmlers = new HashSet<Filmler>();
        }

        public int KatId { get; set; }
        public string KategoriAdi { get; set; } = null!;

        public virtual ICollection<Filmler>? Filmlers { get; set; }
    }
}

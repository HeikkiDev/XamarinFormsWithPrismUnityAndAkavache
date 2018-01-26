using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForms_PrismExample.Models
{
    public abstract class EntityBase
    {
        [PrimaryKey]
        public int id { get; set; }
    }
}

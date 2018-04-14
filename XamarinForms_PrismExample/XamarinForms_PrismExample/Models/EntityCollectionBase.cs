using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForms_PrismExample.Models
{
    public abstract class EntityCollectionBase<T> where T : EntityBase
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<T> results { get; set; }
    }
}

namespace Shop.Web.Entities
{
    using System;
    public class Country : IEntity
    {
        public int Id { get; set ; }

        public String Name { get; set; }
    }
}

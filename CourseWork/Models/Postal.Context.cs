//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PostalService.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PostalServiceEntities : DbContext
    {
        public PostalServiceEntities()
            : base("name=PostalServiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Postman> Postmans { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<PubType> PubTypes { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<AddressesView> AddressesViews { get; set; }
        public virtual DbSet<PublicationsView> PublicationsViews { get; set; }
        public virtual DbSet<SubscribersView> SubscribersViews { get; set; }
        public virtual DbSet<PostmansAddView> PostmansAddViews { get; set; }
        public virtual DbSet<PostmansView> PostmansViews { get; set; }
        public virtual DbSet<SubscribersAddView> SubscribersAddViews { get; set; }
        public virtual DbSet<SubscriptionsView> SubscriptionsViews { get; set; }
    
        public virtual ObjectResult<AddressPostman_Result> AddressPostman(string street, string building, Nullable<int> apartment)
        {
            var streetParameter = street != null ?
                new ObjectParameter("street", street) :
                new ObjectParameter("street", typeof(string));
    
            var buildingParameter = building != null ?
                new ObjectParameter("building", building) :
                new ObjectParameter("building", typeof(string));
    
            var apartmentParameter = apartment.HasValue ?
                new ObjectParameter("apartment", apartment) :
                new ObjectParameter("apartment", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AddressPostman_Result>("AddressPostman", streetParameter, buildingParameter, apartmentParameter);
        }
    
        public virtual int EachPubAmount()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EachPubAmount");
        }
    
        public virtual ObjectResult<Nullable<int>> PostmansAmount()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PostmansAmount");
        }
    
        public virtual int SubscribersNewspapers(string surname, string name, string patronymic)
        {
            var surnameParameter = surname != null ?
                new ObjectParameter("surname", surname) :
                new ObjectParameter("surname", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var patronymicParameter = patronymic != null ?
                new ObjectParameter("patronymic", patronymic) :
                new ObjectParameter("patronymic", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SubscribersNewspapers", surnameParameter, nameParameter, patronymicParameter);
        }
    
        public virtual int SubscribesAverageTerms()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SubscribesAverageTerms");
        }
    
        public virtual int TopPubsDistrict()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TopPubsDistrict");
        }
    
        public virtual int SubscribtionAverageTerms()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SubscribtionAverageTerms");
        }
    }
}

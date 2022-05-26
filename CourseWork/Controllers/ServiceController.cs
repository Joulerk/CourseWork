using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using PostalService.Models;

namespace PostalService.Controllers
{
    public class ServiceController
	{
		// связь с базой данных
		private PostalServiceEntities _postalDb;

		public PostalServiceEntities PostalDB
        {
			get => _postalDb;
			set => _postalDb = value;
        }

		#region Конструкторы

		public ServiceController() : this(new PostalServiceEntities()) { }

		public ServiceController(PostalServiceEntities postalDb)
		{
			_postalDb = postalDb;

		}

		#endregion

		#region Запросы для вывода 

		// таблица сведений о подписичках

		public List<SubscribersView> GetSubscribersView() => _postalDb.SubscribersViews.ToList();

		// таблица сведений о представлении почтальонах
		public List<PostmansView> GetPostmansView() => _postalDb.PostmansViews.ToList();

		// таблица сведений о представлении почтальонах
		public List<PostmansAddView> GetPostmansAddView() => _postalDb.PostmansAddViews.ToList();

		// таблица сведений о улицах
		public List<Street> GetStreets() => _postalDb.Streets.ToList();

		// таблица сведений о персонах
		public List<Person> GetPersons()=>_postalDb.Persons.ToList();

		// таблица сведений о подписках
		public List<SubscriptionsView> GetSubscriptionsView() => _postalDb.SubscriptionsViews.ToList();

		// таблица сведений о изданиях
		public List<PublicationsView> GetPublications() => _postalDb.PublicationsViews.ToList();

		// таблица сведений о типах
		public List<PubType> GetPubType() => _postalDb.PubTypes.ToList();

		// таблица сведений о районах
		public List<AddressesView> GetAdressesView() => _postalDb.AddressesViews.ToList();

		// таблица сведений о подписчиках(ФамилияИО)
		public List<SubscribersAddView> GetSubscribersAddView() => _postalDb.SubscribersAddViews.ToList();

		// таблица сведений о изданиях
		public List<PublicationsView> GetPublicationsViews() => _postalDb.PublicationsViews.ToList();



		#endregion



	}
}

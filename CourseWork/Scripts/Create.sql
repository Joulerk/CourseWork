drop table if exists dbo.Subscriptions;
drop table if exists dbo.Publications;
drop table if exists dbo.PubTypes;
drop table if exists dbo.Subscribers;
drop table if exists dbo.Addresses;
drop table if exists dbo.Districts;
drop table if exists dbo.Sectors;
drop table if exists dbo.Streets;
drop table if exists dbo.Postmans;
drop table if exists dbo.Persons;

-- таблица личных данных
create table dbo.Persons (
	Id          int          not null primary key identity (1, 1),
	[Name]      nvarchar(60) not null,    -- Имя персоны
	Surname     nvarchar(60) not null,    -- Фамилия персоны
	Patronymic  nvarchar(60) not null,    -- Отчество персоны
);
go

-- таблица данных почтальонов
create table dbo.Postmans (
	Id          int		not null primary key identity (1, 1),
	IdPerson	int		not null,		  -- данные почтальона

	constraint FK_Postmans_Persons foreign key (IdPerson) references dbo.Persons(Id)
);
go

-- таблица названий улиц
create table dbo.Streets (
	Id          int				not null primary key identity (1, 1),
	Name		nvarchar(50)	not null,
);
go


-- Участок
create table dbo.Sectors (
	Id		int					not null primary key identity (1,1),
	Name	nvarchar(50)		not null, -- Название участка
);
go

-- таблица соотношений почтальонов к участкам (у нескольких участков может быть один почтальон)
create table dbo.Districts (
	Id          int				not null primary key identity (1, 1),
	IdPostman	int				not null,	-- id, ключ к таблице почтальонов
	IdSector    int				not null    -- id, ключ к таблице участков

	constraint FK_Disctricts_Postmans foreign key (IdPostman) references dbo.Postmans(Id),
	constraint FK_Disctricts_Sectors  foreign key (IdSector)  references dbo.Sectors(Id)
);
go




-- таблица адресов (справочник всех адресов улица-дом с указанием участка)
create table dbo.Addresses(
	Id          int			not null primary key identity (1, 1),
	IdStreet	int			not null,    -- id, ключ к таблице списка улиц
	Building	nvarchar(5)	not null,    -- номер дома
	IdDistrict	int			not null	 -- id, ключ к таблице участков

	constraint FK_Addresses_Streets		foreign key (IdStreet) references dbo.Streets(Id),
	constraint FK_Addresses_Districts	foreign key (IdDistrict) references dbo.Districts(Id)
);
go

-- таблица данных подписчиков
create table dbo.Subscribers (
	Id          int		not null primary key identity (1, 1),
	IdPerson	int		not null,   -- id, ключ к таблице персон
	IdAddress	int		not null,	-- id, ключ таблице адресов
	SubAddress	int		not null    -- номер квартиры

	constraint FK_Subscribers_Persons	foreign key (IdPerson) references dbo.Persons(Id),
	constraint FK_Subscribers_Addresses	foreign key (IdAddress) references dbo.Addresses(Id)
);
go

-- таблица типов изданий
create table dbo.PubTypes (
	Id          int				not null primary key identity (1, 1),
	[Name]		nvarchar(10)	not null
);
go

-- таблица изданий
create table dbo.Publications (
	Id          int				not null primary key identity (1, 1),
	IdPubType	int				not null,  -- id, ключ к таблицы типов изданий
	Title		nvarchar(150)	not null,  -- название издания,
	PubIndex	nvarchar(15)	not null,  -- индекс издания
	Price		int				not null   -- стоимость подписки

	constraint FK_Publications_Pubtypes	foreign key (IdPubType) references dbo.PubTypes(Id)
);
go

-- таблица подписок на издания
create table dbo.Subscriptions (
	Id				int		not null primary key identity (1, 1),
	IdSubscriber	int		not null,	-- id, ключ к таблицам подписчиков
	IdPublication	int		not null,	-- id, ключ к таблице изданий
	StartDate		Date	not null,	-- дата начала подписки
	Duration		int		not null	-- длительность подписки

	constraint FK_Subscribes_Subscribers	foreign key (IdSubscriber) references dbo.Subscribers(Id),
	constraint FK_Subscribes_Publications	foreign key (IdPublication) references dbo.Publications(Id)
);
go



-------------------------------------------------------------------------------

drop view if exists AddressesView;
drop view if exists SubscribersView;
drop view if exists SubscribersAddView;
drop view if exists PublicationsView;
drop view if exists SubscriptionsView;
drop view if exists PostmansView;
drop view if exists PostmansAddView;
go

-- Представление списка адресов
create view AddressesView as
select
	Addresses.Id
	, Streets.Name as Street
	, Building
	, IdDistrict 
	, Sectors.Name as SectorName
	, Persons.Surname + N' ' + N' ' + Substring([Persons].Name, 1, 1) 
				  + N'.' + Substring(Persons.Patronymic, 1, 1) 
				  + N'.' as Postman
from
	Addresses join Streets on Addresses.IdStreet = Streets.Id
			  join (Districts join (Postmans join Persons on Persons.Id = Postmans.IdPerson) on Districts.IdPostman = Postmans.Id) 
							  join Sectors on Sectors.Id = Districts.IdSector
				   on Addresses.IdDistrict = Districts.Id;
go

-- Представление списка подписчиков
create view SubscribersView as
select
	Subscribers.Id
	, Persons.Surname
	, Persons.Name
	, Persons.Patronymic
	, Street
	, Building
	, SubAddress as Apartment
	, AddressesView.IdDistrict
	, AddressesView.SectorName
	, AddressesView.Postman
from
	Subscribers join Persons on Persons.Id = Subscribers.IdPerson
				join AddressesView on AddressesView.Id = Subscribers.IdAddress;
go

-- Представление списка подписчиков
create view SubscribersAddView as
select
	Subscribers.Id
	, Persons.Surname + N' ' + N' ' + Substring([Persons].Name, 1, 1) 
				  + N'.' + Substring(Persons.Patronymic, 1, 1) 
				  + N'.' as SurnameNP
from
	Subscribers join Persons on Persons.Id = Subscribers.IdPerson;
go

-- Представление списка почтальонов
create view PostmansView as
select
	Postmans.Id
	, Persons.Surname
	, Persons.Name
	, Persons.Patronymic
from
	Postmans join Persons on Persons.Id = Postmans.IdPerson;
go

-- Представление списка почтальонов для списка
create view PostmansAddView as
select
	Postmans.Id
	, Persons.Surname + N' ' + N' ' + Substring([Persons].Name, 1, 1) 
				  + N'.' + Substring(Persons.Patronymic, 1, 1) 
				  + N'.' as SurnameNP
from
	Postmans join Persons on Persons.Id = Postmans.IdPerson;
go

-- Представление списка изданий
create view PublicationsView as
select
	Publications.Id
	, Title	as PubTitle
	, PubTypes.[Name] as PubType
	, PubIndex
	, Price
from
	Publications join PubTypes on Publications.IdPubType = PubTypes.Id;
go

-- Представление списка подписок
create view SubscriptionsView as
select 
	Subscriptions.Id
	, SubscribersView.Surname
	, SubscribersView.[Name]
	, SubscribersView.Patronymic
	, SubscribersView.Street
	, SubscribersView.Building
	, SubscribersView.Apartment
	, SubscribersView.IdDistrict
	, SubscribersView.SectorName
	, SubscribersView.Postman
	, PublicationsView.PubType 
	, PublicationsView.PubIndex
	, PublicationsView.PubTitle
	, StartDate
	, Duration
	, PublicationsView.Price * Duration as TotalPrice
from
	Subscriptions join SubscribersView on Subscriptions.IdSubscriber = SubscribersView.Id
				  join PublicationsView on Subscriptions.IdPublication = PublicationsView.Id;
go


-------------------------------------------------------------------------------------------
			--- Хранимые процедуры ---

drop procedure if exists EachPubAmount;
drop procedure if exists AddressPostman;
drop procedure if exists SubscribersNewspapers;
drop procedure if exists PostmansAmount;
drop procedure if exists TopPubsDistrict;
drop procedure if exists SubscribtionsAverageTerms;
go


--	Определить наименование и количество экземпляров всех изданий, получаемых отделением связи.
create or alter proc EachPubAmount
as begin
    select
		PubTitle
		,Count(PubTitle) as Amount
	from 
		SubscriptionsView
	group by 
		PubTitle
	order by Amount desc 
end
go

--	По заданному адресу определить фамилию почтальона, обслуживающего подписчика.
create or alter proc AddressPostman 
		@street nvarchar(50),
		@building nvarchar(5),
		@apartment int
as begin
	select
		Surname + N' ' + N' ' + Substring(Name, 1, 1) 
				  + N'.' + Substring(Patronymic, 1, 1) 
				  + N'.' as Subscriber
		,Street
		,Building
		,Apartment
		,Postman
	from
		SubscribersView
	where
		Street = @street and Building = @building and Apartment = @apartment;
end
go


--	Какие газеты выписывает гражданин с указанной фамилией, именем, отчеством?	
create or alter proc SubscribersNewspapers
		@surname     nvarchar(60),
		@name        nvarchar(60),
		@patronymic  nvarchar(60)
as begin
	select
		Surname
		,Name
		,Patronymic
		,PubTitle
	from
		SubscriptionsView
	where 
		Name = @name and Surname = @surname and Patronymic = @patronymic and PubType = N'Газета'
end
go

--	Сколько почтальонов работает в почтовом отделении?
create or alter proc PostmansAmount
as begin
	select
		COUNT(Id) as TotalPostmans
	from
		Postmans;
end
go

--	На каком участке количество экземпляров подписных изданий максимально?
create or alter proc TopPubsDistrict
as begin
	select top(1)
		IdDistrict
		,SectorName
		,COUNT(IdDistrict) as Amount
	from
		SubscriptionsView
	group by 
		IdDistrict, SectorName
	order by Amount desc;
end
go

--	Каков средний срок подписки по каждому изданию?
create or alter proc SubscribtionAverageTerms
as begin
	select
		PubTypes.Name as 'PubType'
		,Publications.Title as 'PubTitle'
		,Publications.PubIndex
		, ISNULL(AVG(Duration),0) as 'AvgDuration'
	from 
		Publications join PubTypes on PubTypes.Id = Publications.IdPubType
			    left join Subscribes on Subscribes.IdPublication = Publications.Id
	group by
		PubTypes.Name,
		Publications.Title,
		Publications.PubIndex
	order by 'AvgDuration' desc
end
go


-- Запуск процедур процедур

-- "нет невыполнению"
set noexec off
go


select * from SubscriptionsView;


-- Процедуры запросов к БД
exec EachPubAmount;

-- Пример с параметром null и без
exec AddressPostman 'пр.Ленинский', '1', 11;
exec AddressPostman 'ул.Куйбышева', '12', null;

exec SubscribersNewspapers N'Яструб',      N'Владимир',	N'Данилович';
exec SubscribersNewspapers N'Мелашенко',   N'Александр',	N'Алексеевич';

exec PostmansAmount;

exec TopPubsDistrict;

exec SubscribtionsAverageTerms;


 


-- ������ �������� ��������

-- "��� ������������"
set noexec off
go


select * from SubscriptionsView;


-- ��������� �������� � ��
exec EachPubAmount;

-- ������ � ���������� null � ���
exec AddressPostman '��.���������', '1', 11;
exec AddressPostman '��.���������', '12', null;

exec SubscribersNewspapers N'������',      N'��������',	N'���������';
exec SubscribersNewspapers N'���������',   N'���������',	N'����������';

exec PostmansAmount;

exec TopPubsDistrict;

exec SubscribtionsAverageTerms;


 


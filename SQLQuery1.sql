use master
drop database if exists WPFShopManagementDB
go

use master
create database WPFShopManagementDB
go

use WPFShopManagementDB
go

create table Roles
(
	RoleId int not null,
	RoleName varchar(255)
	primary key (RoleId)
)

use WPFShopManagementDB
go

create table Accounts
(
	AccountId int not null,
	FullName varchar(255),
	RoleId int not null,
	DateOfBirth date not null,
	PhoneNumber varchar(10),
	HomeAddress varchar(255),
	AccountEmail varchar(255),
	AccountPassword varchar(255),
	AccountStatus bit,
	primary key (AccountId),
	foreign key (RoleId) references Roles(RoleId)
)

use WPFShopManagementDB
go

create table Categories
(
	CategoryId int not null,
	CategoryName nvarchar(255),
	CategoryDescription nvarchar(255),
	primary key (CategoryId)
)

use WPFShopManagementDB
go

create table Products
(
	ProductId int not null,
	ProductName nvarchar(255),
	CalculationUnit nvarchar(50) not null,
	Quantity float not null,
	CategoryId int not null,
	primary key(ProductId),
	foreign key(CategoryId) references Categories(CategoryId)
)

use WPFShopManagementDB
go
insert into Roles(RoleId, RoleName)
values
(1, 'Admin'),
(2, 'Manager'),
(3, 'Staff');

use WPFShopManagementDB
go
insert into Accounts(AccountId, FullName, RoleId, DateOfBirth, PhoneNumber, HomeAddress, AccountEmail, AccountPassword, AccountStatus)
values
(1, 'Administrator', 1, '2024-01-01', '0123456789', '123 Ngo Quyen St., District 12, HCMC', 'admin@shop.us', '0000', 1),
(2, 'Test Manager', 2, '2024-02-02', '0321654987', '234 Ly Thuong Kiet St., Di An, Binh Duong', 'manager@shop.us', '1111', 1),
(3, 'Test Staff', 3, '2024-03-03', '0987654321', '68 Vo Thi Sau St., Go Vap District , HCMC', 'staff@shop.us', '2222', 1)

use WPFShopManagementDB
go
insert into Categories(CategoryId, CategoryName, CategoryDescription)
values
(1, 'Vegetables', 'Fresh vegetables from trusted local farms'),
(2, 'Dairies', 'Fresh vegetables from trusted local farms'),
(3, 'Bread', 'Fresh vegetables from trusted local farms'),
(4, 'Chemicals', 'Fresh vegetables from trusted local farms'),
(5, 'Meat', 'Fresh vegetables from trusted local farms'),
(6, 'Spices', 'Fresh vegetables from trusted local farms')

use WPFShopManagementDB
go
insert into Products(ProductId, ProductName, CalculationUnit, Quantity, CategoryId)
values
(1, 'Pepper', 'Bottle(s)', 100, 6),
(2, 'Detergent', 'Can(s)', 100, 4),
(3, 'Chicken Breast', 'Portion(s)(500grams)', 100, 5),
(4, 'Milk', 'Box(es)', 50, 2),
(5, 'Potato', 'Kilograms', 30, 1),
(6, 'Plain Sandwich', 'Bag(s)', 100, 3)

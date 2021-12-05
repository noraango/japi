--drop database [JAP];
--create database [JAP];

drop table Category;
CREATE TABLE [Category] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Name] nvarchar(200),
  [Level] int,
  [BelongToCategoryId] int,
  CONSTRAINT [FK_Category.BelongToCategoryId]
    FOREIGN KEY ([BelongToCategoryId])
      REFERENCES [Category]([Id])
);

insert into Category(Name,Level,BelongToCategoryId) values
(N'Mẹ - Bé',1,NULL),
(N'Thực phẩm',1,NULL),
(N'Nhà cửa - Đời sống',1,NULL),
(N'Thời trang',1,NULL),
(N'Văn phòng phẩm',1,NULL),
(N'Chăm sóc sắc đẹp',1,NULL),
(N'Sữa',2,1),
(N'Đồ chơi',2,1),
(N'Bát đĩa',2,1),
(N'Quần áo',2,1)

--select * from Category

drop table [ProductStatus];
CREATE TABLE [ProductStatus] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] varchar(10),
  [Name] nvarchar(50),
);

insert into [ProductStatus](Code,Name) values
('P1',N'Đang trưng bày'),
('P2',N'Trong kho')

/*select * from [Product.Status];*/

drop table [ProductPackingMethod]
CREATE TABLE [ProductPackingMethod] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Name] nvarchar(200),
);

insert into [ProductPackingMethod](Name) values
(N'Thùng 24 hộp'),
(N'Thùng 12 hộp'),
(N'Hộp 550ml'),
(N'Hộp 750ml'),
(N'Chai 750ml'),
(N'Chai 1500ml'),
(N'Hộp 20x20x20cm'),
(N'Hộp 30x30x30cm')

/*select * from [Product.PackingMethod];*/


/*tạo table origin*/
drop table [Origin]
CREATE TABLE [Origin] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Name] nvarchar(200),
);

/*insert data cho Origin*/
insert into Origin([Name]) values
(N'Nhật Bản'),
(N'Việt Nam'),
(N'Trung Quốc')

/*test table origin*/
/*select * from Origin*/

/*tạo table Image*/
drop table [ProductImage]
CREATE TABLE [ProductImage] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Name] nvarchar(200),
  [ProductId] int
);
CREATE INDEX [UK] ON  [ProductImage] ([Name]);
insert into ProductImage([Name],[ProductId]) values
('soldier-bu210527141.jpg',1);
select * from ProductImage



/*tạo table product*/
drop table [Product]
CREATE TABLE [Product] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] varchar(50),
  [Name] nvarchar(200),
  [Price] decimal(9,0),
  [Size] varchar(100),
  [Weight] float,
  [Manufacturer] nvarchar(200),
  [ShortDescription] nvarchar(1000),
  [Description] nvarchar(MAX),
  [Brand] nvarchar(200),
  [OriginId] int,
  [PackingMethodId] int,
  [ProductStatusId] int,
  [DisplayImageName] varchar(200),
);
insert into Product(Code,[Name],Price,Size,[Weight],Manufacturer,ShortDescription,[Description],Brand,OriginId,PackingMethodId,ProductStatusId,[DisplayImageName]) values
('P0001',N'Củ hành',50000,'20cm x 20cm x 30cm',500,N'Nông trại vui vẻ',N'Đây là củ hành',N'Đây cũng là củ hành',N'Vui vẻ',2,2,1,'01.jpg'),
('P0002',N'Củ khoai',60000,'20cm x 20cm x 30cm',600,N'Nông trại khoai',N'Đây là củ khoai',N'Đây cũng là củ khoai',N'Khoai',1,1,2,'02.jpg'),
('P0003',N'Củ kiệu',70000,'20cm x 20cm x 30cm',700,N'Nông trại kiệu',N'Đây là củ kiệu',N'Đây cũng là củ kiệu',N'Kiệu',3,3,1,'03.jpg'),
('P0004',N'Củ măng',71000,'20cm x 20cm x 30cm',750,N'Nông trại măng',N'Đây là củ măng',N'Đây cũng là củ măng',N'Măng',2,4,2,'04.jpg'),
('P0005',N'Củ Sắn',74000,'20cm x 20cm x 30cm',810,N'Nông trại sắn',N'Đây là củ sắn',N'Đây cũng là củ sắn',N'Sắn',1,5,1,'05.jpg'),
('P0006',N'Bắp Ngô',73000,'20cm x 20cm x 30cm',800,N'Nông trại ngô',N'Đây là bắp ngô',N'Đây cũng là bắp ngô',N'Ngô',3,6,2,'06.jpg'),
('P0007',N'Rau Muống',75000,'20cm x 20cm x 30cm',820,N'Nông trại rau muống',N'Đây là rau muống',N'Đây cũng là rau muống',N'Rau muống',2,7,1,'07.jpg'),
('P0008',N'Rau Cải',7600,'20cm x 20cm x 30cm',830,N'Nông trại rau cải',N'Đây là rau cải',N'Đây cũng là rau cải',N'Rau cải',1,8,2,'08.jpg'),
('P0009',N'Rau Ngót',77000,'20cm x 20cm x 30cm',840,N'Nông trại rau ngót',N'Đây là rau ngót',N'Đây cũng là rau ngót',N'Rau ngót',3,2,1,'09.jpg'),
('P0010',N'Rau Bắp Cải',78000,'20cm x 20cm x 30cm',850,N'Nông trại bắp cải',N'Đây là rau bắp cải',N'Đây cũng là rau bắp cải',N'Bắp Cải',2,2,2,'10.jpg'),
('P0011',N'Rau Súp Lơ',79000,'20cm x 20cm x 30cm',860,N'Nông trại súp lơ',N'Đây là rau súp lơ',N'Đây cũng là súp lơ',N'Súp lơ',2,2,1,'11.jpg'),
('P0012',N'Quả Bầu',80000,'20cm x 20cm x 30cm',870,N'Nông trại bầu',N'Đây là quả bầu',N'Đây cũng là quả bầu',N'Quả bầu',2,2,2,'12.jpg'),
('P0013',N'Quả Bí',81000,'20cm x 20cm x 30cm',880,N'Nông trại bí',N'Đây là quả bí',N'Đây cũng là cquả bí',N'Quả bí',2,2,1,'13.jpg'),
('P0014',N'Quả Mướp',82000,'20cm x 20cm x 30cm',890,N'Nông trại mướp',N'Đây là quả mướp',N'Đây cũng là quả mướp',N'Mướp',2,2,2,'14.jpg'),
('P0015',N'Quả Bí Dao',83000,'20cm x 20cm x 30cm',900,N'Nông trại bí đao',N'Đây là quả bí đao',N'Đây cũng là quả bí đao',N'Bí Đao',2,2,1,'15.jpg'),
('P0016',N'Quả Đậu',84000,'20cm x 20cm x 30cm',910,N'Nông trại đậu',N'Đây là quả đậu',N'Đây cũng là quả đậu',N'Đậu',2,2,2,'16.jpg'),
('P0017',N'Quả Ổi',85000,'20cm x 20cm x 30cm',920,N'Nông trại ổi',N'Đây là quả ổi',N'Đây cũng là quả ổi',N'Ổi',2,2,1,'01.jpg'),
('P0018',N'Quả Sung',86000,'20cm x 20cm x 30cm',930,N'Nông trại sung',N'Đây là quả sung',N'Đây cũng là quả sung',N'Sung',2,2,2,'02.jpg'),
('P0019',N'Quả Táo',87000,'20cm x 20cm x 30cm',940,N'Nông trại táo',N'Đây là quả táo',N'Đây cũng là quả táo',N'Táo',2,2,1,'03.jpg'),
('P0020',N'Quả Lê',88000,'20cm x 20cm x 30cm',941,N'Nông trại lê',N'Đây là quả lê',N'Đây cũng là quả lê',N'Lê',2,2,2,'04.jpg'),
('P0021',N'Quả Dưa',89000,'20cm x 20cm x 30cm',942,N'Nông trại dưa',N'Đây là quả dưa',N'Đây cũng là quả dưa',N'Dưa',2,2,1,'05.jpg'),
('P0022',N'Quả Nho',90000,'20cm x 20cm x 30cm',943,N'Nông trại nho',N'Đây là quả nho',N'Đây cũng là quả nho',N'Nho',2,2,2,'06.jpg'),
('P0023',N'Quả Dâu Tây',91000,'20cm x 20cm x 30cm',284,N'Nông trại dâu tây',N'Đây là quả dâu tây',N'Đây cũng là quả dâu tây',N'Dây tây',2,1,1,'07.jpg'),
('P0024',N'Quả Cóc',92000,'20cm x 20cm x 30cm',945,N'Nông trại cóc',N'Đây là quả cóc',N'Đây cũng là quả cóc',N'Quả Cóc',2,2,2,'08.jpg'),
('P0025',N'Quả Cherry',93000,'20cm x 20cm x 30cm',946,N'Nông trại cherry',N'Đây là quả cherry',N'Đây cũng là quả cherry',N'Cherry',2,3,1,'09.jpg'),
('P0026',N'Quả Xoài',94000,'20cm x 20cm x 30cm',947,N'Nông trại xoài',N'Đây là quả xoài',N'Đây cũng là quả xoài',N'Quả xoài',2,4,2,'10.jpg'),
('P0027',N'Quả Bơ',95000,'20cm x 20cm x 30cm',948,N'Nông trại bơ',N'Đây là quả bơ',N'Đây cũng là quả bơ',N'Quả bơ',2,5,1,'11.jpg'),
('P0028',N'Quả Sầu Riêng',96000,'20cm x 20cm x 30cm',949,N'Nông trại sầu riêng',N'Đây là quả sầu riêng',N'Đây cũng là quả sầu riêng',N'Sầu riêng',2,6,2,'12.jpg'),
('P0029',N'Quả Mít',97000,'20cm x 20cm x 30cm',950,N'Nông trại mít',N'Đây là quả mít',N'Đây cũng là quả mít',N'Mít',2,7,2,'13.jpg'),
('P0030',N'Quả Nhãn',98000,'20cm x 20cm x 30cm',960,N'Nông trại nhãn',N'Đây là quả nhãn',N'Đây cũng là quả nhãn',N'Nhãn',2,8,2,'14.jpg')

select * from Product;

/*tạo kho hàng*/
drop table [Store];
create table [Store](
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(200),
	[Square] float,
	[Floor] int,
	[Address] nvarchar(500),	
	[VillageId] varchar(20),
	[WardId] varchar(20),
	[DistrictId] varchar(20),
	[ProvinceId] varchar(20),
);
insert into Store([Name],[Square],[Floor],[Address],[VillageId],[WardId],[DistrictId],[ProvinceId]) values
(N'Cửa hàng Mỹ Đình',200,2,N'số 12, ngõ 23, đường Lê Đức Thắng',NULL,NULL,NULL,NULL),
(N'Kho Mỹ Đình',200,2,N'số 12, ngõ 23, đường Lê Đức Thắng',NULL,NULL,NULL,NULL);

select * from [Store]

--storage-item
drop table [StoreProduct];
CREATE TABLE [StoreProduct] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [StoreId] int,
  [ProductId] int,
  [Quantity] int
);
insert into StoreProduct(StoreId,ProductId,Quantity) values
(1,1,100),
(1,2,100),
(1,3,100),
(1,4,100),
(1,5,100),
(1,6,100),
(1,7,100),
(1,8,100),
(1,9,100),
(1,10,100),
(1,11,100),
(1,11,100),
(1,12,100),
(1,13,100),
(1,14,100),
(1,15,100),
(1,16,100),
(1,17,100),
(1,18,100),
(1,19,100),
(1,20,100),
(1,21,100),
(1,22,100),
(1,23,100),
(1,24,100),
(1,25,100),
(1,26,100),
(1,27,100),
(1,28,100),
(1,29,100),
(1,30,100),
(2,1,100),
(2,2,100),
(2,3,100),
(2,4,100),
(2,5,100),
(2,6,100),
(2,7,100),
(2,8,100),
(2,9,100),
(2,10,100),
(2,11,100),
(2,11,100),
(2,12,100),
(2,13,100),
(2,14,100),
(2,15,100),
(2,16,100),
(2,17,100),
(2,18,100),
(2,19,100),
(2,20,100),
(2,21,100),
(2,22,100),
(2,23,100),
(2,24,100),
(2,25,100),
(2,26,100),
(2,27,100),
(2,28,100),
(2,29,100),
(2,30,100);

select * from StoreProduct;

/*tạo table user.role*/

drop table [UserRole];
CREATE TABLE [UserRole] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] varchar(10),
  [Name] nvarchar(50),
);



insert into [UserRole](Code,[Name]) values
('U1','Admin'),
('U2','Seller'),
('U3','Staff'),
('U4','Customer');

select* from [UserRole];

/*tạo table User*/

drop table [User];

CREATE TABLE [User] (
  [UserId] int IDENTITY(1,1) PRIMARY KEY,
  [UserRoleId] int,
  [EncodedPassword] varchar(500),
  [FirstName] nvarchar(50),
  [MiddleName] nvarchar(50),
  [LastName] nvarchar(50),
  [Phone] varchar(10),
  [Email] varchar(100),
  [Address] nvarchar(500),
  [AvatarFilename] varchar(200),
  [WardId] varchar(20),
  [DistrictId] varchar(20),
  [ProvinceId] varchar(20),
  [Status] int
);

insert into [User](UserRoleId,EncodedPassword,LastName,MiddleName,FirstName,Phone,Email,[Address],AvatarFilename,WardId,DistrictId,ProvinceId,[Status]) values 
(1,'123',N'Ngô',N'Thế',N'Anh','0357467491','timer217@gmail.com','Đối diện bến xe buýt','avatar.jpg','04138','125HH','14TTT',1),
(5,'123','Ngo',N'Quang','Anh','086522454','timer218@gmail.com','PhuTho','avatar.jpg','00625','019HH','01TTT',1),
(1,'123','Do',N'Quang','Tung','0763845','tung123@gmail.com','PhuTho','avatar.jpg','00625','019HH','01TTT',1),
(1,'123','Nguyen',N'Quang','Dat','066354845','tung123@gmail.com','PhuTho','avatar.jpg','00625','019HH','01TTT',1),
(1,'123','Nguyen',N'Quang','Khanh','056359548','khanh123@gmail.com','PhuTho','avatar.jpg','00625','019HH','01TTT',1),
(2,'123','Nguyen',N'Quang','Van','046359548','nguyenvan23@gmail.com','PhuTho','avatar.jpg','00625','019HH','01TTT',1),
(3,'123','Hoang',N'Quang','An','036359548','hoangan123@gmail.com','PhuTho','avatar.jpg','00625','019HH','01TTT',1),
(4,'123','Chu',N'Quang','Nam','026359548','chunam23@gmail.com','PhuTho','avatar.jpg','00625','019HH','01TTT',1);

select * from [User]



/*tạo table Cart*/
drop table [Cart];
CREATE TABLE [Cart] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] nvarchar(50),
  [OrderTime] date,
  [Note] nvarchar(1000),
  [UserId] int,
  [OrderStatusId] int,
);
insert into Cart(Code,OrderTime,Note,UserId,OrderStatusId) values
('C01','03-11-2021',N'Đóng gói',1,1),
('C02','04-11-2021',N'Túi',2,2),
('C03','05-11-2021',N'Đựng thùng',3,3),
('C04','07-11-2021',N'Bao',4,4);

select *from Cart

/*tạo table CartItem*/
drop table [CartItem];
CREATE TABLE [CartItem] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [CartId] int,
  [ProductId] int,
  [Quantity] int,
);
insert into CartItem(CartId,ProductId,Quantity)values
(1,6,15),
(2,7,15),
(3,8,15),
(4,9,15),
(2,12,15),
(3,15,15),
(4,16,15),
(4,19,15),
(3,25,15),
(2,24,15),
(1,33,15);
--select * from CartItem where CartId = 1;

/*tạo table Order.Status */
drop table [OrderStatus] ;

CREATE TABLE [OrderStatus] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] nvarchar(10),
  [Name] nvarchar(50),
);
insert into  [OrderStatus](Code,[Name]) values
('O1', N'Chờ xác nhận'),
('O2', N'Chờ lấy hàng'),
('O3', N'Đang giao'),
('O4', N'Đã giao'),
('O5', N'Đã hủy');
select * from OrderStatus

/*tạo table OrderItem*/
drop table [OrderItem];
CREATE TABLE [OrderItem] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [OrderId] int,
  [ProductId] int,
  [Quantity] int,
);
insert into OrderItem(OrderId,ProductId,Quantity)values
(1,1,15),
(1,2,15),
(1,3,15),
(1,4,15);
select * from [OrderItem];



/*tạo table ShippingCompany*/
drop table [ShippingCompany];
CREATE TABLE [ShippingCompany] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Name] nvarchar(50),
  [Phone] varchar(10),
  [Email] varchar(100),
);
insert into ShippingCompany([Name],Phone,Email) values
(N'Giao hàng nhanh','1900636677','cskh@ghn.vn'),
(N'Viettel Post','19008095','support@viettelpost.com.vn'),
(N'Giao hàng tiết kiệm','18006092','cskh@ghtk.vn');
select * from ShippingCompany;

/*tạo table Shipper*/
drop table [Shipper];
CREATE TABLE [Shipper] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [CompanyId] int,
  [Name] nvarchar(50),
  [Phone] varchar(10),
  [Email] varchar(100),
);
insert into Shipper(CompanyId,[Name],Phone,Email)values
(1,N'Nguyễn Văn An','0185662553','nguyenvanan@gmail.com'),
(1,N'Nguyễn Văn An','0258412985','nguyenvanan@gmail.com'),
(2,N'Nguyễn Văn An','0366998500','nguyenvanan@gmail.com'),
(2,N'Nguyễn Văn An','0411557729','nguyenvanan@gmail.com'),
(3,N'Nguyễn Văn An','081296555','nguyenvanan@gmail.com'),
(4,N'Nguyễn Văn An','096877114','nguyenvanan@gmail.com');
select * from Shipper;

/*tạo table Order*/
drop table [Order];
CREATE TABLE [Order] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Guid] varchar(50),
  [UserId] int,
  [WeekendDelivery] bit,
  [EarliestDeliveryDate] date,
  [LatestDeliveryDate] date,
  [Address] nvarchar(500),
  [WardId] varchar(20),
    [DistrictId] varchar(20),
	  [ProvinceId] varchar(20),
  [OrderStatusId] int,
  [ShipperID] int,
  [Price] int,
  [CancelReason] nvarchar(1000),
  [ShopId] int
);
insert into [Order](UserId,Guid,WeekendDelivery,EarliestDeliveryDate,LatestDeliveryDate,[Address],ProvinceId,DistrictId,WardId,OrderStatusId,ShipperID,CancelReason,Price,ShopId) values
(1,'42550d49-5dd3-414e-a496-3beb6613e5bd','True','2021-12-05','2021-12-01',N'số 12, ngõ 23, đường Lê Đức Thắng','00625','019HH','01TTT',1,1,'',0,1);

select * from [Order];


--Product Rating
drop table [ProductRating]
CREATE TABLE [ProductRating] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [ProductId] int,
  [Rating] int,
  [RateTime] datetime,
  [Comment] nvarchar(1000),
  [UserId] int
);
insert into [ProductRating](ProductId,Rating,RateTime,Comment,UserId) values 
(1,3,'01-08-2000',N'Tuyệt vời',1),
(1,4,'01-30-2000',N'Tuyệt vời',1),
(1,3,'01-08-2000',N'Tuyệt vời',1),
(1,5,'01-08-2000',N'Tuyệt vời',1),
(1,3,'01-08-2000',N'Tuyệt vời',1),
(1,4,'01-08-2000',N'Tuyệt vời',1),
(1,5,'01-08-2000',N'Tuyệt vời',1),
(1,2,'01-08-2000',N'Tuyệt vời',1),
(2,4,'01-08-2000',N'Tuyệt vời',1),
(2,5,'01-08-2000',N'Tuyệt vời',1),
(2,4,'01-08-2000',N'Tuyệt vời',1),
(2,3,'01-08-2000',N'Tuyệt vời',1),
(2,4,'01-08-2000',N'Tuyệt vời',1),
(2,2,'01-08-2000',N'Tuyệt vời',1),
(2,4,'01-08-2000',N'Tuyệt vời',1),
(2,5,'01-08-2000',N'Tuyệt vời',1),
(2,2,'01-08-2000',N'Tuyệt vời',1),
(2,4,'01-08-2000',N'Tuyệt vời',1),
(2,5,'01-08-2000',N'Tuyệt vời',1),
(2,2,'01-08-2000',N'Tuyệt vời',2),
(2,4,'01-08-2000',N'Tuyệt vời',2);
select pr.*, (u.LastName + u.FirstName) as UserName from [ProductRating] pr left join [User] u on u.UserId = pr.UserId where pr.ProductId = 2;

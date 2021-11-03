drop database [JAP];
create database [JAP];

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

/*tạo kho hàng*/
drop table [Storage];
create table [Storage](
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
insert into Storage([Name],[Square],[Floor],[Address],[VillageId],[WardId],[DistrictId],[ProvinceId]) values
(N'Kho Mỹ Đình',200,2,N'số 12, ngõ 23, đường Lê Đức Thắng',NULL,NULL,NULL,NULL);
select * from [Storage]

/*tạo table product*/
drop table [Product]
CREATE TABLE [Product] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] varchar(50),
  [Name] nvarchar(200),
  [Price] decimal(9,0),
  [Size] float,
  [Weight] float,
  [Quantity] int,
  [Manufacturer] nvarchar(200),
  [ShortDescription] nvarchar(1000),
  [Description] nvarchar(MAX),
  [Brand] nvarchar(200),
  [OriginId] int,
  [PackingMethodId] int,
  [ProductStatusId] int,
  [DisplayImageName] varchar(200),
  [StorageId] int
);
insert into Product(Code,[Name],Price,Size,[Weight],Quantity,Manufacturer,ShortDescription,[Description],Brand,OriginId,PackingMethodId,ProductStatusId,[DisplayImageName],StorageId) values
('P0001',N'Củ hành',50000,50,500,100,N'Nông trại vui vẻ',N'Đây là củ hành',N'Đây cũng là củ hành',N'Vui vẻ',2,2,1,'soldier-bu210527141.jpg',1),
('P0002',N'Củ khoai',60000,60,600,110,N'Nông trại khoai',N'Đây là củ khoai',N'Đây cũng là củ khoai',N'Khoai',1,1,2,'soldier-bu210527141.jpg',1),
('P0003',N'Củ kiệu',70000,70,700,120,N'Nông trại kiệu',N'Đây là củ kiệu',N'Đây cũng là củ kiệu',N'Kiệu',3,3,1,'soldier-bu210527141.jpg',1),
('P0004',N'Củ măng',71000,75,750,130,N'Nông trại măng',N'Đây là củ măng',N'Đây cũng là củ măng',N'Măng',2,4,2,'soldier-bu210527141.jpg',1),
('P0005',N'Củ Sắn',74000,90,810,150,N'Nông trại sắn',N'Đây là củ sắn',N'Đây cũng là củ sắn',N'Sắn',1,5,1,'soldier-bu210527141.jpg',1),
('P0006',N'Bắp Ngô',73000,80,800,140,N'Nông trại ngô',N'Đây là bắp ngô',N'Đây cũng là bắp ngô',N'Ngô',3,6,2,'soldier-bu210527141.jpg',1),
('P0007',N'Rau Muống',75000,85,820,160,N'Nông trại rau muống',N'Đây là rau muống',N'Đây cũng là rau muống',N'Rau muống',2,7,1,'soldier-bu210527141.jpg',1),
('P0008',N'Rau Cải',7600,91,830,170,N'Nông trại rau cải',N'Đây là rau cải',N'Đây cũng là rau cải',N'Rau cải',1,8,2,'soldier-bu210527141.jpg',1),
('P0009',N'Rau Ngót',77000,92,840,180,N'Nông trại rau ngót',N'Đây là rau ngót',N'Đây cũng là rau ngót',N'Rau ngót',3,2,1,'soldier-bu210527141.jpg',1),
('P0010',N'Rau Bắp Cải',78000,93,850,190,N'Nông trại bắp cải',N'Đây là rau bắp cải',N'Đây cũng là rau bắp cải',N'Bắp Cải',2,2,2,'soldier-bu210527141.jpg',1),
('P0011',N'Rau Súp Lơ',79000,94,860,200,N'Nông trại súp lơ',N'Đây là rau súp lơ',N'Đây cũng là súp lơ',N'Súp lơ',2,2,1,'soldier-bu210527141.jpg',1),
('P0012',N'Quả Bầu',80000,95,870,210,N'Nông trại bầu',N'Đây là quả bầu',N'Đây cũng là quả bầu',N'Quả bầu',2,2,2,'soldier-bu210527141.jpg',1),
('P0013',N'Quả Bí',81000,96,880,220,N'Nông trại bí',N'Đây là quả bí',N'Đây cũng là cquả bí',N'Quả bí',2,2,1,'soldier-bu210527141.jpg',1),
('P0014',N'Quả Mướp',82000,97,890,230,N'Nông trại mướp',N'Đây là quả mướp',N'Đây cũng là quả mướp',N'Mướp',2,2,2,'soldier-bu210527141.jpg',1),
('P0015',N'Quả Bí Dao',83000,98,900,240,N'Nông trại bí đao',N'Đây là quả bí đao',N'Đây cũng là quả bí đao',N'Bí Đao',2,2,1,'soldier-bu210527141.jpg',1),
('P0016',N'Quả Đậu',84000,99,910,250,N'Nông trại đậu',N'Đây là quả đậu',N'Đây cũng là quả đậu',N'Đậu',2,2,2,'soldier-bu210527141.jpg',1),
('P0017',N'Quả Ổi',85000,100,920,260,N'Nông trại ổi',N'Đây là quả ổi',N'Đây cũng là quả ổi',N'Ổi',2,2,1,'soldier-bu210527141.jpg',1),
('P0018',N'Quả Sung',86000,105,930,270,N'Nông trại sung',N'Đây là quả sung',N'Đây cũng là quả sung',N'Sung',2,2,2,'soldier-bu210527141.jpg',1),
('P0019',N'Quả Táo',87000,110,940,280,N'Nông trại táo',N'Đây là quả táo',N'Đây cũng là quả táo',N'Táo',2,2,1,'soldier-bu210527141.jpg',1),
('P0020',N'Quả Lê',88000,111,941,281,N'Nông trại lê',N'Đây là quả lê',N'Đây cũng là quả lê',N'Lê',2,2,2,'soldier-bu210527141.jpg',1),
('P0021',N'Quả Dưa',89000,115,942,282,N'Nông trại dưa',N'Đây là quả dưa',N'Đây cũng là quả dưa',N'Dưa',2,2,1,'soldier-bu210527141.jpg',1),
('P0022',N'Quả Nho',90000,120,943,283,N'Nông trại nho',N'Đây là quả nho',N'Đây cũng là quả nho',N'Nho',2,2,2,'soldier-bu210527141.jpg',1),
('P0023',N'Quả Dâu Tây',91000,130,944,284,N'Nông trại dâu tây',N'Đây là quả dâu tây',N'Đây cũng là quả dâu tây',N'Dây tây',2,1,1,'soldier-bu210527141.jpg',1),
('P0024',N'Quả Cóc',92000,135,945,285,N'Nông trại cóc',N'Đây là quả cóc',N'Đây cũng là quả cóc',N'Quả Cóc',2,2,2,'soldier-bu210527141.jpg',1),
('P0025',N'Quả Cherry',93000,136,946,286,N'Nông trại cherry',N'Đây là quả cherry',N'Đây cũng là quả cherry',N'Cherry',2,3,1,'soldier-bu210527141.jpg',1),
('P0026',N'Quả Xoài',94000,137,947,287,N'Nông trại xoài',N'Đây là quả xoài',N'Đây cũng là quả xoài',N'Quả xoài',2,4,2,'soldier-bu210527141.jpg',1),
('P0027',N'Quả Bơ',95000,138,948,288,N'Nông trại bơ',N'Đây là quả bơ',N'Đây cũng là quả bơ',N'Quả bơ',2,5,1,'soldier-bu210527141.jpg',1),
('P0028',N'Quả Sầu Riêng',96000,139,949,289,N'Nông trại sầu riêng',N'Đây là quả sầu riêng',N'Đây cũng là quả sầu riêng',N'Sầu riêng',2,6,2,'soldier-bu210527141.jpg',1),
('P0029',N'Quả Mít',97000,140,950,290,N'Nông trại mít',N'Đây là quả mít',N'Đây cũng là quả mít',N'Mít',2,7,2,'soldier-bu210527141.jpg',1),
('P0030',N'Quả Nhãn',98000,150,960,295,N'Nông trại nhãn',N'Đây là quả nhãn',N'Đây cũng là quả nhãn',N'Nhãn',2,8,2,'soldier-bu210527141.jpg',1)

select * from Product;


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
('U3','Staff');

/*select* from [User.Role];*/

/*tạo table User*/

drop table [User];

CREATE TABLE [User] (
  [UserId] int IDENTITY(1,1) PRIMARY KEY,
  [UserRoleId] int,
  [EncodedPassword] varchar(500),
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [Phone] varchar(10),
  [Email] varchar(100),
  [Address] nvarchar(500),
  [AvatarURL] varchar(200),
  [VillageId] varchar(20),
  [WardId] varchar(20),
  [DistrictId] varchar(20),
  [ProvinceId] varchar(20),
);

insert into [User](UserRoleId,EncodedPassword,FirstName,LastName,Phone,Email,[Address],AvatarURL,VillageId,WardId,DistrictId,ProvinceId) values
(1,'123','Ngo','Duy','096359548','duy123@gmail.com','PhuTho','https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg',null,null,null,null),
(1,'123','Ngo','Anh','086522454','anh123@gmail.com','PhuTho','https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg',null,null,null,null),
(1,'123','Do','Tung','0763845','tung123@gmail.com','PhuTho','https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg',null,null,null,null),
(1,'123','Nguyen','Dat','066354845','tung123@gmail.com','PhuTho','https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg',null,null,null,null),
(1,'123','Nguyen','Khanh','056359548','khanh123@gmail.com','PhuTho','https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg',null,null,null,null),
(2,'123','Nguyen','Van','046359548','nguyenvan23@gmail.com','PhuTho','https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg',null,null,null,null),
(3,'123','Hoang','An','036359548','hoangan123@gmail.com','PhuTho','https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg',null,null,null,null),
(4,'123','Chu','Nam','026359548','chunam23@gmail.com','PhuTho','https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg',null,null,null,null);

--select * from [User]



/*tạo table Cart*/
drop table [Cart];
CREATE TABLE [Cart] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] nvarchar(50),
  [OrderTime] date,
  [Note] nvarchar(1000),
  [UserID] int,
  [OrderStatusID] int,
);
insert into Cart(Code,OrderTime,Note,UserID,OrderStatusID) values
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
(1,20,15),
(2,21,15),
(3,24,15),
(4,26,15);
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
);
insert into [Order](UserId,WeekendDelivery,EarliestDeliveryDate,LatestDeliveryDate,[Address],ProvinceId,DistrictId,WardId,OrderStatusId,ShipperID) values
(1,'True','2021-12-05','2021-12-01',N'số 12, ngõ 23, đường Lê Đức Thắn/g','00625','019HH','01TTT',1,1),
(2,'False','2021-12-05','2021-12-01',N'số 12, ngõ 23, đường Lê Đức Thắn/g','00625','019HH','01TTT',2,2),
(3,'True','2021-12-05','2021-12-01',N'số 12, ngõ 23, đường Lê Đức Thắn/g','00625','019HH','01TTT',3,3),
(4,'False','2021-12-05','2021-12-01',N'số 12, ngõ 23, đường Lê Đức Thắn/g','00625','019HH','01TTT',4,4),
(4,'False','2021-12-05','2021-12-01',N'số 12, ngõ 23, đường Lê Đức Thắn/g','00625','019HH','01TTT',4,5);

select * from [Order]

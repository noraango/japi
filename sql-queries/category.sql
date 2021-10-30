/*Chạy file sql này 2 lần*/

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
(N'Mẹ - Bé',1,1),
(N'Thực phẩm',1,1),
(N'Nhà cửa - Đời sống',1,1),
(N'Thời trang',1,1),
(N'Văn phòng phẩm',1,1),
(N'Chăm sóc sắc đẹp',1,1),
(N'Sữa',2,1),
(N'Đồ chơi',2,1),
(N'Bát đĩa',2,1),
(N'Quần áo',2,1)

/*select * from Category;*/

drop table [Product.Status];
CREATE TABLE [Product.Status] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] varchar(10),
  [Name] nvarchar(50),
);

insert into [Product.Status](Code,Name) values
('P1',N'Đang trưng bày'),
('P2',N'Trong kho')

/*select * from [Product.Status];*/

drop table [Product.PackingMethod]
CREATE TABLE [Product.PackingMethod] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Name] nvarchar(200),
);

insert into [Product.PackingMethod](Name) values
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
('Nhật Bản'),
('Việt Nam'),
('Trung Quốc')

/*test table origin*/
/*select * from Origin*/

/*tạo table Image*/
drop table [Image]
CREATE TABLE [Image] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [FilePath] varchar(200),
  [Name] nvarchar(200),
  [Description] nvarchar(1000),
);
CREATE INDEX [UK] ON  [Image] ([FilePath]);
insert into Image(FilePath,Name,Description) values
('https://product.hstatic.net/1000282430/product/upload_8d2c176d96af4226897a01367d5c9f1b_grande.jpg',N'Hành',N'Củ hành')
/*select * from Image*/

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
  [DisplayImageId] int,
);
insert into Product(Code,[Name],Price,Size,[Weight],Quantity,Manufacturer,ShortDescription,[Description],Brand,OriginId,PackingMethodId,ProductStatusId,DisplayImageId) values
('P0001',N'Củ hành',50000,50,500,100,N'Nông trại vui vẻ',N'Đây là củ hành',N'Đây cũng là củ hành',N'Vui vẻ',2,2,1,1)
select * from Product;
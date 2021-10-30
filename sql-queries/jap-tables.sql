﻿/*Chạy file sql này 2 lần*/

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
('https://product.hstatic.net/1000282430/product/upload_8d2c176d96af4226897a01367d5c9f1b_grande.jpg',N'Hành',N'Củ hành'),
('https://cdn.tgdd.vn/Products/Images/8785/226905/bhx/khoai-lang-nhat-1kg-202101150934398643.jpg',N'Khoai',N'Củ khoai'),
('https://cdn.tgdd.vn/2021/01/CookProduct/Cu-kieu-co-tac-dung-gi-meo-chon-mua-va-cach-phan-biet-cu-kieu-va-cu-hanh-0-1200x676.jpg',N'Kiệu',N'Củ kiệu'),
('https://image-us.eva.vn/upload/2-2019/images/2019-05-20/cach-muoi-mang-chua-ot-toi-ngon-de-duoc-lau-khong-noi-vang-cach-muoi-mang-chua-1-1558340205-5-width600height337.jpg',N'Măng',N'Củ măng'),
('https://anphupet.com/wp-content/uploads/2021/03/cu-san-gioi-thieu.jpg',N'Sắn',N'Củ sắn'),
('https://cdn.tgdd.vn/2021/07/content/1(1)-800x450-9.jpg',N'Ngô',N'Bắp Ngô'),
('https://cdn.tgdd.vn/Products/Images/8820/226916/bhx/rau-muong-hat-tui-500g-202009292358279889.jpg',N'Rau muống',N'Rau muống'),
('https://media-cdn.laodong.vn/storage/newsportal/Uploaded/ctvsuckhoe/2016_10_30/rau-cai-ngot_MBFW.jpg',N'Rau cải',N'Rau cải'),
('https://vinmec-prod.s3.amazonaws.com/images/20201125_143129_322415_ba-bau-khong-an-rau-g.max-800x800.jpg',N'Rau Ngót',N'Rau ngót'),
('https://tieudung.vn/upload_images/images/2020/10/16/bap-cai.jpg',N'Bắp Cải',N'Rau Bắp Cải'),
('https://blog.happytrade.org/wp-content/uploads/2019/05/cach-luoc-sup-lo-xanh-2.jpg',N'Súp Lơ',N'Rau Súp Lơ'),
('https://caythuoc.org/wp-content/uploads/2019/06/Qua-bau.jpg',N'Bầu',N'Quả Bầu'),
('https://biggreen.vn/publish/thumbnail/20366/480x0xfull/upload/s/20170116/2b679be19afb07b34a00537298599182bi-do-co-tien.jpg_v=1502673846367.jpg',N'Bí',N'Quả Bí'),
('http://hn.check.net.vn/data/product/mainimages/original/product5266.jpg',N'Mướp',N'Quả Mướp'),
('https://zicxa.com/vi/uploads/news/luu-y-khi-giam-can-voi-bi-dao.jpg',N'Bí Đao',N'Quả bí đao'),
('https://caythuoc.org/wp-content/uploads/2021/03/dau-que.jpg',N'Đậu',N'Quả đậu'),
('https://tieudung.vn/upload_images/images/2021/09/13/oi.jpg',N'Ổi',N'Quả ổi'),
('http://media.doisongphapluat.com/589/2018/4/20/sung_1_ZHPI.jpg',N'Sung',N'Quả sung'),
('https://cafefcdn.com/thumb_w/650/203337114487263232/2020/10/13/photo1602578080665-16025780808271098279436.jpg',N'Táo',N'Quả táo'),
('https://bizweb.dktcdn.net/thumb/large/100/036/299/products/6361871701769088964678471-le.jpg',N'Lê',N'Quả Lê'),
('https://hinh365.com/wp-content/uploads/2020/06/thu-vien-anh-dep-ve-qua-dua-hau-voi-khoan-100-619-tam-anh-chat-luong-cuc-cao.jpg',N'Dưa',N'Quả Dưa'),
('https://dep.com.vn/Uploaded/phuongnth/2012_09_18/grapesRed.jpg',N'Nho',N'Quả nho'),
('https://e.khoahoc.tv/photos/image/2018/06/14/dau-tay.jpg',N'Dâu tây',N'Quả dâu tây'),
('https://namlimxanh.vn/wp-content/uploads/2018/10/qua-coc-co-tac-dung-chua-benh-gi-va-cach-dung-qua-coc-hieu-qua.jpg',N'Cóc',N'Quả cóc'),
('https://e.khoahoc.tv/photos/image/2019/02/18/cherry.jpg',N'Cherry',N'Quả cherry'),
('https://suckhoedoisong.qltns.mediacdn.vn/Images/bichvan/2019/07/03/loi_ich_suc_khoe_cua_xoai2.jpg',N'Xoài',N'Quả xoài'),
('https://caygiongbo.com/datafiles/3/2017-10-13/15078724336512_nhung-tac-dung-phu-can-luu-y-khi-an-qua-bo.jpg',N'Bơ',N'Quả bơ'),
('https://afamilycdn.com/Images/Uploaded/Share/2010/08/28/010828gt82.jpg',N'Sầu Riêng',N'Quả sầu riêng'),
('https://www.foodnk.com/wp-content/uploads/2020/08/tim-hieu-ve-qua-mit-va-cac-ung-dung-trong-doi-song-cong-nghiep-4-min.jpg',N'Mít',N'Quả mít'),
('https://baoxaydung.com.vn/stores/news_dataimages/vananh/082014/08/15/150149baoxaydung_image001.jpg',N'Nhãn',N'Quả nhãn')


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
('P0001',N'Củ hành',50000,50,500,100,N'Nông trại vui vẻ',N'Đây là củ hành',N'Đây cũng là củ hành',N'Vui vẻ',2,2,1,1),
('P0002',N'Củ khoai',60000,60,600,110,N'Nông trại khoai',N'Đây là củ khoai',N'Đây cũng là củ khoai',N'Khoai',1,1,2,2),
('P0003',N'Củ kiệu',70000,70,700,120,N'Nông trại kiệu',N'Đây là củ kiệu',N'Đây cũng là củ kiệu',N'Kiệu',3,3,1,3),
('P0004',N'Củ măng',71000,75,750,130,N'Nông trại măng',N'Đây là củ măng',N'Đây cũng là củ măng',N'Măng',2,4,2,4),
('P0005',N'Củ Sắn',74000,90,810,150,N'Nông trại sắn',N'Đây là củ sắn',N'Đây cũng là củ sắn',N'Sắn',1,5,1,5),
('P0006',N'Bắp Ngô',73000,80,800,140,N'Nông trại ngô',N'Đây là bắp ngô',N'Đây cũng là bắp ngô',N'Ngô',3,6,2,6),
('P0007',N'Rau Muống',75000,85,820,160,N'Nông trại rau muống',N'Đây là rau muống',N'Đây cũng là rau muống',N'Rau muống',2,7,1,7),
('P0008',N'Rau Cải',7600,91,830,170,N'Nông trại rau cải',N'Đây là rau cải',N'Đây cũng là rau cải',N'Rau cải',1,8,2,8),
('P0009',N'Rau Ngót',77000,92,840,180,N'Nông trại rau ngót',N'Đây là rau ngót',N'Đây cũng là rau ngót',N'Rau ngót',3,2,1,9),
('P0010',N'Rau Bắp Cải',78000,93,850,190,N'Nông trại bắp cải',N'Đây là rau bắp cải',N'Đây cũng là rau bắp cải',N'Bắp Cải',2,2,2,10),
('P0011',N'Rau Súp Lơ',79000,94,860,200,N'Nông trại súp lơ',N'Đây là rau súp lơ',N'Đây cũng là súp lơ',N'Súp lơ',2,2,1,11),
('P0012',N'Quả Bầu',80000,95,870,210,N'Nông trại bầu',N'Đây là quả bầu',N'Đây cũng là quả bầu',N'Quả bầu',2,2,2,12),
('P0013',N'Quả Bí',81000,96,880,220,N'Nông trại bí',N'Đây là quả bí',N'Đây cũng là cquả bí',N'Quả bí',2,2,1,13),
('P0014',N'Quả Mướp',82000,97,890,230,N'Nông trại mướp',N'Đây là quả mướp',N'Đây cũng là quả mướp',N'Mướp',2,2,2,14),
('P0015',N'Quả Bí Dao',83000,98,900,240,N'Nông trại bí đao',N'Đây là quả bí đao',N'Đây cũng là quả bí đao',N'Bí Đao',2,2,1,15),
('P0016',N'Quả Đậu',84000,99,910,250,N'Nông trại đậu',N'Đây là quả đậu',N'Đây cũng là quả đậu',N'Đậu',2,2,2,16),
('P0017',N'Quả Ổi',85000,100,920,260,N'Nông trại ổi',N'Đây là quả ổi',N'Đây cũng là quả ổi',N'Ổi',2,2,1,17),
('P0018',N'Quả Sung',86000,105,930,270,N'Nông trại sung',N'Đây là quả sung',N'Đây cũng là quả sung',N'Sung',2,2,2,18),
('P0019',N'Quả Táo',87000,110,940,280,N'Nông trại táo',N'Đây là quả táo',N'Đây cũng là quả táo',N'Táo',2,2,1,19),
('P0020',N'Quả Lê',88000,111,941,281,N'Nông trại lê',N'Đây là quả lê',N'Đây cũng là quả lê',N'Lê',2,2,2,20),
('P0021',N'Quả Dưa',89000,115,942,282,N'Nông trại dưa',N'Đây là quả dưa',N'Đây cũng là quả dưa',N'Dưa',2,2,1,21),
('P0022',N'Quả Nho',90000,120,943,283,N'Nông trại nho',N'Đây là quả nho',N'Đây cũng là quả nho',N'Nho',2,2,2,22),
('P0023',N'Quả Dâu Tây',91000,130,944,284,N'Nông trại dâu tây',N'Đây là quả dâu tây',N'Đây cũng là quả dâu tây',N'Dây tây',2,1,1,23),
('P0024',N'Quả Cóc',92000,135,945,285,N'Nông trại cóc',N'Đây là quả cóc',N'Đây cũng là quả cóc',N'Quả Cóc',2,2,2,24),
('P0025',N'Quả Cherry',93000,136,946,286,N'Nông trại cherry',N'Đây là quả cherry',N'Đây cũng là quả cherry',N'Cherry',2,3,1,25),
('P0026',N'Quả Xoài',94000,137,947,287,N'Nông trại xoài',N'Đây là quả xoài',N'Đây cũng là quả xoài',N'Quả xoài',2,4,2,26),
('P0027',N'Quả Bơ',95000,138,948,288,N'Nông trại bơ',N'Đây là quả bơ',N'Đây cũng là quả bơ',N'Quả bơ',2,5,1,27),
('P0028',N'Quả Sầu Riêng',96000,139,949,289,N'Nông trại sầu riêng',N'Đây là quả sầu riêng',N'Đây cũng là quả sầu riêng',N'Sầu riêng',2,6,2,28),
('P0029',N'Quả Mít',97000,140,950,290,N'Nông trại mít',N'Đây là quả mít',N'Đây cũng là quả mít',N'Mít',2,7,2,29),
('P0030',N'Quả Nhãn',98000,150,960,295,N'Nông trại nhãn',N'Đây là quả nhãn',N'Đây cũng là quả nhãn',N'Nhãn',2,8,2,30)

/*select * from Product;*/


/*tạo table user.role*/

drop table [User.Role];
CREATE TABLE [User.Role] (
  [Id] int IDENTITY(1,1) PRIMARY KEY,
  [Code] varchar(10),
  [Name] nvarchar(50),
);



insert into [User.Role](Code,[Name]) values
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

select * from [User]


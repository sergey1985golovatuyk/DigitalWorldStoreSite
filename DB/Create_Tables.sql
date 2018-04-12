-- Creation of DB --

-- Creation Tables --
USE [DigitalWorldStoreDB]
GO
CREATE TABLE Categories(
						categoryID integer NOT NULL PRIMARY KEY
						, categoryName nvarchar(100) NOT NULL)

CREATE TABLE Products(
						productID integer NOT NULL PRIMARY KEY
						,categoryID integer NOT NULL
							CONSTRAINT FK_category_product
							REFERENCES [Categories](CategoryID) ON DELETE NO ACTION ON UPDATE NO ACTION
						, name nvarchar(100) NOT NULL
						, weight float NOT NULL
						, price decimal NULL
						, productionDate datetime NOT NULL
						, expirationDate datetime NOT NULL)

CREATE TABLE Orders(
						orderID integer NOT NULL PRIMARY KEY
						, customerName nvarchar(100) NOT NULL
						, customerLastName nvarchar(100) NOT NULL
						, customerMiddleName nvarchar(100) NOT NULL
						, deliveryAddress nvarchar(200) NOT NULL
						, customerPhone nvarchar(30) NOT NULL
						, customerEmail nvarchar(100) NULL
						, totalPrice decimal)
						
CREATE TABLE OrderDetails(
							orderDetailID integer NOT NULL PRIMARY KEY
							, orderID integer NOT NULL
								CONSTRAINT FK_orderDet_order
								REFERENCES [Orders](OrderID) ON DELETE NO ACTION ON UPDATE NO ACTION
							, productID integer NOT NULL
							, quantity integer NOT NULL
							, productUnitPrice decimal NOT NULL)

GO
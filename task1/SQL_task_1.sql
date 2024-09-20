
--vinay solanki assignment 1 task 1

create database TechShop;

use TechShop;

--creating table

create table Products(ProductID int primary key identity ,ProductName varchar(100),Description text,Price decimal(10,2));

create table Customers(CustomerID int primary key identity,FirstName varchar(50), LastName varchar(50),Email varchar(100),Phone varchar(15),Address varchar(255));

create table Orders(OrderID int primary key identity, CustomerID int, OrderDate Datetime, TotalAmount decimal(10,2),foreign key(CustomerID) references Customers(CustomerID));

create table OrderDetails(OrderDetailID int primary key identity, OrderID int,ProductID int,foreign key(ProductID) references Products(ProductID), foreign key(OrderID) references Orders(OrderID), Quantity int );

create table Inventory (InventoryID int primary key identity, ProductID int, QuantityInStock int, LastStockUpdate datetime, foreign key(ProductID) references Products(ProductID));



--inserting values

INSERT INTO Products (ProductName, Description, Price) VALUES
('Laptop', '15 inch laptop', 50000.00),
('Smartphone', 'Latest Android smartphone', 25000.00),
('Tablet', '10 inch tablet', 20000.00),
('Headphones', 'Wireless over-ear headphones', 3000.00),
('Charger', 'Fast charging USB charger', 800.00),
('Smartwatch', 'Fitness smartwatch', 6000.00),
('Camera', 'Digital camera with 20MP', 35000.00),
('Bluetooth Speaker', 'Portable Bluetooth speaker', 4000.00),
('Mouse', 'Wireless optical mouse', 1500.00),
('Keyboard', 'Mechanical gaming keyboard', 2500.00);


INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) VALUES
('Ravi', 'Sharma', 'ravi.sharma@example.com', '9876543210', '123, MG Road, Bangalore'),
('Anita', 'Desai', 'anita.desai@example.com', '8765432109', '456, 2nd Main, Delhi'),
('Ajay', 'Kumar', 'ajay.kumar@example.com', '7654321098', '789, 3rd Cross, Mumbai'),
('Sneha', 'Patel', 'sneha.patel@example.com', '6543210987', '321, Market Street, Ahmedabad'),
('Vikram', 'Singh', 'vikram.singh@example.com', '5432109876', '654, Sector 17, Chandigarh'),
('Pooja', 'Gupta', 'pooja.gupta@example.com', '4321098765', '987, Park Lane, Pune'),
('Rahul', 'Verma', 'rahul.verma@example.com', '3210987654', '234, Beach Road, Chennai'),
('Neha', 'Iyer', 'neha.iyer@example.com', '2109876543', '567, Hilltop, Hyderabad'),
('Suresh', 'Menon', 'suresh.menon@example.com', '1098765432', '890, Green Valley, Kolkata'),
('Aditi', 'Mishra', 'aditi.mishra@example.com', '0987654321', '345, River Side, Jaipur');


INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) VALUES
(1, GETDATE(), 51000.00),
(2, GETDATE(), 25000.00),
(3, GETDATE(), 35000.00),
(4, GETDATE(), 9000.00),
(5, GETDATE(), 30000.00),
(6, GETDATE(), 15000.00),
(7, GETDATE(), 27000.00),
(8, GETDATE(), 12000.00),
(9, GETDATE(), 48000.00),
(10, GETDATE(), 6000.00);


INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 1),
(4, 4, 2),
(5, 5, 1),
(6, 6, 1),
(7, 7, 1),
(8, 8, 2),
(9, 9, 1),
(10, 10, 3);


INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate) VALUES
(1, 10, GETDATE()),
(2, 20, GETDATE()),
(3, 15, GETDATE()),
(4, 30, GETDATE()),
(5, 50, GETDATE()),
(6, 25, GETDATE()),
(7, 12, GETDATE()),
(8, 35, GETDATE()),
(9, 40, GETDATE()),
(10, 35, GETDATE());


--displaying the tables data

select * from Products;
select * from Customers;
select * from Inventory;
select * from Orders;
select * from OrderDetails;
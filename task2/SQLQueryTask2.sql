use TechShop;

--1 question
select FirstName, LastName, Email from Customers where Customers.CustomerID is not null;

--2 question
select o.OrderID, o.OrderDate,c.FirstName,c.LastName from Orders o join Customers c on o.CustomerID=c.CustomerID;

--3 question
insert into Customers values ('Vinay','Solanki','xyz@email.com','1234567890','Pune');

--4 question

--previous products prices
select Price from Products;

--after incrementing by 10%
update Products
set Price = price * 1.10;

select Price from Products;

--5 question

--declaring the variable for orderID
declare @OrderID int = 10;

--before deletion
select * from Orders;
select * from OrderDetails;

--after deletion
delete from OrderDetails
where OrderID=@OrderID;

delete from Orders
where OrderID = @OrderID;

Select * from Orders;
select * from OrderDetails;

--6 question
select * from Orders;
insert into Orders values(1, GETDATE(), 100.00);
select * from Orders;

--7 question
select * from Customers where CustomerID = 11;

declare @CustomerID int = 11;
declare @Email varchar(255) = 'vinay@gmail.com';

update Customers
set Email = @Email
where CustomerID = @CustomerID;

select * from Customers where CustomerID = 11;

--8 question

--in task 1 schema of orderdetails does not included price column to complete this query we need to add price column
alter table OrderDetails
add Price decimal(18,2);

DBCC CHECKIDENT ('OrderDetails', RESEED, 0);

INSERT INTO OrderDetails (OrderID, ProductID, Quantity,Price) VALUES
(1, 1, 1,10.00),
(2, 2, 1,25.55),
(3, 3, 1,40.00),
(4, 4, 2,55.00),
(5, 5, 1,80.00),
(6, 6, 1,22.10),
(7, 7, 1,90.00),
(8, 8, 2,89.00),
(9, 9, 1,99.99);

select TotalAmount from Orders;

update o
set o.TotalAmount = sub.TotalCost
from Orders o
join (
	select OrderID, sum(Price * Quantity) as TotalCost
	from OrderDetails
	group by OrderID
) as sub on o.OrderID = sub.OrderID;

select TotalAmount from Orders;

--9 question
select * from Orders;
select * from OrderDetails;

begin transaction;
declare @CustomerID int= 10;
delete from OrderDetails
where OrderID in (select OrderID from Orders where CustomerID = @CustomerID);

Delete from Orders
where CustomerID = @CustomerID;

commit transaction;

select * from Orders;
select * from OrderDetails;

-- 10 question
select * from Products;
INSERT INTO Products (ProductName,Description,Price )
VALUES ('Speaker', 'Latest model with advanced features', 2000);
select * from Products;



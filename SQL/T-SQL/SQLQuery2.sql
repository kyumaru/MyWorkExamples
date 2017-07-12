SELECT TABLE_NAME
FROM testDB2.INFORMATION_SCHEMA.Tables 
WHERE TABLE_TYPE = 'BASE TABLE'


select*from customers
select*from orders

select * 
from customers c, orders o
where c.CustomerID= o.CustomerID

alter table orders
add amount int
go

alter table customers
drop column extraColumn1

ALTER TABLE Orders
ADD FOREIGN KEY (customerID)
REFERENCES customers (customerID)

insert into orders
values(5,111,'something1');

SELECT c.Name,avg(o.CustomerID) as countResult
FROM Orders o ,customers c
where o.CustomerID=c.CustomerID 
GROUP BY c.Name
HAVING avg(o.CustomerID) > 1




SELECT	AVG(myCount)
FROM
(
	SELECT c.Name, count (amount) as myCount
	from orders o, customers c
	where o.CustomerID=c.CustomerID 
	GROUP BY c.Name 
) q1


SELECT avg(amount) as myAverage,c.Name
from orders o, customers c
where o.CustomerID=c.CustomerID 
GROUP BY c.Name 
having avg( amount) < 20


update orders
set amount = 10

UPDATE orders
SET amount = 20
WHERE CustomerID = 111;


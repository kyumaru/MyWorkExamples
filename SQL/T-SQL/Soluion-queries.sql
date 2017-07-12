/*to query do t1,t2,t3 this creates the cross product from all tables
and then start filtering unncessary rows using where over table
columns with repeated fields (i.e t1.id=t2.id), this method will 
show rows matching such condicional discarding all others, apply to 
other columns to filter further. This can get done using join as well

A set of rows can be grouped for a give field column(group by), when
this takes place an aggregate function can be used, MAX(colum), Count(*)
however if you use group by colum1, column2 etc, the same column set has
to appear in the proyeccion. 

There are conditions to use a function or a storedProcedure, check below

An schema is related to db security, check this tab on t-sql object explorer
An schema contains database objects such as tables, views, stored procedures, etc
A schema owner can be a database user a database role or application role.
Every database object is encapsulated by a schema like tst, dbo
dbo seems to be a user which owns a list of schema
*/
 
--out of all databases use mydb as current context for queries
use mydb
go

--1
select custname,c.custID,ordertypename,count(custname) as orderCount
from tblCustomer c, tblOrder o, tblOrderType ot 
where c.custid=o.custid
and o.ordertypeid=ot.ordertypeid
GROUP BY custname,c.custID,ordertypename
ORDER BY custname DESC

--2
select c.custName,c.custID,o.loanNumber,oa.activityDate
from tblCustomer c, tblOrder o, 
     tblOrderType ot, tblActivity a,
	 tblOrderActivity oa
where c.custid=o.custid
and orderTypeName='assignment'
and o.orderid=oa.orderid
and (activityName='received' )
and (a.activityID=oa.activityID)
and o.orderID NOT IN 
(
	select o.orderID
	from tblCustomer c, tblOrder o, 
		 tblOrderType ot, tblActivity a,
		 tblOrderActivity oa
	where c.custid=o.custid
	and orderTypeName='assignment'
	and o.orderid=oa.orderid
	and (activityName='delivered' )
	and (a.activityID=oa.activityID)
)

--3
INSERT INTO tblOrderType
VALUES (3,'Document Retrieval');

--4
INSERT INTO tblOrderActivity( orderid,activityid,activitydate )
SELECT  o.orderID,3,GETDATE()
FROM    tblOrder o
where	o.loanNumber=45566856  

--5
/*write a function that returns a table with the orderid and 4 diff times as
result set which can be joined upon.
filter accordingly by orderid column*/

--a table is a set of rows
--function creates a row for requested table
CREATE FUNCTION createDiffDatesRow (@orderID Int) 
  RETURNS @diffsTable Table 
    (orderID Int, 
     tt1 int, 
     tt2 int,
	 tt3 int,
	 tt4 int ) 
AS 
  BEGIN 
  
	DECLARE @time1 smalldatetime,
	@time2 smalldatetime,
	@dummyID int,
	@dummyActivty int,
	@myCursor CURSOR,
	@tt1 int,@tt2 int,@tt3 int,@tt4 int,
	@receiv smalldatetime, @deliver smalldatetime;

	--variables not set start as null

	SET @myCursor = CURSOR FOR
	select orderID,activityID,activityDate
	from tblOrderActivity oa
	where oa.orderid=@orderID

	DECLARE @cont int=1;

	OPEN @myCursor;
	FETCH NEXT FROM @myCursor INTO @dummyID,@dummyActivty,@time2;
 
	WHILE @@FETCH_STATUS = 0--fetch success, 1 otherwise
	BEGIN
	 set @time1=@time2;
	 FETCH NEXT FROM @myCursor INTO @dummyID,@dummyActivty,@time2;

	 IF @@FETCH_STATUS = 0
		BEGIN
		 IF @cont=1
			BEGIN
				set @receiv=@time1
				set @tt1=DATEDIFF(MINUTE, @time1,@time2)
			END
		 IF @cont=2
			BEGIN
				set @tt2=DATEDIFF(MINUTE, @time1,@time2)
			END
		 IF @cont=3
			BEGIN
				set @deliver=@time2
				set @tt3=DATEDIFF(MINUTE, @time1,@time2)
			END
		END

	 set @cont=@cont+1
	
	END

	CLOSE @myCursor;
	DEALLOCATE @myCursor;

	IF @deliver is not null and @receiv is not null
	BEGIN
		set @tt4=DATEDIFF(MINUTE, @receiv,@deliver)
	END

	insert into @diffsTable
	values(@orderID,@tt1,@tt2,@tt3,@tt4);
		
    RETURN 
  END 
GO

--a function to return a table out of diffrows
CREATE FUNCTION createDiffDatesTable() 
  RETURNS @aTable Table 
    (orderID Int, 
     tt1 int, 
     tt2 int,
	 tt3 int,
	 tt4 int ) 
AS 
  BEGIN 
  
	DECLARE @myCursor CURSOR,
			@orderID int;
	
	SET @myCursor = CURSOR FOR
	select orderid
	from tblOrder 

	OPEN @myCursor;
	FETCH NEXT FROM @myCursor INTO @orderid;
 
	WHILE @@FETCH_STATUS = 0
	BEGIN
	 
	 INSERT INTO @aTable( orderid,tt1,tt2,tt3,tt4 )
	 SELECT  *
	 FROM   createDiffDatesRow(@orderid)
     
	 FETCH NEXT FROM @myCursor INTO @orderid;
	
	END

	CLOSE @myCursor;
	DEALLOCATE @myCursor;
			
    RETURN 
  END 
GO

--final answer to #5
select custname,ordertypename,loanNumber,tt1 as TurnTime1,tt2 as TurnTime2,tt3 as TurnTime3,tt4 as TurnTime4
from tblCustomer c, tblOrder o, tblordertype ot,createDiffDatesTable() dt
where c.custid=o.custid
and o.orderTypeID=ot.orderTypeID
and o.orderID=dt.orderID

--6
--since this alters the db permanently it should be a stored_procedure() 
/*the sp has 2 modes of operation if input parameter is 'all' it will
create a new retrieval order(orderType=3) for each orderType=1(assigment).
If mode of operation is null it will skip assigment orders with 
retrieval orders already created fo it, avoiding duplicates, thats how
"...can be run multiple times without creating duplicate document retrieval orders each time"
was implemented, every time a new order is created this way its orderID is logged
into tblOrderActivity as activityID=1(received)*/

exec createRetrievalOrders null

Create PROCEDURE createRetrievalOrders(
	@mode CHAR(3)	--Input parameter ,  the mode of insertion, null no duplicates, all to duplicate rows
)
AS
BEGIN
	
DECLARE @myCursor CURSOR,
		@orderID int,
		@orderTypeID int,
		@custID int,
		@loanNumber int,
		@maxID int,
		@offset int,
		@rowCount int,
		@maxActivityID int;

IF @mode='all'
	BEGIN	
		SET @myCursor = CURSOR FOR  
	  --query all assigment orders 
		select o.orderID,c.custID,o.orderTypeID,o.loanNumber
		from tblOrder o,tblCustomer c
		where o.orderTypeID=1
		and o.custID=c.custID
	
		set @maxID= (select MAX(orderid) FROM tblOrder)
		set @rowCount= (select count(*) FROM tblOrder)
		set @offset=0;
		
	END--if end
	ELSE 
		BEGIN

			DECLARE @tmpOrderTable TABLE(
				orderID int,
				custID int,
				orderTypeID int,
				loanNumber int
			);
			
			insert into @tmpOrderTable(orderID,custID,orderTypeID,loanNumber)
			select o.orderID,c.custID,o.orderTypeID,o.loanNumber
			from tblOrder o,tblCustomer c
			where o.orderTypeID=1
			and o.custID=c.custID
			and o.loanNumber NOT IN
			(
				select o.loanNumber
				from tblOrder o,tblCustomer c
				where o.orderTypeID=3
				and o.custID=c.custID		
			)			

			SET @myCursor = CURSOR FOR
			select*
			from @tmpOrderTable

			set @maxID= (select MAX(orderid) FROM @tmpOrderTable)
			set @rowCount= (select count(*) FROM @tmpOrderTable)
			set @offset=0;	
		END--else end

	OPEN @myCursor;
	FETCH NEXT FROM @myCursor INTO @orderid,@custID,@orderTypeID,@loanNumber;
	
	WHILE @@FETCH_STATUS = 0 and @rowCount > 0
	BEGIN

		set @offset=@offset+1;
		set @rowCount=@rowCount-1;
	
		INSERT INTO tblOrder
		values(@maxID+@offset,@custID,3,@loanNumber)  
		
		INSERT INTO tblOrderActivity
		values(@maxID+@offset,1,GETDATE())
		
		FETCH NEXT FROM @myCursor INTO @orderid,@custID,@orderTypeID,@loanNumber;	
	
	END

	CLOSE @myCursor;
	DEALLOCATE @myCursor;

END
GO--sp needs GO by batch

--References, Auxiliary code used and table creation

--when to use a stored_procedure() or a function() in sql
--http://stackoverflow.com/questions/1179758/function-vs-stored-procedure-in-sql-server
--http://stevestedman.com/2013/04/t-sql-a-simple-example-using-a-cursor/
--http://stackoverflow.com/questions/14506871/how-to-execute-a-stored-procedure-inside-a-select-query
--http://stackoverflow.com/questions/61967/is-there-a-way-to-loop-through-a-table-variable-in-tsql-without-using-a-cursor
--http://www.codeproject.com/Articles/126898/Sql-Server-How-To-Write-a-Stored-Procedure-in-SQL
--http://stackoverflow.com/questions/18532663/if-else-in-stored-procedure-sql-server

--if a function returns a table, the result set has to be used as such
SELECT * FROM createDiffDatesTable() 

delete from tblOrder
where tblOrder.orderID in (select top 10 tblOrder.orderID from tblOrder order by tblOrder.orderID desc)

drop proc createRetrievalOrders

exec createRetrievalOrders ''

DROP FUNCTION createDiffDatesRow

CREATE TABLE tblCustomer
(
custID int,
custName varchar(50),
PRIMARY KEY (custID)
);

CREATE TABLE tblOrder
(
orderID int,
custID int,
orderTypeID int,
loanNumber int,
PRIMARY KEY (orderID),
FOREIGN KEY (custID) REFERENCES tblCustomer(custID)
);

CREATE TABLE tblOrderType
(
orderTypeID int,
orderTypeName varchar(50),
PRIMARY KEY (orderTypeID)
);

CREATE TABLE tblActivity
(
activityID int,
activityName varchar(50),
PRIMARY KEY (activityID),
);

CREATE TABLE tblOrderActivity
(
orderID int,
activityID int,
activityDate smalldatetime,
FOREIGN KEY (orderID) REFERENCES tblorder(orderid),
FOREIGN KEY (activityID) REFERENCES tblactivity(activityid)
);

INSERT INTO tblOrderType
VALUES (1,'ABC bank');
 
INSERT INTO tblCustomer
VALUES (2,'chase bank');

INSERT INTO tblOrder
VALUES (1,2,1,8888);

ALTER TABLE tblOrder
ADD FOREIGN KEY (orderTypeID)
REFERENCES tblOrderType(ordertypeID)

ALTER TABLE tblOrder
DROP CONSTRAINT FK__tblOrder__orderT__15502E78;


ALTER TABLE tblOrder
ADD FOREIGN KEY (orderTypeID)
REFERENCES tblOrderType(orderID)

insert into tblorderactivity
values(2,1,NOW())
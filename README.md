# ASPNET_CRUDBASIC
A  CRUD using ASPNET MVC app.

### Queries
```sql
CREATE DATABASE DBCRUDCORE;
GO

USE DBCRUDCORE;
GO

--CREATE TABLE
CREATE TABLE CONTACT (
  UserId INT IDENTITY PRIMARY KEY,
  UserEmail VARCHAR(50) NOT NULL,
  UserName VARCHAR(50) NOT NULL,
  UserPhone VARCHAR(50)
);
GO

-- STORED PROC TO SELECT * CONTACTS
CREATE PROCEDURE sp_List
AS
BEGIN
  SELECT * FROM CONTACT;
END;

GO
USE DBCRUDCORE;
GO

-- SELECT * FROM TABLE
EXEC sp_List

GO
USE DBCRUDCORE;
GO

-- GET AN USER BY ID
create procedure sp_getUser(
@UserId int)
as
begin
select * from CONTACT where UserId = @UserId
end
GO

-- GET AN USER BY ID == TO?
EXEC sp_getUser @UserId = 2;

GO
-- STORED PROCED. TO INSERT A NEW USER
create procedure sp_insertUser(
  @UserEmail VARCHAR(50),
  @UserName VARCHAR(50),
  @UserPhone VARCHAR(50)
)
as
begin
insert into CONTACT(UserName,UserEmail,UserPhone) values(@UserName,@UserEmail,@UserPhone)
end
GO

-- INSERT A NEW USER  USING THE STORED WITH THE FOLLOWING PARAMS
EXEC sp_insertUser @Username = "jeremy", @UserEmail="jeremy@mail.com", @UserPhone ="12345678"

GO
-- STORED PROCED. TO UPDATE AN USER BY ID
create procedure sp_updateUser(
  @UserId int,
  @UserEmail VARCHAR(50),
  @UserName VARCHAR(50),
  @UserPhone VARCHAR(50)
)
as
begin
update CONTACT SET UserName =@UserName, UserEmail = @UserEmail, UserPhone = @UserPhone where UserId = @UserId
end

-- UPDATE BY ID
GO
exec sp_updateUser @UserId=1, @UserName = "Jeremy Willians", @UserPhone ="1234678", @UserEmail="jeremy@mail.com"


GO
-- DELETE AN USER BY ID
create procedure sp_deleteUser(
@UserId int
)
as
begin
delete from CONTACT where UserId = @UserId
end

GO
exec sp_deleteUser @UserId= 1

GO
create procedure sp_CheckUserExistence
    @UserEmail VARCHAR(100)
AS
BEGIN
    SELECT COUNT(*) FROM CONTACT WHERE UserEmail = @UserEmail;
END

GO
exec sp_CheckUserExistence
@UserEmail= "jeremy@mail.com"

```
![Screenshot 2023-07-11 114716](https://github.com/BinaryLeo/ASPNET_CRUDBASIC/assets/72607039/9da02d96-ee96-4013-8cd0-8fa2e4c0f4e2)
![Screenshot 2023-07-11 114757](https://github.com/BinaryLeo/ASPNET_CRUDBASIC/assets/72607039/5e8d456f-2fb2-44bb-a260-1c837db8e206)

![![Screenshot 2023-07-11 114952](https://github.com/BinaryLeo/ASPNET_CRUDBASIC/assets/72607039/de039cde-6344-4f99-b433-048e055c2f11)
Screenshot 2023-07-11 114838](https://github.com/BinaryLeo/ASPNET_CRUDBASIC/assets/72607039/0b7b6d22-1494-4c55-87bf-6732bc99280e)

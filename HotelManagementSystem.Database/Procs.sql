USE Hotel;
GO

CREATE FUNCTION fnIsRoomFree (@roomId INT)
RETURNS BIT
AS
BEGIN
	RETURN (SELECT is_free FROM Room WHERE id = @roomId);	
END
GO

CREATE PROCEDURE spGetAllRooms
AS
BEGIN
	SELECT R.id, R.category, C.name AS 'categoryName', R.places, R.price, R.is_free
	FROM Room AS R
	JOIN Category AS C
	ON R.category = C.id;
END
GO

CREATE PROCEDURE spGetAllFreeRooms
AS
BEGIN
	SELECT R.id, R.category, C.name AS 'categoryName', R.places, R.price, R.is_free
	FROM Room AS R
	JOIN Category AS C
	ON R.category = C.id
	WHERE R.is_free = 1;
END
GO

CREATE PROCEDURE spGetInfoAboutRoom 
	@id INT
AS
BEGIN
	SELECT R.id, C.name AS 'category', R.places, R.price, C.TV, C.conditioner, C.internet, C.jacuzzi 
	FROM Room AS R
	JOIN Category AS C
	ON R.category = C.id
	WHERE R.id = @id;
END
GO

CREATE PROCEDURE spSettleGuestInTheRoom
	@roomId INT,
	@name NVARCHAR(50),
	@passport NVARCHAR(50),
	@check_in_date DATE,
	@check_out_date DATE
AS
BEGIN TRAN
	BEGIN TRY
		IF (dbo.fnIsRoomFree(@roomId) = 1)
			BEGIN
				INSERT INTO Guest(name, passport, room, check_in_date, check_out_date)
					VALUES(@name, @passport, @roomId, @check_in_date, @check_out_date);
		
				UPDATE Room
				SET is_free = 0
				WHERE id = @roomId;
			END
		ELSE
			BEGIN;
				THROW 50100, 'Currently room isn"t free. It is impossible to settle people.', 1;
			END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN 
	END CATCH
GO

CREATE PROCEDURE spAddRoom
	@id INT,
	@category INT,
	@places INT,
	@price INT
AS
BEGIN
	INSERT INTO Room(id, category, places, price)
		VALUES (@id, @category, @places, @price);
END
GO

CREATE PROCEDURE spChangeRoom
	@id INT,
	@category INT,
	@places INT,
	@price INT
AS
BEGIN
	IF (dbo.fnIsRoomFree(@id) = 1)
		BEGIN	
			UPDATE Room
			SET category = @category, places = @places, price = @price
			WHERE id = @id;
		END
	ELSE
		BEGIN;
			THROW 50200, 'Currently room isn"t free. It is impossible to change it.', 1;
		END
END
GO

CREATE PROCEDURE spDeleteRoom
	@id INT
AS
BEGIN
	IF (dbo.fnIsRoomFree(@id) = 1)
		BEGIN	
			DELETE FROM Room
			WHERE id = @id;
		END
	ELSE
		BEGIN;
			THROW 50300, 'Currently room isn"t free. It is impossible to delete it.', 1;
		END
END
GO

CREATE PROCEDURE spGetAllGuests
AS
BEGIN
	SELECT * FROM Guest;
END
GO

CREATE PROCEDURE spChangeCheckOutDate
	@id INT,
	@newDate DATE
AS
BEGIN
	UPDATE Guest
	SET check_out_date = @newDate
	WHERE id = @id;
END 
GO

CREATE PROCEDURE spGetGuestsByName
	@name VARCHAR(50)
AS
BEGIN
	SELECT *
	FROM Guest
	WHERE name LIKE CONCAT('%', @name, '%');
END
GO

CREATE PROCEDURE spGetListOfFreedSoonRooms
	@days INT
AS
BEGIN
	SELECT R.id, R.category, C.name AS 'categoryName', R.places, R.price, R.is_free, G.check_out_date
	FROM Room AS R
	JOIN Category AS C
	ON R.category = C.id
	JOIN Guest AS G
	ON G.room = R.id
	WHERE DATEDIFF(DAY, GETDATE(), G.check_out_date) <= @days
	ORDER BY G.check_out_date;
END
GO

CREATE PROCEDURE spGetListOfLeaveSoonGuests
	@days INT
AS
BEGIN
	SELECT id, name, passport, room, check_in_date, check_out_date
	FROM Guest
	WHERE DATEDIFF(DAY, GETDATE(), check_out_date) <= @days
	ORDER BY check_out_date;
END
GO

CREATE FUNCTION fnCalculateTheLengthOfStay (@guestId INT)
RETURNS INT
AS
BEGIN
	DECLARE @days INT;
	SELECT @days = DATEDIFF(DAY, check_in_date, check_out_date)
	FROM Guest
	WHERE id = @guestId;
	RETURN @days; 
END
GO

CREATE PROCEDURE spCalculateTheLengthOfStay
	@guestId INT
AS
BEGIN
	SELECT dbo.fnCalculateTheLengthOfStay(@guestId) AS result;
END
GO

CREATE FUNCTION fnCalculatePriceOfStay (@guestId INT)
RETURNS INT
AS
BEGIN
	DECLARE @days INT;
	DECLARE @price INT;
	DECLARE @sum INT;

	SET @days = dbo.fnCalculateTheLengthOfStay(@guestId );
	
	SELECT @price = R.price
	FROM Guest AS G
	JOIN Room AS R
	ON G.room = R.id
	WHERE G.id = @guestId;

	SET @sum = @days * @price;

	RETURN @sum; 
END
GO

CREATE PROCEDURE spCalculatePriceOfStay
	@guestId INT
AS
BEGIN
	SELECT dbo.fnCalculatePriceOfStay(@guestId) AS result;
END
GO

CREATE PROCEDURE spFilterRooms
	@category NVARCHAR(50),
	@places INT,
	@priceLow REAL,
	@priceUp REAL,
	@onlyFree BIT
AS
BEGIN
	IF (@onlyFree = 1)
	BEGIN 
		SELECT R.id, R.category, C.name AS 'categoryName', R.places, R.price, R.is_free 
		FROM Room AS R
		JOIN Category AS C
		ON R.category = C.id
		WHERE @category LIKE CONCAT('%', c.name, '%') AND R.places = @places AND R.price >= @priceLow AND R.price <= @priceUP AND is_free = 1;
	END
	ELSE
	BEGIN
		SELECT R.id, R.category, C.name AS 'categoryName', R.places, R.price, R.is_free 
		FROM Room AS R
		JOIN Category AS C
		ON R.category = C.id
		WHERE @category LIKE CONCAT('%', c.name, '%') AND R.places = @places AND R.price >= @priceLow AND R.price <= @priceUP;
	END
END
GO

CREATE PROCEDURE spEvictGuest
	@id INT
AS
BEGIN TRAN
	BEGIN TRY
		UPDATE Room
		SET is_free = 1
		FROM Room
		JOIN Guest ON Room.id = Guest.room
		WHERE Guest.id = @id;

		DELETE FROM Guest
		WHERE id = @id;

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
GO

CREATE PROCEDURE spGetAdministratorByLogin
@Login VARCHAR(50),
@Password VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM Administrator
	WHERE [Login] = @Login and [Password] = @Password;
END;
GO
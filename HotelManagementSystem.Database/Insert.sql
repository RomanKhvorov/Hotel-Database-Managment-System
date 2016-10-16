USE Hotel;
GO

SET IDENTITY_INSERT Category ON;
INSERT INTO Category (id, name, TV, conditioner, internet, jacuzzi)
	VALUES
	(1, 'Econom', 0, 0, 0, 0),
	(2, 'Standard', 1, 0, 1, 0),
	(3, 'Suit', 1, 1, 1, 1),
	(4, 'Family', 1, 1, 1, 0);
SET IDENTITY_INSERT Category OFF;
GO

INSERT INTO Room (id, category, places, price, is_free)
	VALUES
	(101, 1, 2, 500, 0),
	(102, 1, 2, 500, 1),
	(103, 1, 3, 750, 0),
	(104, 1, 3, 750, 1),
	(105, 1, 1, 250, 1),
	(106, 1, 1, 250, 0),
	(107, 2, 2, 700, 1),
	(108, 2, 2, 700, 0),
	(109, 2, 2, 700, 0),
	(110, 2, 2, 700, 1),
	(111, 2, 1, 350, 0),
	(112, 2, 1, 350, 1),
	(113, 2, 3, 1050, 1),
	(114, 2, 3, 1050, 1),
	(201, 3, 1, 1000, 0),
	(202, 3, 2, 2000, 0),
	(203, 3, 3, 3000, 1),
	(204, 4, 4, 1000, 0),
	(205, 4, 4, 1000, 0);
GO

SET IDENTITY_INSERT Guest ON;
INSERT INTO Guest (id, name, passport, room, check_in_date, check_out_date)
	VALUES
	(1, 'Jack Andrews', '586-01-3937', 201, '2016-10-10', '2016-10-20'),
	(2, 'Earl Henderson', '599-95-7526', 103, '2016-10-15', '2016-10-19'),
	(3, 'Jonathan Cole', '443-16-9184', 111, '2016-10-14', '2016-10-21'),
	(4, 'Ralph Holmes', '675-22-9533', 205, '2016-10-08', '2016-10-20'),
	(5, 'Jennifer Stanley', '874-93-4166', 108, '2016-10-15', '2016-10-18'),
	(6, 'Clarence Lynch', '345-35-5475', 202, '2016-10-17', '2016-10-22'),
	(7, 'Kenneth Daniels', '406-49-1755', 101, '2016-10-16', '2016-10-19'),
	(8, 'Frank Hansen', '747-20-9606', 106, '2016-10-12', '2016-10-22'),
	(9, 'Jean Rose', '952-99-5544', 109, '2016-10-14', '2016-10-30'),
	(10, 'Barbara Olson', '337-47-3800', 204, '2016-10-18', '2016-10-20');
SET IDENTITY_INSERT Guest OFF;
GO

UPDATE Room
SET is_free = 0
FROM Room
JOIN Guest
ON Guest.room = Room.id
WHERE Guest.room IS NOT NULL;
GO

SET IDENTITY_INSERT Administrator ON;
INSERT INTO Administrator (id, name, [Login], [Password])
	VALUES
	(1, 'Diane Elliott', 'admin', 'a66abb5684c45962d887564f08346e8d'), --admin123456
	(2, 'Jack Mendoza', 'jack007', '9e94b15ed312fa42232fd87a55db0d39'), --007
	(3, 'Donna Wells', 'DonnaCarleone', 'b0baee9d279d34fa1dfd71aadb908c3f'); --11111
SET IDENTITY_INSERT Administrator OFF;
GO
CREATE PROCEDURE [dbo].[spUsers_GetAll]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [UserName], [NormalizedUserName], [Email],
		   [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp],
		   [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled],
		   [LockoutEnd], [LockoutEnabled], [AccessFailedCount]
	FROM [dbo].[Users]
END

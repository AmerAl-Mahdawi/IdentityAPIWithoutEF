CREATE PROCEDURE [dbo].[spUsers_GetByEmail]
	@NormalizedEmail   NVARCHAR (256)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [UserName], [NormalizedUserName], [Email],
		   [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp],
		   [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled],
		   [LockoutEnd], [LockoutEnabled], [AccessFailedCount]
	FROM [dbo].[Users]
	WHERE [NormalizedEmail] = @NormalizedEmail
END


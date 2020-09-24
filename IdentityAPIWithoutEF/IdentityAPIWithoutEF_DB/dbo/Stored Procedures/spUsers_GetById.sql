CREATE PROCEDURE [dbo].[spUsers_GetById]
	@Id NVARCHAR(128)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [UserName], [NormalizedUserName], [Email],
		   [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp],
		   [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled],
		   [LockoutEnd], [LockoutEnabled], [AccessFailedCount]
	FROM [dbo].[Users]
	WHERE [Id] = @Id
END

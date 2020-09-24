CREATE PROCEDURE [dbo].[spUserRoles_GetUsersInRole]
	@RoleName NVARCHAR (256)

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @RoleId NVARCHAR(128)
	SET @RoleId = (SELECT [Id]
				   FROM [dbo].[Roles]
				   WHERE [NormalizedName] = UPPER(@RoleName))

	SELECT [Id], [UserName], [NormalizedUserName], [Email],
			[NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp],
			[ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled],
			[LockoutEnd], [LockoutEnabled], [AccessFailedCount]
	FROM [dbo].[Users]
	WHERE Id in (SELECT UserId
				 FROM [dbo].[UserRoles]
				 WHERE RoleId = @RoleId)
END

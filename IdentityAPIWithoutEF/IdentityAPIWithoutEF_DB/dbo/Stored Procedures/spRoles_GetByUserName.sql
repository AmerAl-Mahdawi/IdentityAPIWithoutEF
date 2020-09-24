CREATE PROCEDURE [dbo].[spRoles_GetByUserName]
@UserName NVARCHAR(256)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT r.[Name]
	FROM [dbo].[Roles] r
	INNER JOIN [dbo].[UserRoles] ur
	ON ur.[RoleId] = r.[Id]
	INNER JOIN [dbo].[Users] u
	ON u.[Id] = ur.[UserId]
	WHERE u.[UserName] = @UserName
END

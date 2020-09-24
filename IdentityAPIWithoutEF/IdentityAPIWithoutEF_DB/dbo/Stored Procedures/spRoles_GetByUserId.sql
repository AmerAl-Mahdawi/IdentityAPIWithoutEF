CREATE PROCEDURE [dbo].[spRoles_GetByUserId]
@UserId NVARCHAR(128)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT r.[Name]
	FROM [dbo].[Roles] r
	INNER JOIN [dbo].[UserRoles] ur
	ON ur.[RoleId] = r.[Id]
	WHERE ur.[UserId] = @UserId
END

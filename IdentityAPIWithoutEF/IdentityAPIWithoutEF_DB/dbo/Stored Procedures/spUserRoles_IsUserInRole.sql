CREATE PROCEDURE [dbo].[spUserRoles_IsUserInRole]
	@RoleName NVARCHAR (256),
	@UserId   NVARCHAR (128)

AS
BEGIN
	SET NOCOUNT ON;
		
	SELECT COUNT(*)
	FROM [dbo].[UserRoles]
	WHERE UserId = @UserId
	AND RoleId = (SELECT [Id]
				  FROM [dbo].[Roles]
				  WHERE [NormalizedName] = UPPER(@RoleName))
END
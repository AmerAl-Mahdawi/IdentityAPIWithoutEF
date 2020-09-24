CREATE PROCEDURE [dbo].[spUserRoles_RemoveRoleFromUser]
	@RoleName NVARCHAR (256),
	@UserId   NVARCHAR (128)

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @RoleId NVARCHAR(128)
	SET @RoleId = (SELECT [Id]
				   FROM [dbo].[Roles]
				   WHERE [NormalizedName] = UPPER(@RoleName))

	DELETE FROM [dbo].[UserRoles]
	WHERE RoleId = @RoleId
END

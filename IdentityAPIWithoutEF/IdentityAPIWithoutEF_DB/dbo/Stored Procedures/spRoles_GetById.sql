CREATE PROCEDURE [dbo].[spRoles_GetById]
	@Id NVARCHAR(128)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name], [NormalizedName], [ConcurrencyStamp]
	FROM [dbo].[Roles]
	WHERE [Id] = @Id
END

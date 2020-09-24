CREATE PROCEDURE [dbo].[spRoles_GetByName]
	@NormalizedName   NVARCHAR (256)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name], [NormalizedName], [ConcurrencyStamp]
	FROM [dbo].[Roles]
	WHERE [NormalizedName] = @NormalizedName
END

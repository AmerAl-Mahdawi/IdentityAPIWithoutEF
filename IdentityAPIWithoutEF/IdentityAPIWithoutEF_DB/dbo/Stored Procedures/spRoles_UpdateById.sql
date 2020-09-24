CREATE PROCEDURE [dbo].[spRoles_UpdateById]
	@Id               NVARCHAR (128),
    @Name             NVARCHAR (256),
    @NormalizedName   NVARCHAR (256),
    @ConcurrencyStamp NVARCHAR (MAX)

AS
BEGIN
	SET NOCOUNT ON;

    UPDATE [dbo].[Roles]
    SET [Name] = @Name, [NormalizedName] = @NormalizedName, [ConcurrencyStamp] = @ConcurrencyStamp
    WHERE [Id] = @Id
END


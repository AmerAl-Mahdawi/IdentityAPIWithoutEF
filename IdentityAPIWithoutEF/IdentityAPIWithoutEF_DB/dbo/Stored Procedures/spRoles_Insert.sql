CREATE PROCEDURE [dbo].[spRoles_Insert]
    @Id                 NVARCHAR (128),
    @Name               NVARCHAR (256),
    @NormalizedRoleName NVARCHAR (256),
    @ConcurrencyStamp   NVARCHAR (MAX)

    AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [dbo].[Roles]([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    OUTPUT INSERTED.Id
	VALUES (NEWID(), @Name, @NormalizedRoleName, @ConcurrencyStamp)
END

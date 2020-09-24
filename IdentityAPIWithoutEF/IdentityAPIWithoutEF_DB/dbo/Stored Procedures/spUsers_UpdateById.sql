CREATE PROCEDURE [dbo].[spUsers_UpdateById]
	@Id                   NVARCHAR (128)    ,
    @UserName             NVARCHAR (256)    ,
    @NormalizedUserName   NVARCHAR (256)    ,
    @Email                NVARCHAR (256)    ,
    @NormalizedEmail      NVARCHAR (256)    ,
    @EmailConfirmed       BIT               ,
    @PasswordHash         NVARCHAR (MAX)    ,
    @SecurityStamp        NVARCHAR (MAX)    ,
    @ConcurrencyStamp     NVARCHAR (MAX)    ,
    @PhoneNumber          NVARCHAR (MAX)    ,
    @PhoneNumberConfirmed BIT               ,
    @TwoFactorEnabled     BIT               ,   
    @LockoutEnd           DATETIMEOFFSET (7),
    @LockoutEnabled       BIT               ,
    @AccessFailedCount    INT

AS
BEGIN
	SET NOCOUNT ON;

    UPDATE [dbo].[Users]
    SET [UserName] = @UserName, [NormalizedUserName] = @NormalizedUserName,
        [Email] = @Email, [NormalizedEmail] = @NormalizedEmail, [EmailConfirmed] = @EmailConfirmed, [PasswordHash] = @PasswordHash,
        [SecurityStamp] = @SecurityStamp, [ConcurrencyStamp] = @ConcurrencyStamp, [PhoneNumber] = @PhoneNumber,
        [PhoneNumberConfirmed] = @PhoneNumberConfirmed, [TwoFactorEnabled] = @TwoFactorEnabled, [LockoutEnd] = @LockoutEnd,
        [LockoutEnabled] = @LockoutEnabled, [AccessFailedCount] = @AccessFailedCount
    WHERE [Id] = @Id
END

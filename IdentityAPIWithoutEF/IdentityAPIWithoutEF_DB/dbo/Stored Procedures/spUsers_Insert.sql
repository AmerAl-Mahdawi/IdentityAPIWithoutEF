CREATE PROCEDURE [dbo].[spUsers_Insert]
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

	INSERT INTO [dbo].[Users]([Id], [UserName], [NormalizedUserName], [Email],
			[NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp],
			[ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled],
			[LockoutEnd], [LockoutEnabled], [AccessFailedCount])
    OUTPUT INSERTED.Id
	VALUES (NEWID(), @UserName, @NormalizedUserName, @Email,
			@NormalizedEmail, @EmailConfirmed, @PasswordHash, @SecurityStamp,
			@ConcurrencyStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled,
			@LockoutEnd, @LockoutEnabled, @AccessFailedCount)
END

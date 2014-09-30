CREATE PROCEDURE [dbo].[ChangeUsersTypeToPaid]
    @ID [int]
AS
BEGIN
    UPDATE dbo.[User] SET Discriminator='PaidUser' WHERE ID = @ID
END
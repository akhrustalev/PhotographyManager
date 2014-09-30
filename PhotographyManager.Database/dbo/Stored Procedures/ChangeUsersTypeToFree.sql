CREATE PROCEDURE [dbo].[ChangeUsersTypeToFree]
    @ID [int]
AS
BEGIN
    UPDATE dbo.[User] SET Discriminator='FreeUser' WHERE ID = @ID
END
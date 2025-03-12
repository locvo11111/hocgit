UPDATE AppRoles
   SET [Id] = @Id
      ,[Description] = @Description
      ,[Name] = @Name
      ,[NormalizedName] = @NormalizedName
      ,[ConcurrencyStamp] = @ConcurrencyStamp
 WHERE Id = @Id
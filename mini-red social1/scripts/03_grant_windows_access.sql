-- Run as a SQL Server administrator after creating the database.
-- Replace this with the teacher's Windows account, for example N'MYPC\profesor'.
DECLARE @LoginName sysname = N'COMPUTER\usuario';
DECLARE @Sql nvarchar(max);

IF SUSER_ID(@LoginName) IS NULL
BEGIN
    SET @Sql = N'CREATE LOGIN ' + QUOTENAME(@LoginName) + N' FROM WINDOWS;';
    EXEC sys.sp_executesql @Sql;
END;

USE [MiniRedSocialDb];

IF DATABASE_PRINCIPAL_ID(@LoginName) IS NULL
BEGIN
    SET @Sql = N'CREATE USER ' + QUOTENAME(@LoginName) +
               N' FOR LOGIN ' + QUOTENAME(@LoginName) + N';';
    EXEC sys.sp_executesql @Sql;
END;

IF ISNULL(IS_ROLEMEMBER(N'db_datareader', @LoginName), 0) <> 1
BEGIN
    SET @Sql = N'ALTER ROLE [db_datareader] ADD MEMBER ' + QUOTENAME(@LoginName) + N';';
    EXEC sys.sp_executesql @Sql;
END;

IF ISNULL(IS_ROLEMEMBER(N'db_datawriter', @LoginName), 0) <> 1
BEGIN
    SET @Sql = N'ALTER ROLE [db_datawriter] ADD MEMBER ' + QUOTENAME(@LoginName) + N';';
    EXEC sys.sp_executesql @Sql;
END;

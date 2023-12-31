USE EasyBudget
GO
/****** Object:  StoredProcedure [dbo].[sp_getRolesForUser]    Script Date: 19/7/2018 14:15:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Ramos
-- =============================================
create PROCEDURE [dbo].[sp_getRolesForLogin]
	@loginName varchar(20)
	
AS
BEGIN
	SET NOCOUNT ON;

	select r.RoleName from LOGINSInROLES ur
	inner join LOGINS u on ur.Login_LoginId = u.LoginId
	inner join ROLES r on ur.Role_RoleId = r.RoleId
	where u.LoginUsername = @loginName
	
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Ramos
-- =============================================
Create PROCEDURE [dbo].[sp_getLoginRole]
	
	@roleName varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	
	select u.LoginUsername from LOGINSInROLES ur
	inner join LOGINS u on ur.Login_LoginId = u.LoginId
	inner join ROLES r on ur.Role_RoleId = r.RoleId
	where r.RoleName = @roleName 
 
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Ramos
-- =============================================
Create PROCEDURE [dbo].[sp_isLoginInRole]
	@loginName varchar(20),
	@roleName varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	declare @resultado bit = 0;

	if exists(
	select * from LOGINSInROLES ur
	inner join LOGINS u on ur.Login_LoginId = u.LoginId
	inner join ROLES r on ur.Role_RoleId = r.RoleId
	where r.RoleName = @roleName and u.LoginUsername = @loginName)
	set @resultado = 1
	 
	select @resultado
END

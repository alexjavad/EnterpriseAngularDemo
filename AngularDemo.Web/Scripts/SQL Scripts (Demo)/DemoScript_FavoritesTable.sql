/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
	   fav.[Id]
      ,d.[FirstName] + ' ' + d.[LastName] AS [Developer]
      ,c.[Color]
  FROM 
		[dbo].[DEMO_DeveloperColorXRef] AS fav
  INNER JOIN
		[dbo].[DEMO_Colors] AS c
			ON fav.ColorId = c.Id
  INNER JOIN
		[dbo].[DEMO_Developer] AS d
			ON fav.DeveloperId = d.Id

		

SELECT TOP(1) 
	[s].[StudentID], [s].[Email], [s].[FavoriteCourseId], 
	[t].[StudentID], [t].[FirstName], [t].[LastName]
FROM [Student] AS [s]
LEFT JOIN (
    SELECT [s0].[StudentID], [s0].[FirstName], [s0].[LastName], [s1].[StudentID] AS [StudentID0]
    FROM [Student] AS [s0]
    INNER JOIN [Student] AS [s1] ON [s0].[StudentID] = [s1].[StudentID]
    WHERE [s0].[LastName] IS NOT NULL OR [s0].[FirstName] IS NOT NULL
) AS [t] ON [s].[StudentID] = [t].[StudentID]
WHERE [s].[StudentID] = @StudentID
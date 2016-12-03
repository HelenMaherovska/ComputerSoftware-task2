USE [Library]
GO
SET IDENTITY_INSERT [dbo].[Section] ON 

GO
INSERT [dbo].[Section] ([id], [name]) VALUES (1, N'Detectives')
GO
INSERT [dbo].[Section] ([id], [name]) VALUES (2, N'Special literature')
GO
INSERT [dbo].[Section] ([id], [name]) VALUES (3, N'Schoolbooks')
GO
SET IDENTITY_INSERT [dbo].[Section] OFF
GO
SET IDENTITY_INSERT [dbo].[Estimate] ON 

GO
INSERT [dbo].[Estimate] ([id], [origin], [availability], [worth], [recommendation]) VALUES (1, N'origin1', 1, N'worth1', N'recommeddation1')
GO
INSERT [dbo].[Estimate] ([id], [origin], [availability], [worth], [recommendation]) VALUES (2, N'origin2', 0, N'worth2', N'reccomendation2')
GO
INSERT [dbo].[Estimate] ([id], [origin], [availability], [worth], [recommendation]) VALUES (3, N'origin3', 1, N'worth3', N'recommendation3')
GO
INSERT [dbo].[Estimate] ([id], [origin], [availability], [worth], [recommendation]) VALUES (4, N'origin1', 1, N'worth1', N'recom')
GO
INSERT [dbo].[Estimate] ([id], [origin], [availability], [worth], [recommendation]) VALUES (5, N'origin1', 1, N'worth123', N'rr')
GO
INSERT [dbo].[Estimate] ([id], [origin], [availability], [worth], [recommendation]) VALUES (9, N'my awesome origin', 1, N'my awesome worth', N'my awesome recommendation')
GO
INSERT [dbo].[Estimate] ([id], [origin], [availability], [worth], [recommendation]) VALUES (13, N'or', 0, N'wor', N'rec')
GO
INSERT [dbo].[Estimate] ([id], [origin], [availability], [worth], [recommendation]) VALUES (14, N'orrr', 1, N'WORTHH', N'recome')
GO
SET IDENTITY_INSERT [dbo].[Estimate] OFF
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

GO
INSERT [dbo].[Book] ([id], [author], [title], [edition], [year], [pages], [section_id], [estimate_id]) VALUES (1, N'author1', N'title1', N'edition2221', 2001, 101, 1, 1)
GO
INSERT [dbo].[Book] ([id], [author], [title], [edition], [year], [pages], [section_id], [estimate_id]) VALUES (2, N'author2', N'title2', N'edition2', 2002, 102, 2, 2)
GO
INSERT [dbo].[Book] ([id], [author], [title], [edition], [year], [pages], [section_id], [estimate_id]) VALUES (3, N'author3', N'title3', N'edition3', 2003, 103, 3, 3)
GO
INSERT [dbo].[Book] ([id], [author], [title], [edition], [year], [pages], [section_id], [estimate_id]) VALUES (4, N'author11', N'title11', N'edi34tion', 2011, 111, 1, 4)
GO
INSERT [dbo].[Book] ([id], [author], [title], [edition], [year], [pages], [section_id], [estimate_id]) VALUES (5, N'author111', N'title111', N'edition212', 2011, 111, 1, 5)
GO
INSERT [dbo].[Book] ([id], [author], [title], [edition], [year], [pages], [section_id], [estimate_id]) VALUES (8, N'my awesome author', N'my awesome title', N'my awesome edition', 1000001, 1000001, 1, 9)
GO
INSERT [dbo].[Book] ([id], [author], [title], [edition], [year], [pages], [section_id], [estimate_id]) VALUES (10, N'auth', N'titl', N'edi', 222, 33, 3, 13)
GO
INSERT [dbo].[Book] ([id], [author], [title], [edition], [year], [pages], [section_id], [estimate_id]) VALUES (11, N'authi', N'qwert', N'qwert', 1234, 1234567, 3, 14)
GO
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
INSERT [dbo].[Detective] ([id], [heroes_number]) VALUES (1, 11)
GO
INSERT [dbo].[Detective] ([id], [heroes_number]) VALUES (4, 44)
GO
INSERT [dbo].[Detective] ([id], [heroes_number]) VALUES (5, 55)
GO
INSERT [dbo].[Detective] ([id], [heroes_number]) VALUES (8, 1000001)
GO
INSERT [dbo].[Schoolbook] ([id], [form], [subject]) VALUES (10, 2, N'222')
GO
INSERT [dbo].[Schoolbook] ([id], [form], [subject]) VALUES (11, 11, N'xcvbnm,')
GO

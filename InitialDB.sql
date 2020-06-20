USE [EFCoreDDD]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 6/20/2020 7:03:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollment]    Script Date: 6/20/2020 7:03:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollment](
	[EnrollmentID] [uniqueidentifier] NOT NULL,
	[CourseID] [uniqueidentifier] NOT NULL,
	[StudentID] [uniqueidentifier] NOT NULL,
	[Grade] [int] NOT NULL,
 CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
(
	[EnrollmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 6/20/2020 7:03:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FavoriteCourseID] [uniqueidentifier] NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[NameSuffixID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suffix]    Script Date: 6/20/2020 7:03:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suffix](
	[SuffixID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Suffix] PRIMARY KEY CLUSTERED 
(
	[SuffixID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Course] ([CourseID], [Name]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213671', N'Calculus')
INSERT [dbo].[Course] ([CourseID], [Name]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213672', N'Chemistry')
INSERT [dbo].[Course] ([CourseID], [Name]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213673', N'Literature')
INSERT [dbo].[Course] ([CourseID], [Name]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213674', N'Trigonometry')
INSERT [dbo].[Course] ([CourseID], [Name]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213675', N'Microeconomics')
INSERT [dbo].[Student] ([StudentID], [FirstName], [Email], [FavoriteCourseID], [LastName], [NameSuffixID]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213661', N'David', N'david.hu@gmail.com', N'0e9fbb79-6c71-42ff-942f-74bdd9213671', N'Hu', N'0e9fbb79-6c71-42ff-942f-74bdd9213681')
INSERT [dbo].[Student] ([StudentID], [FirstName], [Email], [FavoriteCourseID], [LastName], [NameSuffixID]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213662', N'Chris', N'chris.goh@gmail.com', N'0e9fbb79-6c71-42ff-942f-74bdd9213672', N'Goh', N'0e9fbb79-6c71-42ff-942f-74bdd9213681')
INSERT [dbo].[Suffix] ([SuffixID], [Name]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213681', N'Jr')
INSERT [dbo].[Suffix] ([SuffixID], [Name]) VALUES (N'0e9fbb79-6c71-42ff-942f-74bdd9213682', N'Sr')
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Course]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Student]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Course] FOREIGN KEY([FavoriteCourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Course]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Suffix] FOREIGN KEY([NameSuffixID])
REFERENCES [dbo].[Suffix] ([SuffixID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Suffix]
GO
